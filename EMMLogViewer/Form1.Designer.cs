namespace EMMLogViewer
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logFileÖffnenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logViaFTPÖffnenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.einstellungenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.überToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topPanel = new System.Windows.Forms.Panel();
            this.EMMCALabel = new System.Windows.Forms.Label();
            this.EMMCardLabel = new System.Windows.Forms.Label();
            this.SerialTextBox = new System.Windows.Forms.TextBox();
            this.CardTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.TextBox = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.analyseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.kopierenlängenbegrenztToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kopierenverixtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.statistikToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dateCheckBox = new System.Windows.Forms.CheckBox();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.SearchTextBox = new System.Windows.Forms.TextBox();
            this.SearchButton = new System.Windows.Forms.Button();
            this.doubleCheckBox = new System.Windows.Forms.CheckBox();
            this.KeyCheckBox = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.LengthRichTextBox = new System.Windows.Forms.RichTextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.PaarListView = new System.Windows.Forms.ListView();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.inDieZwischenablageKopierenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.singleEMMInDieZwischenablageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ColorCheckBox = new System.Windows.Forms.CheckBox();
            this.analyzeButton = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.CountStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.TypeStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.TimeStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.LogTimeStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.topPanel.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1042, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logFileÖffnenToolStripMenuItem,
            this.logViaFTPÖffnenToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.fileToolStripMenuItem.Text = "Datei";
            // 
            // logFileÖffnenToolStripMenuItem
            // 
            this.logFileÖffnenToolStripMenuItem.Name = "logFileÖffnenToolStripMenuItem";
            this.logFileÖffnenToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.logFileÖffnenToolStripMenuItem.Text = "Log File öffnen";
            this.logFileÖffnenToolStripMenuItem.Click += new System.EventHandler(this.logFileÖffnenToolStripMenuItem_Click);
            // 
            // logViaFTPÖffnenToolStripMenuItem
            // 
            this.logViaFTPÖffnenToolStripMenuItem.Name = "logViaFTPÖffnenToolStripMenuItem";
            this.logViaFTPÖffnenToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.logViaFTPÖffnenToolStripMenuItem.Text = "Log via FTP öffnen";
            this.logViaFTPÖffnenToolStripMenuItem.Click += new System.EventHandler(this.logViaFTPÖffnenToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.exitToolStripMenuItem.Text = "Be&enden";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.einstellungenToolStripMenuItem,
            this.überToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Hilfe";
            // 
            // einstellungenToolStripMenuItem
            // 
            this.einstellungenToolStripMenuItem.Name = "einstellungenToolStripMenuItem";
            this.einstellungenToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.einstellungenToolStripMenuItem.Text = "Einstellungen";
            this.einstellungenToolStripMenuItem.Click += new System.EventHandler(this.einstellungenToolStripMenuItem_Click);
            // 
            // überToolStripMenuItem
            // 
            this.überToolStripMenuItem.Name = "überToolStripMenuItem";
            this.überToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.überToolStripMenuItem.Text = "Über";
            this.überToolStripMenuItem.Click += new System.EventHandler(this.überToolStripMenuItem_Click);
            // 
            // topPanel
            // 
            this.topPanel.Controls.Add(this.EMMCALabel);
            this.topPanel.Controls.Add(this.EMMCardLabel);
            this.topPanel.Controls.Add(this.SerialTextBox);
            this.topPanel.Controls.Add(this.CardTextBox);
            this.topPanel.Controls.Add(this.label4);
            this.topPanel.Controls.Add(this.label3);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 24);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(1042, 77);
            this.topPanel.TabIndex = 1;
            // 
            // EMMCALabel
            // 
            this.EMMCALabel.AutoSize = true;
            this.EMMCALabel.Location = new System.Drawing.Point(267, 42);
            this.EMMCALabel.Name = "EMMCALabel";
            this.EMMCALabel.Size = new System.Drawing.Size(100, 13);
            this.EMMCALabel.TabIndex = 23;
            this.EMMCALabel.Text = "--> EMM Hex Code:";
            // 
            // EMMCardLabel
            // 
            this.EMMCardLabel.AutoSize = true;
            this.EMMCardLabel.Location = new System.Drawing.Point(267, 15);
            this.EMMCardLabel.Name = "EMMCardLabel";
            this.EMMCardLabel.Size = new System.Drawing.Size(100, 13);
            this.EMMCardLabel.TabIndex = 22;
            this.EMMCardLabel.Text = "--> EMM Hex Code:";
            // 
            // SerialTextBox
            // 
            this.SerialTextBox.Enabled = false;
            this.SerialTextBox.Location = new System.Drawing.Point(102, 39);
            this.SerialTextBox.Name = "SerialTextBox";
            this.SerialTextBox.Size = new System.Drawing.Size(159, 20);
            this.SerialTextBox.TabIndex = 21;
            // 
            // CardTextBox
            // 
            this.CardTextBox.Enabled = false;
            this.CardTextBox.Location = new System.Drawing.Point(102, 12);
            this.CardTextBox.Name = "CardTextBox";
            this.CardTextBox.Size = new System.Drawing.Size(159, 20);
            this.CardTextBox.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Box CA Serial";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Kartennr.:";
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 101);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1042, 431);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.TextBox);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1034, 402);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Logfile";
            // 
            // TextBox
            // 
            this.TextBox.CausesValidation = false;
            this.TextBox.DetectUrls = false;
            this.TextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBox.Location = new System.Drawing.Point(3, 3);
            this.TextBox.Name = "TextBox";
            this.TextBox.ReadOnly = true;
            this.TextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.TextBox.Size = new System.Drawing.Size(1028, 396);
            this.TextBox.TabIndex = 0;
            this.TextBox.Text = "";
            this.TextBox.WordWrap = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.listView1);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1034, 402);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Analyse";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.SystemColors.Window;
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(3, 39);
            this.listView1.Name = "listView1";
            this.listView1.ShowItemToolTips = true;
            this.listView1.Size = new System.Drawing.Size(1028, 360);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.analyseToolStripMenuItem,
            this.toolStripMenuItem2,
            this.toolStripMenuItem1,
            this.kopierenlängenbegrenztToolStripMenuItem,
            this.kopierenverixtToolStripMenuItem,
            this.toolStripMenuItem3,
            this.statistikToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(241, 148);
            // 
            // analyseToolStripMenuItem
            // 
            this.analyseToolStripMenuItem.Name = "analyseToolStripMenuItem";
            this.analyseToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.analyseToolStripMenuItem.Text = "Details";
            this.analyseToolStripMenuItem.Click += new System.EventHandler(this.analyseToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(237, 6);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(240, 22);
            this.toolStripMenuItem1.Text = "In die Zwischenablage kopieren";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // kopierenlängenbegrenztToolStripMenuItem
            // 
            this.kopierenlängenbegrenztToolStripMenuItem.Name = "kopierenlängenbegrenztToolStripMenuItem";
            this.kopierenlängenbegrenztToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.kopierenlängenbegrenztToolStripMenuItem.Text = "kopieren (längenbegrenzt)";
            this.kopierenlängenbegrenztToolStripMenuItem.Click += new System.EventHandler(this.kopierenlängenbegrenztToolStripMenuItem_Click);
            // 
            // kopierenverixtToolStripMenuItem
            // 
            this.kopierenverixtToolStripMenuItem.Name = "kopierenverixtToolStripMenuItem";
            this.kopierenverixtToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.kopierenverixtToolStripMenuItem.Text = "kopieren (xxxxxxxx)";
            this.kopierenverixtToolStripMenuItem.Click += new System.EventHandler(this.kopierenverixtToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(237, 6);
            // 
            // statistikToolStripMenuItem
            // 
            this.statistikToolStripMenuItem.Name = "statistikToolStripMenuItem";
            this.statistikToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.statistikToolStripMenuItem.Text = "Statistik";
            this.statistikToolStripMenuItem.Click += new System.EventHandler(this.statistikToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dateCheckBox);
            this.panel1.Controls.Add(this.dateTimePicker);
            this.panel1.Controls.Add(this.SearchTextBox);
            this.panel1.Controls.Add(this.SearchButton);
            this.panel1.Controls.Add(this.doubleCheckBox);
            this.panel1.Controls.Add(this.KeyCheckBox);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1028, 36);
            this.panel1.TabIndex = 1;
            // 
            // dateCheckBox
            // 
            this.dateCheckBox.AutoSize = true;
            this.dateCheckBox.Location = new System.Drawing.Point(402, 10);
            this.dateCheckBox.Name = "dateCheckBox";
            this.dateCheckBox.Size = new System.Drawing.Size(41, 17);
            this.dateCheckBox.TabIndex = 6;
            this.dateCheckBox.Text = "ab:";
            this.dateCheckBox.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(446, 8);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(199, 20);
            this.dateTimePicker.TabIndex = 5;
            // 
            // SearchTextBox
            // 
            this.SearchTextBox.Location = new System.Drawing.Point(683, 8);
            this.SearchTextBox.Name = "SearchTextBox";
            this.SearchTextBox.Size = new System.Drawing.Size(185, 20);
            this.SearchTextBox.TabIndex = 4;
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(874, 6);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 23);
            this.SearchButton.TabIndex = 3;
            this.SearchButton.Text = "Suchen";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // doubleCheckBox
            // 
            this.doubleCheckBox.AutoSize = true;
            this.doubleCheckBox.Location = new System.Drawing.Point(229, 10);
            this.doubleCheckBox.Name = "doubleCheckBox";
            this.doubleCheckBox.Size = new System.Drawing.Size(158, 17);
            this.doubleCheckBox.TabIndex = 2;
            this.doubleCheckBox.Text = "doppelte Einträge ignorieren";
            this.doubleCheckBox.UseVisualStyleBackColor = true;
            // 
            // KeyCheckBox
            // 
            this.KeyCheckBox.AutoSize = true;
            this.KeyCheckBox.Location = new System.Drawing.Point(109, 10);
            this.KeyCheckBox.Name = "KeyCheckBox";
            this.KeyCheckBox.Size = new System.Drawing.Size(114, 17);
            this.KeyCheckBox.TabIndex = 1;
            this.KeyCheckBox.Text = "Key anonymisieren";
            this.KeyCheckBox.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Analyse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.LengthRichTextBox);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1034, 402);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Längen";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // LengthRichTextBox
            // 
            this.LengthRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LengthRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.LengthRichTextBox.Name = "LengthRichTextBox";
            this.LengthRichTextBox.ReadOnly = true;
            this.LengthRichTextBox.Size = new System.Drawing.Size(1034, 402);
            this.LengthRichTextBox.TabIndex = 0;
            this.LengthRichTextBox.Text = "";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.PaarListView);
            this.tabPage4.Controls.Add(this.panel2);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1034, 402);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Pärchen (Beta)";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // PaarListView
            // 
            this.PaarListView.ContextMenuStrip = this.contextMenuStrip2;
            this.PaarListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PaarListView.FullRowSelect = true;
            this.PaarListView.Location = new System.Drawing.Point(3, 39);
            this.PaarListView.MultiSelect = false;
            this.PaarListView.Name = "PaarListView";
            this.PaarListView.Size = new System.Drawing.Size(1028, 360);
            this.PaarListView.TabIndex = 1;
            this.PaarListView.UseCompatibleStateImageBehavior = false;
            this.PaarListView.View = System.Windows.Forms.View.Details;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inDieZwischenablageKopierenToolStripMenuItem,
            this.singleEMMInDieZwischenablageToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(267, 48);
            // 
            // inDieZwischenablageKopierenToolStripMenuItem
            // 
            this.inDieZwischenablageKopierenToolStripMenuItem.Name = "inDieZwischenablageKopierenToolStripMenuItem";
            this.inDieZwischenablageKopierenToolStripMenuItem.Size = new System.Drawing.Size(266, 22);
            this.inDieZwischenablageKopierenToolStripMenuItem.Text = "alles in die Zwischenablage kopieren";
            this.inDieZwischenablageKopierenToolStripMenuItem.Click += new System.EventHandler(this.inDieZwischenablageKopierenToolStripMenuItem_Click);
            // 
            // singleEMMInDieZwischenablageToolStripMenuItem
            // 
            this.singleEMMInDieZwischenablageToolStripMenuItem.Name = "singleEMMInDieZwischenablageToolStripMenuItem";
            this.singleEMMInDieZwischenablageToolStripMenuItem.Size = new System.Drawing.Size(266, 22);
            this.singleEMMInDieZwischenablageToolStripMenuItem.Text = "Single EMM kopieren";
            this.singleEMMInDieZwischenablageToolStripMenuItem.Click += new System.EventHandler(this.singleEMMInDieZwischenablageToolStripMenuItem_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ColorCheckBox);
            this.panel2.Controls.Add(this.analyzeButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1028, 36);
            this.panel2.TabIndex = 0;
            // 
            // ColorCheckBox
            // 
            this.ColorCheckBox.AutoSize = true;
            this.ColorCheckBox.Location = new System.Drawing.Point(109, 10);
            this.ColorCheckBox.Name = "ColorCheckBox";
            this.ColorCheckBox.Size = new System.Drawing.Size(135, 17);
            this.ColorCheckBox.TabIndex = 1;
            this.ColorCheckBox.Text = "gleiche EMM einfärben";
            this.ColorCheckBox.UseVisualStyleBackColor = true;
            // 
            // analyzeButton
            // 
            this.analyzeButton.Location = new System.Drawing.Point(8, 6);
            this.analyzeButton.Name = "analyzeButton";
            this.analyzeButton.Size = new System.Drawing.Size(75, 23);
            this.analyzeButton.TabIndex = 0;
            this.analyzeButton.Text = "Analyse";
            this.analyzeButton.UseVisualStyleBackColor = true;
            this.analyzeButton.Click += new System.EventHandler(this.analyzeButton_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CountStripLabel,
            this.TypeStripLabel,
            this.TimeStripLabel,
            this.LogTimeStripLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 532);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1042, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // CountStripLabel
            // 
            this.CountStripLabel.Name = "CountStripLabel";
            this.CountStripLabel.Size = new System.Drawing.Size(66, 17);
            this.CountStripLabel.Text = "Einträge: --";
            // 
            // TypeStripLabel
            // 
            this.TypeStripLabel.Name = "TypeStripLabel";
            this.TypeStripLabel.Size = new System.Drawing.Size(43, 17);
            this.TypeStripLabel.Text = "Typ: --";
            // 
            // TimeStripLabel
            // 
            this.TimeStripLabel.Name = "TimeStripLabel";
            this.TimeStripLabel.Size = new System.Drawing.Size(54, 17);
            this.TimeStripLabel.Text = "Dauer: --";
            // 
            // LogTimeStripLabel
            // 
            this.LogTimeStripLabel.Name = "LogTimeStripLabel";
            this.LogTimeStripLabel.Size = new System.Drawing.Size(74, 17);
            this.LogTimeStripLabel.Text = "LogDauer: --";
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1042, 554);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "EMM Logfile Viewer";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem logFileÖffnenToolStripMenuItem;
        private System.Windows.Forms.RichTextBox TextBox;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem einstellungenToolStripMenuItem;
        private System.Windows.Forms.Label EMMCALabel;
        private System.Windows.Forms.Label EMMCardLabel;
        private System.Windows.Forms.TextBox SerialTextBox;
        private System.Windows.Forms.TextBox CardTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem logViaFTPÖffnenToolStripMenuItem;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox KeyCheckBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.CheckBox doubleCheckBox;
        private System.Windows.Forms.ToolStripMenuItem analyseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kopierenlängenbegrenztToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kopierenverixtToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.RichTextBox LengthRichTextBox;
        private System.Windows.Forms.TextBox SearchTextBox;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem statistikToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel TimeStripLabel;
        private System.Windows.Forms.ToolStripStatusLabel LogTimeStripLabel;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ListView PaarListView;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button analyzeButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem inDieZwischenablageKopierenToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel CountStripLabel;
        private System.Windows.Forms.ToolStripMenuItem singleEMMInDieZwischenablageToolStripMenuItem;
        private System.Windows.Forms.CheckBox ColorCheckBox;
        private System.Windows.Forms.ToolStripStatusLabel TypeStripLabel;
        private System.Windows.Forms.ToolStripMenuItem überToolStripMenuItem;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.CheckBox dateCheckBox;
    }
}

