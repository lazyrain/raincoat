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
            this.SuspendLayout();
            // 
            // comboSkillType
            // 
            this.comboSkillType.FormattingEnabled = true;
            this.comboSkillType.Location = new Point(67, 41);
            this.comboSkillType.Name = "comboSkillType";
            this.comboSkillType.Size = new Size(121, 23);
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
            this.textArgument.Location = new Point(67, 70);
            this.textArgument.Name = "textArgument";
            this.textArgument.ScrollBars = ScrollBars.Both;
            this.textArgument.Size = new Size(430, 23);
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
            this.textName.Location = new Point(67, 12);
            this.textName.Name = "textName";
            this.textName.Size = new Size(241, 23);
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
            // ButtonSetting
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(509, 106);
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
    }
}