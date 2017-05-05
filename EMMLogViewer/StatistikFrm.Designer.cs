namespace EMMLogViewer
{
    partial class StatistikFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatistikFrm));
            this.labelCount = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.labelStart = new System.Windows.Forms.Label();
            this.labelEnde = new System.Windows.Forms.Label();
            this.labelMedian = new System.Windows.Forms.Label();
            this.labelMittel = new System.Windows.Forms.Label();
            this.labelShort = new System.Windows.Forms.Label();
            this.labelLong = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(28, 26);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(45, 13);
            this.labelCount.TabIndex = 0;
            this.labelCount.Text = "Anzahl: ";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(123, 226);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelStart
            // 
            this.labelStart.AutoSize = true;
            this.labelStart.Location = new System.Drawing.Point(28, 54);
            this.labelStart.Name = "labelStart";
            this.labelStart.Size = new System.Drawing.Size(66, 13);
            this.labelStart.TabIndex = 2;
            this.labelStart.Text = "Von Datum: ";
            // 
            // labelEnde
            // 
            this.labelEnde.AutoSize = true;
            this.labelEnde.Location = new System.Drawing.Point(28, 82);
            this.labelEnde.Name = "labelEnde";
            this.labelEnde.Size = new System.Drawing.Size(58, 13);
            this.labelEnde.TabIndex = 3;
            this.labelEnde.Text = "Bis Datum:";
            // 
            // labelMedian
            // 
            this.labelMedian.AutoSize = true;
            this.labelMedian.Location = new System.Drawing.Point(28, 110);
            this.labelMedian.Name = "labelMedian";
            this.labelMedian.Size = new System.Drawing.Size(124, 13);
            this.labelMedian.TabIndex = 4;
            this.labelMedian.Text = "Signalabstand (Median): ";
            // 
            // labelMittel
            // 
            this.labelMittel.AutoSize = true;
            this.labelMittel.Location = new System.Drawing.Point(28, 138);
            this.labelMittel.Name = "labelMittel";
            this.labelMittel.Size = new System.Drawing.Size(111, 13);
            this.labelMittel.TabIndex = 5;
            this.labelMittel.Text = "Signalabstand (Mittel):";
            // 
            // labelShort
            // 
            this.labelShort.AutoSize = true;
            this.labelShort.Location = new System.Drawing.Point(28, 166);
            this.labelShort.Name = "labelShort";
            this.labelShort.Size = new System.Drawing.Size(106, 13);
            this.labelShort.TabIndex = 6;
            this.labelShort.Text = "Signalabstand (kurz):";
            // 
            // labelLong
            // 
            this.labelLong.AutoSize = true;
            this.labelLong.Location = new System.Drawing.Point(28, 194);
            this.labelLong.Name = "labelLong";
            this.labelLong.Size = new System.Drawing.Size(106, 13);
            this.labelLong.TabIndex = 7;
            this.labelLong.Text = "Signalabstand (lang):";
            // 
            // StatistikFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 270);
            this.Controls.Add(this.labelLong);
            this.Controls.Add(this.labelShort);
            this.Controls.Add(this.labelMittel);
            this.Controls.Add(this.labelMedian);
            this.Controls.Add(this.labelEnde);
            this.Controls.Add(this.labelStart);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelCount);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StatistikFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "EMM Statistik";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelStart;
        private System.Windows.Forms.Label labelEnde;
        private System.Windows.Forms.Label labelMedian;
        private System.Windows.Forms.Label labelMittel;
        private System.Windows.Forms.Label labelShort;
        private System.Windows.Forms.Label labelLong;
    }
}