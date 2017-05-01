namespace vuRemote
{
    partial class ArchivList
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.streamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.löschenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ArchiveListView = new System.Windows.Forms.ListView();
            this.textBox = new System.Windows.Forms.RichTextBox();
            this.umbenennenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.streamToolStripMenuItem,
            this.löschenToolStripMenuItem,
            this.downloadToolStripMenuItem,
            this.umbenennenToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 114);
            // 
            // streamToolStripMenuItem
            // 
            this.streamToolStripMenuItem.Image = global::vuRemote.Properties.Resources.play;
            this.streamToolStripMenuItem.Name = "streamToolStripMenuItem";
            this.streamToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.streamToolStripMenuItem.Text = "Stream";
            this.streamToolStripMenuItem.Click += new System.EventHandler(this.streamToolStripMenuItem_Click);
            // 
            // löschenToolStripMenuItem
            // 
            this.löschenToolStripMenuItem.Image = global::vuRemote.Properties.Resources.DeleteRed;
            this.löschenToolStripMenuItem.Name = "löschenToolStripMenuItem";
            this.löschenToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.löschenToolStripMenuItem.Text = "löschen";
            this.löschenToolStripMenuItem.Click += new System.EventHandler(this.löschenToolStripMenuItem_Click);
            // 
            // downloadToolStripMenuItem
            // 
            this.downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            this.downloadToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.downloadToolStripMenuItem.Text = "herunterladen";
            this.downloadToolStripMenuItem.Click += new System.EventHandler(this.downloadToolStripMenuItem_Click);
            // 
            // ArchiveListView
            // 
            this.ArchiveListView.AutoArrange = false;
            this.ArchiveListView.ContextMenuStrip = this.contextMenuStrip1;
            this.ArchiveListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ArchiveListView.FullRowSelect = true;
            this.ArchiveListView.GridLines = true;
            this.ArchiveListView.Location = new System.Drawing.Point(0, 0);
            this.ArchiveListView.MultiSelect = false;
            this.ArchiveListView.Name = "ArchiveListView";
            this.ArchiveListView.ShowGroups = false;
            this.ArchiveListView.Size = new System.Drawing.Size(790, 443);
            this.ArchiveListView.TabIndex = 0;
            this.ArchiveListView.UseCompatibleStateImageBehavior = false;
            this.ArchiveListView.View = System.Windows.Forms.View.Details;
            this.ArchiveListView.SelectedIndexChanged += new System.EventHandler(this.ArchiveListView_SelectedIndexChanged);
            // 
            // textBox
            // 
            this.textBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox.Location = new System.Drawing.Point(0, 443);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(790, 96);
            this.textBox.TabIndex = 1;
            this.textBox.Text = "";
            // 
            // umbenennenToolStripMenuItem
            // 
            this.umbenennenToolStripMenuItem.Name = "umbenennenToolStripMenuItem";
            this.umbenennenToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.umbenennenToolStripMenuItem.Text = "umbenennen";
            this.umbenennenToolStripMenuItem.Click += new System.EventHandler(this.umbenennenToolStripMenuItem_Click);
            // 
            // ArchivList
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(790, 539);
            this.Controls.Add(this.ArchiveListView);
            this.Controls.Add(this.textBox);
            this.Name = "ArchivList";
            this.Text = "Archiv";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ListView ArchiveListView;
        private System.Windows.Forms.ToolStripMenuItem streamToolStripMenuItem;
        private System.Windows.Forms.RichTextBox textBox;
        private System.Windows.Forms.ToolStripMenuItem löschenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem umbenennenToolStripMenuItem;
    }
}