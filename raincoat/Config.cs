using raincoat.Domains.Entities;
using raincoat.Domains.Services;
using raincoat.Infrastructures.Repositories;
using raincoat.UseCases;
using raincoat.UseCases.Config;
using raincoat.UseCases.Skills;

namespace raincoat
{
    public partial class Config : Form
    {
        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;
        private readonly SkillItemsRepository skillItemsRepository = new();
        private readonly Save save;
        private readonly Load load;

        private Dictionary<string, Label> labels;
        private ConfigData configData;

        public SerialPortService SerialPortService { get; private set; }
        public OBSWebSocketService OBSWebSocketService { get; private set; }

        public Config()
        {
            InitializeComponent();
            Disposed += OnDispose;

            InitializeServices();
            InitializeUI();

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
            SerialPortService = new SerialPortService("COM5", 9600, OnReceived);
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
        }

        private void OnDispose(object? sender, EventArgs e)
        {
            components?.Dispose();
            trayIcon.Dispose();
        }

        private void OnExit(object? sender, EventArgs e)
        {
            SerialPortService.CloseSerialPort();
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
            var connectionSetting = new ConnectionSetting(
                HostAddress.Text,
                (int)PortNumber.Value,
                Password.Text);

            save.Execute(new SaveInputPack(new ConfigData(
                connectionSetting,
                this.configData.KeyCommands)));
        }

        private void buttonReconnect_Click(object? sender, EventArgs e)
        {
            try
            {
                SerialPortService.CloseSerialPort();
                SerialPortService.OpenSerialPort();

                OBSWebSocketService.Disconnect();
                OBSWebSocketService.Connect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"接続に失敗しました：{ex.Message}",
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

                HostAddress.Text = output.ConfigData.ConnectionSetting.HostAddress;
                PortNumber.Value = output.ConfigData.ConnectionSetting.Port;
                Password.Text = output.ConfigData.ConnectionSetting.Password;

                OBSWebSocketService.Connect(
                    HostAddress.Text,
                    (int)PortNumber.Value,
                    Password.Text);

                SerialPortService.OpenSerialPort();
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
                        IUseCase<SkillInputPack, SkillOutputPack> usecase = SkillService.Get(skillType);
                        usecase.Execute(new SkillInputPack(OBSWebSocketService, argument));
                    }
                }
            }
        }

        private void Config_FormClosed(object sender, FormClosedEventArgs e)
        {
            SerialPortService.CloseSerialPort();
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
            }
        }
    }
}
