using raincoat.Domains.Entities;
using raincoat.Domains.Services;
using raincoat.Domains.ValueObjects;
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
        public SerialPortService SerialPortService { get; private set; }
        public OBSWebSocketService OBSWebSocketService { get; private set; }

        public Config()
        {
            InitializeComponent();
            Disposed += OnDispose;

            InitializeServices();
            InitializeUI();
            InitializeDataGrid();

            save = new Save();
            load = new Load();
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

        private void InitializeDataGrid()
        {
            KeyBindDataGrid.Columns.RemoveAt(1);
            KeyBindDataGrid.Columns.Insert(1, skillItemsRepository.GetDataGridViewComboBoxColumn());
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
            var keyCommandPairs = new List<KeyCommandPair>();

            foreach (DataGridViewRow row in KeyBindDataGrid.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }

                var buttonId = row.Cells["ButtonID"].Value?.ToString() ?? string.Empty;
                var skillType = (SkillType)int.Parse(row.Cells["Skill"].Value?.ToString() ?? "0");
                var argument = row.Cells["Argument"].Value?.ToString() ?? string.Empty;

                keyCommandPairs.Add(new KeyCommandPair(buttonId, skillType, argument));
            }

            var connectionSetting = new ConnectionSetting(
                HostAddress.Text,
                (int)PortNumber.Value,
                Password.Text);

            save.Execute(new SaveInputPack(new ConfigData(connectionSetting, keyCommandPairs)));
        }

        private void buttonReconnect_Click(object sender, EventArgs e)
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
                HostAddress.Text = output.ConfigData.ConnectionSetting.HostAddress;
                PortNumber.Value = output.ConfigData.ConnectionSetting.Port;
                Password.Text = output.ConfigData.ConnectionSetting.Password;

                KeyBindDataGrid.Rows.Clear();
                foreach (var item in output.ConfigData.KeyCommands.OrderBy(keys => keys.ButtonId))
                {
                    KeyBindDataGrid.Rows.Add(item.ButtonId, (int)item.SkillType, item.Argument);
                }

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

        private void OnReceived(IList<KeyState>? keyStates)
        {
            if (keyStates == null)
            {
                return;
            }

            foreach (var key in keyStates)
            {
                foreach (DataGridViewRow row in KeyBindDataGrid.Rows)
                {
                    if (row.IsNewRow)
                    {
                        continue;
                    }

                    var buttonId = row.Cells["ButtonID"].Value?.ToString() ?? string.Empty;
                    var skillType = (SkillType)int.Parse(row.Cells["Skill"].Value?.ToString() ?? "0");
                    var argument = row.Cells["Argument"].Value?.ToString() ?? string.Empty;

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
    }
}
