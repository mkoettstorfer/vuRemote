namespace vuRemote
{
    partial class ProgrammList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgrammList));
            this.listView1 = new System.Windows.Forms.ListView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ePGEinträgeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ePGSucheToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerHinzufügenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.tvInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.googleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iMDbToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.streamAnsehenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.audioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.audioMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.channelImageList = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.audioLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.serviceBox = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.prevButton = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.timeBox = new System.Windows.Forms.TextBox();
            this.nextButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.audioTimer = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.ContextMenuStrip = this.contextMenuStrip;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.OwnerDraw = true;
            this.listView1.Size = new System.Drawing.Size(678, 531);
            this.listView1.SmallImageList = this.channelImageList;
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.listView1_DrawColumnHeader);
            this.listView1.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.listView1_DrawSubItem);
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.OnSelectedIndexChange);
            this.listView1.DoubleClick += new System.EventHandler(this.ChangeChannelClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.ePGEinträgeToolStripMenuItem,
            this.ePGSucheToolStripMenuItem,
            this.timerHinzufügenToolStripMenuItem,
            this.toolStripMenuItem2,
            this.tvInfoToolStripMenuItem,
            this.googleToolStripMenuItem,
            this.iMDbToolStripMenuItem,
            this.toolStripMenuItem3,
            this.streamAnsehenToolStripMenuItem,
            this.audioToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(169, 214);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = global::vuRemote.Properties.Resources.move;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(168, 22);
            this.toolStripMenuItem1.Text = "Sender wechslen";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.ChangeChannelClick);
            // 
            // ePGEinträgeToolStripMenuItem
            // 
            this.ePGEinträgeToolStripMenuItem.Image = global::vuRemote.Properties.Resources.list;
            this.ePGEinträgeToolStripMenuItem.Name = "ePGEinträgeToolStripMenuItem";
            this.ePGEinträgeToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.ePGEinträgeToolStripMenuItem.Text = "EPG Einträge";
            this.ePGEinträgeToolStripMenuItem.Click += new System.EventHandler(this.OnEPGClick);
            // 
            // ePGSucheToolStripMenuItem
            // 
            this.ePGSucheToolStripMenuItem.Name = "ePGSucheToolStripMenuItem";
            this.ePGSucheToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.ePGSucheToolStripMenuItem.Text = "EPG Suche";
            this.ePGSucheToolStripMenuItem.Click += new System.EventHandler(this.ePGSucheToolStripMenuItem_Click);
            // 
            // timerHinzufügenToolStripMenuItem
            // 
            this.timerHinzufügenToolStripMenuItem.Enabled = false;
            this.timerHinzufügenToolStripMenuItem.Image = global::vuRemote.Properties.Resources.watch;
            this.timerHinzufügenToolStripMenuItem.Name = "timerHinzufügenToolStripMenuItem";
            this.timerHinzufügenToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.timerHinzufügenToolStripMenuItem.Text = "Timer hinzufügen";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(165, 6);
            // 
            // tvInfoToolStripMenuItem
            // 
            this.tvInfoToolStripMenuItem.Name = "tvInfoToolStripMenuItem";
            this.tvInfoToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.tvInfoToolStripMenuItem.Text = "Tv Info";
            this.tvInfoToolStripMenuItem.Click += new System.EventHandler(this.tvInfoToolStripMenuItem_Click);
            // 
            // googleToolStripMenuItem
            // 
            this.googleToolStripMenuItem.Image = global::vuRemote.Properties.Resources.google;
            this.googleToolStripMenuItem.Name = "googleToolStripMenuItem";
            this.googleToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.googleToolStripMenuItem.Text = "Google";
            this.googleToolStripMenuItem.Click += new System.EventHandler(this.OnGoogleClick);
            // 
            // iMDbToolStripMenuItem
            // 
            this.iMDbToolStripMenuItem.Image = global::vuRemote.Properties.Resources.imdb;
            this.iMDbToolStripMenuItem.Name = "iMDbToolStripMenuItem";
            this.iMDbToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.iMDbToolStripMenuItem.Text = "IMDb";
            this.iMDbToolStripMenuItem.Click += new System.EventHandler(this.iMDbToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(165, 6);
            // 
            // streamAnsehenToolStripMenuItem
            // 
            this.streamAnsehenToolStripMenuItem.Image = global::vuRemote.Properties.Resources.play;
            this.streamAnsehenToolStripMenuItem.Name = "streamAnsehenToolStripMenuItem";
            this.streamAnsehenToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.streamAnsehenToolStripMenuItem.Text = "Stream ansehen";
            this.streamAnsehenToolStripMenuItem.Click += new System.EventHandler(this.streamAnsehenToolStripMenuItem_Click);
            // 
            // audioToolStripMenuItem
            // 
            this.audioToolStripMenuItem.DropDown = this.audioMenuStrip;
            this.audioToolStripMenuItem.Name = "audioToolStripMenuItem";
            this.audioToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.audioToolStripMenuItem.Text = "Audio";
            // 
            // audioMenuStrip
            // 
            this.audioMenuStrip.Name = "audioMenuStrip";
            this.audioMenuStrip.OwnerItem = this.audioToolStripMenuItem;
            this.audioMenuStrip.Size = new System.Drawing.Size(61, 4);
            // 
            // channelImageList
            // 
            this.channelImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.channelImageList.ImageSize = new System.Drawing.Size(40, 21);
            this.channelImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.audioLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 694);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(678, 22);
            this.statusStrip.Stretch = false;
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.AutoSize = false;
            this.toolStripStatusLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(200, 17);
            this.toolStripStatusLabel.Text = "Kanal:";
            this.toolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripStatusLabel.Click += new System.EventHandler(this.toolStripStatusLabel_Click);
            // 
            // audioLabel
            // 
            this.audioLabel.Name = "audioLabel";
            this.audioLabel.Size = new System.Drawing.Size(45, 17);
            this.audioLabel.Text = "Audio: ";
            // 
            // richTextBox
            // 
            this.richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox.Location = new System.Drawing.Point(0, 0);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(678, 126);
            this.richTextBox.TabIndex = 2;
            this.richTextBox.Text = "";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.richTextBox);
            this.splitContainer1.Size = new System.Drawing.Size(678, 661);
            this.splitContainer1.SplitterDistance = 531;
            this.splitContainer1.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.serviceBox);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(678, 33);
            this.panel1.TabIndex = 1;
            // 
            // serviceBox
            // 
            this.serviceBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.serviceBox.FormattingEnabled = true;
            this.serviceBox.Location = new System.Drawing.Point(3, 4);
            this.serviceBox.Name = "serviceBox";
            this.serviceBox.Size = new System.Drawing.Size(232, 21);
            this.serviceBox.TabIndex = 5;
            this.serviceBox.SelectedIndexChanged += new System.EventHandler(this.serviceBox_SelectedIndexChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.prevButton);
            this.panel3.Controls.Add(this.timeBox);
            this.panel3.Controls.Add(this.nextButton);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(474, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(202, 31);
            this.panel3.TabIndex = 4;
            // 
            // prevButton
            // 
            this.prevButton.ImageKey = "1390070508_173945.ico";
            this.prevButton.ImageList = this.imageList1;
            this.prevButton.Location = new System.Drawing.Point(75, 3);
            this.prevButton.Name = "prevButton";
            this.prevButton.Size = new System.Drawing.Size(23, 23);
            this.prevButton.TabIndex = 0;
            this.prevButton.UseVisualStyleBackColor = true;
            this.prevButton.Click += new System.EventHandler(this.OnDownButtonClick);
            this.prevButton.MouseHover += new System.EventHandler(this.prevButton_MouseHover);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "1390070484_173949.ico");
            this.imageList1.Images.SetKeyName(1, "1390070508_173945.ico");
            this.imageList1.Images.SetKeyName(2, "1390070486_173956.ico");
            this.imageList1.Images.SetKeyName(3, "google.png");
            this.imageList1.Images.SetKeyName(4, "play.png");
            // 
            // timeBox
            // 
            this.timeBox.Enabled = false;
            this.timeBox.Location = new System.Drawing.Point(103, 4);
            this.timeBox.Name = "timeBox";
            this.timeBox.Size = new System.Drawing.Size(60, 20);
            this.timeBox.TabIndex = 2;
            // 
            // nextButton
            // 
            this.nextButton.ImageKey = "1390070484_173949.ico";
            this.nextButton.ImageList = this.imageList1;
            this.nextButton.Location = new System.Drawing.Point(169, 3);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(23, 23);
            this.nextButton.TabIndex = 1;
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.OnUpButtonClick);
            this.nextButton.MouseHover += new System.EventHandler(this.upButton_MouseHover);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.splitContainer1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 33);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(678, 661);
            this.panel2.TabIndex = 4;
            // 
            // refreshTimer
            // 
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // updateTimer
            // 
            this.updateTimer.Interval = 30000;
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // audioTimer
            // 
            this.audioTimer.Interval = 300;
            this.audioTimer.Tick += new System.EventHandler(this.audioTimer_Tick);
            // 
            // ProgrammList
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(678, 716);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip);
            this.Name = "ProgrammList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Senderliste";
            this.contextMenuStrip.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem ePGEinträgeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem timerHinzufügenToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button prevButton;
        private System.Windows.Forms.TextBox timeBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ImageList channelImageList;
        private System.Windows.Forms.ToolStripMenuItem googleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem streamAnsehenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tvInfoToolStripMenuItem;
        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem iMDbToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ComboBox serviceBox;
        private System.Windows.Forms.ToolStripMenuItem ePGSucheToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel audioLabel;
        private System.Windows.Forms.ToolStripMenuItem audioToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip audioMenuStrip;
        private System.Windows.Forms.Timer audioTimer;

    }
}