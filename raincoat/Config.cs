using raincoat.Domains.Entities;
using raincoat.Domains.Services;
using raincoat.Infrastructures.Adapters;
using raincoat.Infrastructures.Repositories;
using raincoat.UseCases.Config;
using raincoat.UseCases.Triggers;
using System.IO.Ports;

namespace raincoat
{
    public partial class Config : Form
    {
        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;
        private readonly SkillItemsRepository skillItemsRepository = new();
        private readonly MonitorActiveWindow monitor;
        private readonly Save save;
        private readonly Load load;

        private Dictionary<string, Label> labels;
        private ConfigData configData;

        private readonly ISkillService _skillService;
        private readonly IActiveWindowService _activeWindowService;

        public SerialPortService? SerialPortService { get; private set; }
        public OBSWebSocketService OBSWebSocketService { get; private set; }

        public Config()
        {
            InitializeComponent();
            Disposed += OnDispose;

            _skillService = new SkillService();
            _activeWindowService = new ActiveWindowService();

            InitializeServices();
            InitializeUI();

            monitor = new MonitorActiveWindow();
            save = new Save();
            load = new Load();

            this.labels = new()
            {
                { "SW1", labelName1 },
                { "SW2", labelName2 },
                { "SW3", labelName3 },
                { "SW4", labelName4 },
                { "SW5", labelName5 },
                { "SW6", labelName6 },
                { "SW7", labelName7 },
                { "SW8", labelName8 },
                { "SW9", labelName9 },
                { "SW10", labelName10},
                { "SW11", labelName11},
                { "SW12", labelName12},
            };
        }

        private void InitializeServices()
        {
            OBSWebSocketService = new OBSWebSocketService();

            OBSWebSocketService.OnConnected((sender, e) =>
            {
                ConnectionStatus.Text = "Connected to OBS.";
            });
            OBSWebSocketService.OnDisconnected((sender, e) =>
            {
                ConnectionStatus.Text = "Disconnected from OBS.";
            });
        }

        private void InitializeUI()
        {
            trayMenu = new ContextMenuStrip();
            // 「再接続」メニューの追加
            trayMenu.Items.Add("再接続", null, buttonReconnect_Click);

            // 区切り線の追加
            trayMenu.Items.Add(new ToolStripSeparator());

            // 「終了」メニューの追加
            trayMenu.Items.Add("終了", null, OnExit);

            trayIcon = new NotifyIcon
            {
                Text = this.Text,
                Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath),
                ContextMenuStrip = trayMenu,
                Visible = true
            };

            trayIcon.DoubleClick += TrayIcon_DoubleClick;

            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;

            InitializeArduinoSettings();
        }

        private void InitializeArduinoSettings()
        {
            // COMポートのリストを取得して設定
            comboCOM.Items.AddRange(SerialPort.GetPortNames());
            if (comboCOM.Items.Count > 0)
            {
                comboCOM.SelectedIndex = 0;
            }

            // ボーレートのリストを設定
            var baudRates = new string[] { "9600", "14400", "19200", "38400", "57600", "115200" };
            comboBitParSec.Items.AddRange(baudRates);
            comboBitParSec.SelectedItem = "9600";
        }

        private void OnDispose(object? sender, EventArgs e)
        {
            components?.Dispose();
            trayIcon.Dispose();
        }

        private void OnExit(object? sender, EventArgs e)
        {
            SerialPortService?.CloseSerialPort();
            trayIcon.Dispose();
            Application.Exit();
        }

        private void TrayIcon_DoubleClick(object? sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            Activate();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            // 現在のUIの状態（OBS設定など）をConfigDataに反映させてから保存する
            this.configData.ConnectionSetting.HostAddress = HostAddress.Text;
            this.configData.ConnectionSetting.Port = (int)PortNumber.Value;
            this.configData.ConnectionSetting.Password = Password.Text;

            save.Execute(new SaveInputPack(this.configData));
            
            // フォームを非表示にする
            this.Hide();
        }

        private void buttonReconnect_Click(object? sender, EventArgs e)
        {
            // UIから値を取得
            var comPort = comboCOM.SelectedItem?.ToString();
            if (!int.TryParse(comboBitParSec.SelectedItem?.ToString(), out var baudRate))
            {
                MessageBox.Show("ボーレートを正しく選択してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 接続試行
            ConnectSerialPort(comPort, baudRate);

            // OBSも再接続
            OBSWebSocketService.Disconnect();
            OBSWebSocketService.Connect();
        }
        
        private void ConnectSerialPort(string? comPort, int baudRate)
        {
            if (string.IsNullOrEmpty(comPort))
            {
                // ポート名が指定されていない場合は何もしない（エラーも表示しない）
                return;
            }

            try
            {
                // 既存のポートを閉じる
                SerialPortService?.CloseSerialPort();

                // SerialPortServiceを初期化して接続
                SerialPortService = new SerialPortService(new SerialPortWrapper(comPort, baudRate), OnReceived);
                SerialPortService.OpenSerialPort();

                // 成功した設定を保存
                this.configData.ConnectionSetting.SerialPortName = comPort;
                this.configData.ConnectionSetting.BaudRate = baudRate;
                save.Execute(new SaveInputPack(this.configData));
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"シリアルポートへの接続に失敗しました：{ex.Message}",
                    "エラー",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        private void Config_Load(object sender, EventArgs e)
        {
            try
            {
                var output = load.Execute(new LoadInputPack());
                this.configData = output.ConfigData;
                this.ReloadKeyBindings(this.configData);

                // OBS設定をUIに反映
                HostAddress.Text = this.configData.ConnectionSetting.HostAddress;
                PortNumber.Value = this.configData.ConnectionSetting.Port;
                Password.Text = this.configData.ConnectionSetting.Password;

                // シリアルポート設定をUIに反映
                if (!string.IsNullOrEmpty(this.configData.ConnectionSetting.SerialPortName))
                {
                    comboCOM.SelectedItem = this.configData.ConnectionSetting.SerialPortName;
                }
                comboBitParSec.SelectedItem = this.configData.ConnectionSetting.BaudRate.ToString();

                // OBSに接続
                OBSWebSocketService.Connect(
                    HostAddress.Text,
                    (int)PortNumber.Value,
                    Password.Text);

                // シリアルポートに接続
                ConnectSerialPort(
                    this.configData.ConnectionSetting.SerialPortName,
                    this.configData.ConnectionSetting.BaudRate);

                RestartMonitor();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ConnectionStatus.Text = "接続失敗しました。";
            }
        }

        private void ReloadKeyBindings(ConfigData configData)
        {
            foreach (var item in this.labels.Values)
            {
                item.Text = "(未設定)";
            }

            foreach (var item in configData.KeyCommands.OrderBy(keys => keys.ButtonId))
            {
                if (this.labels.ContainsKey(item.ButtonId))
                {
                    var label = this.labels[item.ButtonId];
                    label.Text = item.ButtonName;
                }
            }
        }

        private void OnReceived(IList<KeyState>? keyStates)
        {
            if (keyStates == null)
            {
                return;
            }

            foreach (var key in keyStates)
            {
                foreach (var keyCommand in this.configData.KeyCommands)
                {
                    var buttonId = keyCommand.ButtonId;
                    var skillType = keyCommand.SkillType;
                    var argument = keyCommand.Argument;

                    if (buttonId == key.Button)
                    {
                        _skillService.Execute(
                            skillType,
                            argument,
                            this.configData.ConnectionSetting,
                            this.OBSWebSocketService);
                    }
                }
            }
        }

        private void Config_FormClosed(object sender, FormClosedEventArgs e)
        {
            monitor.Stop();
            SerialPortService?.CloseSerialPort();
            OBSWebSocketService.Disconnect();
        }

        private void Config_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
        }

        private void ShowButtonConfig(object sender, EventArgs e)
        {
            if (sender is Button clickedButton)
            {
                // 押されたボタンの名前を取得
                string buttonName = clickedButton.Text;
                ButtonSetting buttonSettingForm = new ButtonSetting(buttonName);
                buttonSettingForm.ShowDialog();

                var output = load.Execute(new LoadInputPack());
                this.configData = output.ConfigData;
                this.ReloadKeyBindings(output.ConfigData);

                RestartMonitor();
            }
        }

        private void RestartMonitor()
        {
            // Stop the current monitor
            this.monitor.Stop();

            // Start the window monitor with the new config
            var monitorInput = new MonitorActiveWindowInputPack(
                this.configData,
                _activeWindowService,
                _skillService,
                this.OBSWebSocketService);
            this.monitor.Execute(monitorInput);
        }
    }
}
