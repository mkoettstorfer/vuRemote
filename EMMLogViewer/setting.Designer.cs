namespace EMMLogViewer
{
    partial class setting
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(setting));
            this.label1 = new System.Windows.Forms.Label();
            this.OkButton = new System.Windows.Forms.Button();
            this.Cancelbutton = new System.Windows.Forms.Button();
            this.LogFileLabel = new System.Windows.Forms.Label();
            this.LogFileTextBox = new System.Windows.Forms.TextBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CardTextBox = new System.Windows.Forms.TextBox();
            this.SerialTextBox = new System.Windows.Forms.TextBox();
            this.UserLabel = new System.Windows.Forms.Label();
            this.PasswortLabel = new System.Windows.Forms.Label();
            this.UserTextBox = new System.Windows.Forms.TextBox();
            this.PasswortTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.maskedTextBox = new System.Windows.Forms.MaskedTextBox();
            this.gueltigLabel = new System.Windows.Forms.Label();
            this.ipAddressControl = new NullFx.IPAddressControl();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP Adresse";
            // 
            // OkButton
            // 
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(80, 317);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 2;
            this.OkButton.Text = "Ok";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // Cancelbutton
            // 
            this.Cancelbutton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancelbutton.Location = new System.Drawing.Point(188, 317);
            this.Cancelbutton.Name = "Cancelbutton";
            this.Cancelbutton.Size = new System.Drawing.Size(75, 23);
            this.Cancelbutton.TabIndex = 3;
            this.Cancelbutton.Text = "Abbrechen";
            this.Cancelbutton.UseVisualStyleBackColor = true;
            // 
            // LogFileLabel
            // 
            this.LogFileLabel.AutoSize = true;
            this.LogFileLabel.Location = new System.Drawing.Point(6, 20);
            this.LogFileLabel.Name = "LogFileLabel";
            this.LogFileLabel.Size = new System.Drawing.Size(41, 13);
            this.LogFileLabel.TabIndex = 5;
            this.LogFileLabel.Text = "LogFile";
            // 
            // LogFileTextBox
            // 
            this.LogFileTextBox.Location = new System.Drawing.Point(96, 20);
            this.LogFileTextBox.Name = "LogFileTextBox";
            this.LogFileTextBox.Size = new System.Drawing.Size(159, 20);
            this.LogFileTextBox.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Kartennr.:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Box CA Serial";
            // 
            // CardTextBox
            // 
            this.CardTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CardTextBox.Location = new System.Drawing.Point(123, 38);
            this.CardTextBox.Name = "CardTextBox";
            this.CardTextBox.Size = new System.Drawing.Size(159, 20);
            this.CardTextBox.TabIndex = 16;
            // 
            // SerialTextBox
            // 
            this.SerialTextBox.Location = new System.Drawing.Point(123, 65);
            this.SerialTextBox.Name = "SerialTextBox";
            this.SerialTextBox.Size = new System.Drawing.Size(159, 20);
            this.SerialTextBox.TabIndex = 17;
            // 
            // UserLabel
            // 
            this.UserLabel.AutoSize = true;
            this.UserLabel.Location = new System.Drawing.Point(6, 53);
            this.UserLabel.Name = "UserLabel";
            this.UserLabel.Size = new System.Drawing.Size(29, 13);
            this.UserLabel.TabIndex = 18;
            this.UserLabel.Text = "User";
            // 
            // PasswortLabel
            // 
            this.PasswortLabel.AutoSize = true;
            this.PasswortLabel.Location = new System.Drawing.Point(6, 80);
            this.PasswortLabel.Name = "PasswortLabel";
            this.PasswortLabel.Size = new System.Drawing.Size(50, 13);
            this.PasswortLabel.TabIndex = 19;
            this.PasswortLabel.Text = "Passwort";
            // 
            // UserTextBox
            // 
            this.UserTextBox.Location = new System.Drawing.Point(96, 46);
            this.UserTextBox.Name = "UserTextBox";
            this.UserTextBox.Size = new System.Drawing.Size(159, 20);
            this.UserTextBox.TabIndex = 20;
            // 
            // PasswortTextBox
            // 
            this.PasswortTextBox.Location = new System.Drawing.Point(96, 73);
            this.PasswortTextBox.Name = "PasswortTextBox";
            this.PasswortTextBox.Size = new System.Drawing.Size(159, 20);
            this.PasswortTextBox.TabIndex = 21;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.PasswortLabel);
            this.groupBox1.Controls.Add(this.PasswortTextBox);
            this.groupBox1.Controls.Add(this.LogFileLabel);
            this.groupBox1.Controls.Add(this.UserTextBox);
            this.groupBox1.Controls.Add(this.LogFileTextBox);
            this.groupBox1.Controls.Add(this.UserLabel);
            this.groupBox1.Location = new System.Drawing.Point(27, 103);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 107);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FTP Daten";
            // 
            // maskedTextBox
            // 
            this.maskedTextBox.AsciiOnly = true;
            this.maskedTextBox.Location = new System.Drawing.Point(123, 217);
            this.maskedTextBox.Mask = "99/99/9999";
            this.maskedTextBox.Name = "maskedTextBox";
            this.maskedTextBox.Size = new System.Drawing.Size(159, 20);
            this.maskedTextBox.TabIndex = 23;
            this.maskedTextBox.TypeValidationCompleted += new System.Windows.Forms.TypeValidationEventHandler(this.maskedTextBox_TypeValidationCompleted);
            // 
            // gueltigLabel
            // 
            this.gueltigLabel.AutoSize = true;
            this.gueltigLabel.Location = new System.Drawing.Point(36, 223);
            this.gueltigLabel.Name = "gueltigLabel";
            this.gueltigLabel.Size = new System.Drawing.Size(74, 13);
            this.gueltigLabel.TabIndex = 24;
            this.gueltigLabel.Text = "Karte aktiv bis";
            // 
            // ipAddressControl
            // 
            this.ipAddressControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ipAddressControl.Location = new System.Drawing.Point(123, 11);
            this.ipAddressControl.Name = "ipAddressControl";
            this.ipAddressControl.Size = new System.Drawing.Size(100, 20);
            this.ipAddressControl.TabIndex = 13;
            this.ipAddressControl.Text = "0.0.0.0";
            // 
            // setting
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.Cancelbutton;
            this.ClientSize = new System.Drawing.Size(342, 381);
            this.Controls.Add(this.gueltigLabel);
            this.Controls.Add(this.maskedTextBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.SerialTextBox);
            this.Controls.Add(this.CardTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ipAddressControl);
            this.Controls.Add(this.Cancelbutton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "setting";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Einstellungen";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button Cancelbutton;
        private System.Windows.Forms.Label LogFileLabel;
        private System.Windows.Forms.TextBox LogFileTextBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private NullFx.IPAddressControl ipAddressControl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox CardTextBox;
        private System.Windows.Forms.TextBox SerialTextBox;
        private System.Windows.Forms.Label UserLabel;
        private System.Windows.Forms.Label PasswortLabel;
        private System.Windows.Forms.TextBox UserTextBox;
        private System.Windows.Forms.TextBox PasswortTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MaskedTextBox maskedTextBox;
        private System.Windows.Forms.Label gueltigLabel;
    }
}