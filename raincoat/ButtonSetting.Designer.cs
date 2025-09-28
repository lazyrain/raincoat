namespace raincoat
{
    partial class ButtonSetting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboSkillType = new ComboBox();
            this.label1 = new Label();
            this.textArgument = new TextBox();
            this.label2 = new Label();
            this.textName = new TextBox();
            this.label3 = new Label();
            this.IsWindowTrigger = new CheckBox();
            this.label4 = new Label();
            this.textTriggerWindowTitle = new TextBox();
            this.SuspendLayout();
            // 
            // comboSkillType
            // 
            this.comboSkillType.FormattingEnabled = true;
            this.comboSkillType.Location = new Point(108, 41);
            this.comboSkillType.Name = "comboSkillType";
            this.comboSkillType.Size = new Size(111, 23);
            this.comboSkillType.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new Point(20, 44);
            this.label1.Name = "label1";
            this.label1.Size = new Size(41, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "コマンド";
            // 
            // textArgument
            // 
            this.textArgument.Location = new Point(108, 70);
            this.textArgument.Name = "textArgument";
            this.textArgument.ScrollBars = ScrollBars.Both;
            this.textArgument.Size = new Size(420, 23);
            this.textArgument.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new Point(20, 73);
            this.label2.Name = "label2";
            this.label2.Size = new Size(31, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "引数";
            // 
            // textName
            // 
            this.textName.Location = new Point(108, 12);
            this.textName.Name = "textName";
            this.textName.Size = new Size(231, 23);
            this.textName.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new Point(20, 15);
            this.label3.Name = "label3";
            this.label3.Size = new Size(31, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "名前";
            // 
            // IsWindowTrigger
            // 
            this.IsWindowTrigger.AutoSize = true;
            this.IsWindowTrigger.Location = new Point(108, 103);
            this.IsWindowTrigger.Name = "IsWindowTrigger";
            this.IsWindowTrigger.Size = new Size(186, 19);
            this.IsWindowTrigger.TabIndex = 3;
            this.IsWindowTrigger.Text = "アクティブウィンドウによる自動実行";
            this.IsWindowTrigger.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new Point(20, 131);
            this.label4.Name = "label4";
            this.label4.Size = new Size(85, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "ウィンドウタイトル";
            // 
            // textTriggerWindowTitle
            // 
            this.textTriggerWindowTitle.Location = new Point(108, 128);
            this.textTriggerWindowTitle.Name = "textTriggerWindowTitle";
            this.textTriggerWindowTitle.ScrollBars = ScrollBars.Both;
            this.textTriggerWindowTitle.Size = new Size(420, 23);
            this.textTriggerWindowTitle.TabIndex = 4;
            // 
            // ButtonSetting
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(541, 169);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textTriggerWindowTitle);
            this.Controls.Add(this.IsWindowTrigger);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textName);
            this.Controls.Add(this.textArgument);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboSkillType);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ButtonSetting";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "ButtonStting";
            this.FormClosed += this.ButtonSetting_FormClosed;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private ComboBox comboSkillType;
        private Label label1;
        private TextBox textArgument;
        private Label label2;
        private TextBox textName;
        private Label label3;
        private CheckBox IsWindowTrigger;
        private Label label4;
        private TextBox textTriggerWindowTitle;
    }
}