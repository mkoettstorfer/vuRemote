namespace vuRemote
{
    partial class TimerList
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
            this.components = new System.ComponentModel.Container();
            this.TimerListView = new System.Windows.Forms.ListView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.timerLöschenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.channelImageList = new System.Windows.Forms.ImageList(this.components);
            this.textBox = new System.Windows.Forms.RichTextBox();
            this.timerDeaktivierenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // TimerListView
            // 
            this.TimerListView.AutoArrange = false;
            this.TimerListView.ContextMenuStrip = this.contextMenuStrip;
            this.TimerListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TimerListView.FullRowSelect = true;
            this.TimerListView.Location = new System.Drawing.Point(0, 0);
            this.TimerListView.MultiSelect = false;
            this.TimerListView.Name = "TimerListView";
            this.TimerListView.ShowGroups = false;
            this.TimerListView.Size = new System.Drawing.Size(603, 364);
            this.TimerListView.SmallImageList = this.channelImageList;
            this.TimerListView.TabIndex = 0;
            this.TimerListView.UseCompatibleStateImageBehavior = false;
            this.TimerListView.View = System.Windows.Forms.View.Details;
            this.TimerListView.SelectedIndexChanged += new System.EventHandler(this.TimerListView_SelectedIndexChanged);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.timerLöschenToolStripMenuItem,
            this.timerDeaktivierenToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip1";
            this.contextMenuStrip.Size = new System.Drawing.Size(229, 70);
            // 
            // timerLöschenToolStripMenuItem
            // 
            this.timerLöschenToolStripMenuItem.Name = "timerLöschenToolStripMenuItem";
            this.timerLöschenToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.timerLöschenToolStripMenuItem.Text = "Timer löschen";
            this.timerLöschenToolStripMenuItem.Click += new System.EventHandler(this.OnTimerDelete);
            // 
            // channelImageList
            // 
            this.channelImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.channelImageList.ImageSize = new System.Drawing.Size(40, 21);
            this.channelImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // textBox
            // 
            this.textBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox.Location = new System.Drawing.Point(0, 364);
            this.textBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(603, 79);
            this.textBox.TabIndex = 1;
            this.textBox.Text = "";
            // 
            // timerDeaktivierenToolStripMenuItem
            // 
            this.timerDeaktivierenToolStripMenuItem.Name = "timerDeaktivierenToolStripMenuItem";
            this.timerDeaktivierenToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.timerDeaktivierenToolStripMenuItem.Text = "Timer aktivieren/deaktivieren";
            this.timerDeaktivierenToolStripMenuItem.Click += new System.EventHandler(this.timerDeaktivierenToolStripMenuItem_Click);
            // 
            // TimerList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 443);
            this.Controls.Add(this.TimerListView);
            this.Controls.Add(this.textBox);
            this.Name = "TimerList";
            this.Text = "Aufnahmeplan";
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView TimerListView;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem timerLöschenToolStripMenuItem;
        private System.Windows.Forms.ImageList channelImageList;
        private System.Windows.Forms.RichTextBox textBox;
        private System.Windows.Forms.ToolStripMenuItem timerDeaktivierenToolStripMenuItem;
    }
}