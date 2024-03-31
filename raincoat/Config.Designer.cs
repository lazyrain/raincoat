namespace raincoat
{
    partial class Config
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Config));
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.ConnectionStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.KeyBindDataGrid = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.PortNumber = new System.Windows.Forms.NumericUpDown();
            this.labelPassword = new System.Windows.Forms.Label();
            this.labelPort = new System.Windows.Forms.Label();
            this.labelHostAddress = new System.Windows.Forms.Label();
            this.Password = new System.Windows.Forms.TextBox();
            this.HostAddress = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonReconnect = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.ButtonID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CommandType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Argument = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StatusBar.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KeyBindDataGrid)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PortNumber)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // StatusBar
            // 
            this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConnectionStatus});
            this.StatusBar.Location = new System.Drawing.Point(0, 323);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(541, 22);
            this.StatusBar.TabIndex = 2;
            this.StatusBar.Text = "statusStrip1";
            // 
            // ConnectionStatus
            // 
            this.ConnectionStatus.Name = "ConnectionStatus";
            this.ConnectionStatus.Size = new System.Drawing.Size(57, 17);
            this.ConnectionStatus.Text = "unknown";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(517, 275);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.KeyBindDataGrid);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(509, 247);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "キーバインド";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // KeyBindDataGrid
            // 
            this.KeyBindDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.KeyBindDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ButtonID,
            this.CommandType,
            this.Argument});
            this.KeyBindDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.KeyBindDataGrid.Location = new System.Drawing.Point(3, 3);
            this.KeyBindDataGrid.Name = "KeyBindDataGrid";
            this.KeyBindDataGrid.RowTemplate.Height = 25;
            this.KeyBindDataGrid.Size = new System.Drawing.Size(503, 241);
            this.KeyBindDataGrid.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.PortNumber);
            this.tabPage2.Controls.Add(this.labelPassword);
            this.tabPage2.Controls.Add(this.labelPort);
            this.tabPage2.Controls.Add(this.labelHostAddress);
            this.tabPage2.Controls.Add(this.Password);
            this.tabPage2.Controls.Add(this.HostAddress);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(509, 247);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "OBS接続設定";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(450, 265);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // PortNumber
            // 
            this.PortNumber.Location = new System.Drawing.Point(71, 35);
            this.PortNumber.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.PortNumber.Name = "PortNumber";
            this.PortNumber.Size = new System.Drawing.Size(100, 23);
            this.PortNumber.TabIndex = 11;
            this.PortNumber.Value = new decimal(new int[] {
            4455,
            0,
            0,
            0});
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(8, 67);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(57, 15);
            this.labelPassword.TabIndex = 8;
            this.labelPassword.Text = "Password";
            this.labelPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(8, 37);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(29, 15);
            this.labelPort.TabIndex = 9;
            this.labelPort.Text = "Port";
            this.labelPort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelHostAddress
            // 
            this.labelHostAddress.AutoSize = true;
            this.labelHostAddress.Location = new System.Drawing.Point(8, 9);
            this.labelHostAddress.Name = "labelHostAddress";
            this.labelHostAddress.Size = new System.Drawing.Size(32, 15);
            this.labelHostAddress.TabIndex = 10;
            this.labelHostAddress.Text = "Host";
            this.labelHostAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(71, 64);
            this.Password.Name = "Password";
            this.Password.PasswordChar = '*';
            this.Password.Size = new System.Drawing.Size(100, 23);
            this.Password.TabIndex = 6;
            // 
            // HostAddress
            // 
            this.HostAddress.Location = new System.Drawing.Point(71, 6);
            this.HostAddress.Name = "HostAddress";
            this.HostAddress.Size = new System.Drawing.Size(100, 23);
            this.HostAddress.TabIndex = 7;
            this.HostAddress.Text = "localhost";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonReconnect);
            this.panel1.Controls.Add(this.buttonClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 293);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(541, 30);
            this.panel1.TabIndex = 14;
            // 
            // buttonReconnect
            // 
            this.buttonReconnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReconnect.Location = new System.Drawing.Point(382, 4);
            this.buttonReconnect.Name = "buttonReconnect";
            this.buttonReconnect.Size = new System.Drawing.Size(75, 23);
            this.buttonReconnect.TabIndex = 2;
            this.buttonReconnect.Text = "再接続";
            this.buttonReconnect.UseVisualStyleBackColor = true;
            this.buttonReconnect.Click += new System.EventHandler(this.buttonReconnect_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(463, 4);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "OK";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // ButtonID
            // 
            this.ButtonID.HeaderText = "ボタンID";
            this.ButtonID.Name = "ButtonID";
            // 
            // CommandType
            // 
            this.CommandType.HeaderText = "コマンド";
            this.CommandType.Items.AddRange(new object[] {
            "シーン切替",
            "配信開始",
            "配信終了",
            "パス起動"});
            this.CommandType.Name = "CommandType";
            this.CommandType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CommandType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Argument
            // 
            this.Argument.HeaderText = "引数";
            this.Argument.Name = "Argument";
            // 
            // Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 345);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.StatusBar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Config";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Raincoat";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Config_FormClosed);
            this.Load += new System.EventHandler(this.Config_Load);
            this.Resize += new System.EventHandler(this.Config_Resize);
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.KeyBindDataGrid)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PortNumber)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private StatusStrip StatusBar;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Button button1;
        private NumericUpDown PortNumber;
        private Label labelPassword;
        private Label labelPort;
        private Label labelHostAddress;
        private TextBox Password;
        private TextBox HostAddress;
        private Panel panel1;
        private Button buttonClose;
        private Button buttonReconnect;
        private ToolStripStatusLabel ConnectionStatus;
        private DataGridView KeyBindDataGrid;
        private NotifyIcon notifyIcon1;
        private DataGridViewTextBoxColumn ButtonID;
        private DataGridViewComboBoxColumn CommandType;
        private DataGridViewTextBoxColumn Argument;
    }
}