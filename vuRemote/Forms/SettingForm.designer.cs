﻿namespace vuRemote
{
    partial class SettingForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.OkButton = new System.Windows.Forms.Button();
            this.Cancelbutton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.sClientName = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.intervall = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.sOscamPort = new System.Windows.Forms.TextBox();
            this.sWebIfPort = new System.Windows.Forms.TextBox();
            this.ipAddressControl = new NullFx.IPAddressControl();
            ((System.ComponentModel.ISupportInitialize)(this.intervall)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "IP Adresse";
            // 
            // OkButton
            // 
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(80, 192);
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
            this.Cancelbutton.Location = new System.Drawing.Point(188, 192);
            this.Cancelbutton.Name = "Cancelbutton";
            this.Cancelbutton.Size = new System.Drawing.Size(75, 23);
            this.Cancelbutton.TabIndex = 3;
            this.Cancelbutton.Text = "Abbrechen";
            this.Cancelbutton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Streaming Client";
            // 
            // sClientName
            // 
            this.sClientName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sClientName.Location = new System.Drawing.Point(123, 50);
            this.sClientName.Name = "sClientName";
            this.sClientName.Size = new System.Drawing.Size(159, 20);
            this.sClientName.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button1.Location = new System.Drawing.Point(288, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(28, 22);
            this.button1.TabIndex = 7;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Refresh Intervall";
            // 
            // intervall
            // 
            this.intervall.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.intervall.Location = new System.Drawing.Point(123, 80);
            this.intervall.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.intervall.Name = "intervall";
            this.intervall.Size = new System.Drawing.Size(40, 20);
            this.intervall.TabIndex = 11;
            this.intervall.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(169, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "min";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Oscam Port";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(32, 140);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "WebIf Port";
            // 
            // sOscamPort
            // 
            this.sOscamPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sOscamPort.Location = new System.Drawing.Point(123, 110);
            this.sOscamPort.Name = "sOscamPort";
            this.sOscamPort.Size = new System.Drawing.Size(40, 20);
            this.sOscamPort.TabIndex = 16;
            // 
            // sWebIfPort
            // 
            this.sWebIfPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sWebIfPort.Location = new System.Drawing.Point(123, 140);
            this.sWebIfPort.Name = "sWebIfPort";
            this.sWebIfPort.Size = new System.Drawing.Size(40, 20);
            this.sWebIfPort.TabIndex = 17;
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
            this.ClientSize = new System.Drawing.Size(342, 243);
            this.Controls.Add(this.sWebIfPort);
            this.Controls.Add(this.sOscamPort);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ipAddressControl);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.intervall);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.sClientName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Cancelbutton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "setting";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Einstellungen";
            ((System.ComponentModel.ISupportInitialize)(this.intervall)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button Cancelbutton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox sClientName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown intervall;
        private System.Windows.Forms.Label label5;
        private NullFx.IPAddressControl ipAddressControl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox sOscamPort;
        private System.Windows.Forms.TextBox sWebIfPort;
    }
}