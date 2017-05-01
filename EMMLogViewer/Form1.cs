using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EMMLogViewer.FTPClient;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace EMMLogViewer
{

    public partial class MainForm : Form
    {
        public const int OSCAM = 0;
        public const int CCAM = 1;

        private int vCamType = OSCAM;
        private string vCardCode = "";
        private string vBoxCode = "";
        private List<string> vDoubles = new List<string>();
        private List<int> vDoubleCount = new List<int>();
        private List<LengthInfo> vLengthList = new List<LengthInfo>();

        public MainForm()
        {
            InitializeComponent();

            CalcDateForBox();
            
            //Columns anlegen
            listView1.Columns.Add("1", "Zeile", 40);
            listView1.Columns.Add("2", "Anzahl", 40);
            listView1.Columns.Add("3", "Datum", 120);
            listView1.Columns.Add("4", "ID", 120);
            listView1.Columns.Add("5", "Typ", 60);
            listView1.Columns.Add("6", "Card", 30);
            listView1.Columns.Add("7", "Box", 30);
            listView1.Columns.Add("8", "Länge", 80);
            listView1.Columns.Add("9", "Beschreibung", 160);
            listView1.Columns.Add("10", "EMM", 660);
            listView1.Columns.Add("11", "Orig-EMM", 0);

            //Columns anlegen  
            PaarListView.Columns.Add("1", "Datum", 120);
            PaarListView.Columns.Add("2", "Alter (h)", 60);
            PaarListView.Columns.Add("3", "Anzahl", 40);
            PaarListView.Columns.Add("4", "Typ", 60);
            PaarListView.Columns.Add("5", "Länge", 80);
            PaarListView.Columns.Add("6", "Checksumme", 80);
            PaarListView.Columns.Add("7", "EMM", 660);
            //Einstellungen lesen
            ReadIni();

            //LengthRichTextBox
            ReadLengthInfo(OSCAM);
        }


        /// <summary>
        /// für die DatumsBox ein gültiges Datum ermitteln
        /// </summary>
        /// <returns></returns>
        private void CalcDateForBox()
        {
            dateTimePicker.Value = DateTime.Now.AddDays(-40);
            IniFile lIni = new IniFile();
            if (lIni.Read("VALID", "Receiver") != "") {
                try{
                    string date = lIni.Read("VALID", "Receiver");
                    // Specify exactly how to interpret the string.
                    IFormatProvider culture = new System.Globalization.CultureInfo("de-DE", true);

                    DateTime boxDate = DateTime.Parse(date, culture, System.Globalization.DateTimeStyles.AssumeLocal);
                    if (boxDate < System.DateTime.Now)
                    {
                        while (boxDate < System.DateTime.Now)
                        {
                            boxDate = boxDate.AddDays(20);
                        }
                    }
                    dateTimePicker.Value = boxDate.AddDays(-20);
                }
                catch
                {

                }
            }

        }

        /// <summary>
        /// Form schließen
        /// </summary>
        /// <returns></returns>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// File öffnen
        /// </summary>
        /// <returns></returns>
        private void logFileÖffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextBox.Clear();
            OpenFileDialog dlg = new OpenFileDialog();
            IniFile lIni = new IniFile();
            if (lIni.Read("DIR", "Path") != "") {
                dlg.InitialDirectory = lIni.Read("DIR", "Path");
            }else{
                dlg.InitialDirectory = "c:\\";
            }
            dlg.Filter = "log files (*.log)|*.log|All files (*.*)|*.*";
            dlg.FilterIndex = 1;
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                readFile(dlg.FileName); 
                lIni.Write("DIR", Path.GetDirectoryName(dlg.FileName), "Path");
                CountLines();
            }
            DetectCamType();
            ReadLengthInfo(vCamType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private void einstellungenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setting lDlg = new setting();
            lDlg.ShowDialog();
            ReadIni();
        }

        /// <summary>
        /// Ini Datei lesen
        /// </summary>
        /// <returns></returns>
        private void ReadIni()
        {
            IniFile lIni = new IniFile();
            CardTextBox.Text = lIni.Read("CARD", "Receiver");
            SerialTextBox.Text = lIni.Read("BOX", "Receiver");
            CalcEMM();
        }

        /// <summary>
        /// CardID und BoxID in Hex umrechnen und ausgeben
        /// </summary>
        /// <returns></returns>
        private void CalcEMM() 
        {
            string lCode = CardTextBox.Text;
            if (lCode != "")
            {
                lCode = lCode.Remove(lCode.Length - 1, 1);
                lCode = lCode.Substring(Math.Max(0, lCode.Length - 10), 10);
                try
                {
                    lCode = String.Format("{0:X08}", Convert.ToInt32(lCode));
                    vCardCode = lCode;
                    if (int.Parse(lCode.Substring(2, 2), System.Globalization.NumberStyles.HexNumber) < 64)
                    {
                        lCode += " (V13)";
                    }
                    else
                    {
                        lCode += " (V14)";
                    }
                }
                catch 
                { 
                    lCode = ""; 
                    vCardCode = "";
                }

            }
            EMMCardLabel.Text = "--> EMM Hex Code: " + lCode;
            lCode = SerialTextBox.Text;
            if (lCode != "")
            {
                lCode = lCode.Remove(lCode.Length - 1, 1);
                lCode = lCode.Substring(Math.Max(0, lCode.Length - 10), 10);
                try
                {
                    lCode = String.Format("{0:X08}", Convert.ToInt32(lCode));
                    vBoxCode = lCode;
                }
                catch 
                { 
                    lCode = ""; 
                    vBoxCode = "";
                }
            }
            EMMCALabel.Text = "--> EMM Hex Code: " + lCode;
        }

        /// <summary>
        /// Wandelt den übergebenen String in die Hexadezimaldarstellung
        /// </summary>
        /// <param name="Hexstring">der umzuwandelnde String</param>
        /// <returns>Hexadezimaldarstellung</returns>
        private string StringToHex(string hexstring)
        {
            var sb = new StringBuilder();
            foreach (char t in hexstring)
                sb.Append(Convert.ToInt32(t).ToString("x") + " ");
            return sb.ToString();
        }

        /// <summary>
        /// Einträge im File zählen und im Strip ausgeben
        /// </summary>
        /// <returns></returns>
        private void CountLines()
        {
            CountStripLabel.Text = "Einträge: " + TextBox.Lines.Length.ToString();
        }

        /// <summary>
        /// File via FTP öffnen
        /// </summary>
        /// <returns></returns>
        private void logViaFTPÖffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextBox.Clear();
            IniFile lIni = new IniFile();
            if (lIni.Read("LOG", "Receiver") == "") { return; }
            string ftpUri = lIni.Read("IP", "Receiver");
            string ftpUser = lIni.Read("USER", "Receiver"); ;
            string passwd = lIni.Read("PWD", "Receiver");
            string ftpfileName = Path.GetFileName(lIni.Read("LOG", "Receiver"));
            string ftpfilePath = lIni.Read("LOG", "Receiver");
            ftpfilePath = ftpfilePath.Remove(Math.Max(0, ftpfilePath.Length - ftpfileName.Length - 1), ftpfileName.Length + 1); 

            FTPConnection ftpCon = new FTPConnection();
            string localFileName = Path.GetTempFileName();
            //Prgressbar aufbauen
            ProgressForm lProgress = new ProgressForm();
            lProgress.StartPosition = FormStartPosition.CenterParent;
            lProgress.Show(this);
            lProgress.SetProgress("Öffne FTP Verbindung", 0);
            //FTP verbinden
            ftpCon.Open(ftpUri, ftpUser, passwd, FTPMode.Passive);
            lProgress.SetProgress("Wechsle Verzeichnis", 20);
            ftpCon.SetCurrentDirectory(ftpfilePath);
            lProgress.SetProgress("Lade Daten", 40);
            ftpCon.GetFile(ftpfileName, localFileName, FTPFileTransferType.Binary);
            lProgress.SetProgress("Schließe Verbindung", 60);
            ftpCon.Close();
            lProgress.SetProgress("Lese Datei", 80);
            readFile(localFileName);
            //Ende
            lProgress.SetProgress("Fertig ...", 100);
            lProgress.Close();
            CountLines();

            DetectCamType();
            ReadLengthInfo(vCamType);
        }

        /// <summary>
        /// Logfile einlesen und in der Textbox ausgeben
        /// </summary>
        /// <returns></returns>
        private void readFile(string aFileName)
        {
            if (!System.IO.File.Exists(aFileName)) { return;} // wenn kein File gefunden, dann Exit
            string[] lines = System.IO.File.ReadAllLines(aFileName);
            foreach (string line in lines)
            {
                // Use a tab to indent each line of the file.
                TextBox.AppendText(line + "\n");
            }

        }

        /// <summary>
        /// Analyse des EMM's
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            int lineNr = 1;
            int Count = 1;
            string sDatum;
            string sEmmTyp;
            string sTyp = "";
            string sCard = "";
            string sBox = "";
            string sLength = "";
            string sDescription = "";
            string sEmm = "";
            string sOrigEmm = "";
            DateTime firstDate = new DateTime();
            DateTime lastDate = new DateTime();

            listView1.Items.Clear();
            vDoubles.Clear();
            vDoubleCount.Clear();
            foreach (string line in TextBox.Lines)
            {
                string corrLine = RemoveDoubleSpaceCharacters(line); //doppelte Leerzeichen entfernen
                string[] data = corrLine.Split(new Char[] { ' ', '\t' });
                if (data.Count() == 5)
                {
                    if ((!dateCheckBox.Checked) || (dateTimePicker.Value <= new DateTime(Convert.ToInt32(data[0].Substring(0, 4)), Convert.ToInt32(data[0].Substring(5, 2)), Convert.ToInt32(data[0].Substring(8, 2)),
                                                                                         Convert.ToInt32(data[1].Substring(0, 2)), Convert.ToInt32(data[1].Substring(3, 2)), Convert.ToInt32(data[1].Substring(6, 2)))))
                    {
                        if ((!vDoubles.Contains(data[3])) || (!doubleCheckBox.Checked))
                        {
                            //Datum merken
                            if (listView1.Items.Count == 0)
                            {
                                firstDate = new DateTime(Convert.ToInt32(data[0].Substring(0, 4)), Convert.ToInt32(data[0].Substring(5, 2)), Convert.ToInt32(data[0].Substring(8, 2)),
                                                         Convert.ToInt32(data[1].Substring(0, 2)), Convert.ToInt32(data[1].Substring(3, 2)), Convert.ToInt32(data[1].Substring(6, 2)));
                            }
                            lastDate = new DateTime(Convert.ToInt32(data[0].Substring(0, 4)), Convert.ToInt32(data[0].Substring(5, 2)), Convert.ToInt32(data[0].Substring(8, 2)),
                                                     Convert.ToInt32(data[1].Substring(0, 2)), Convert.ToInt32(data[1].Substring(3, 2)), Convert.ToInt32(data[1].Substring(6, 2)));

                            //Einträge auslesen
                            sDatum = data[0] + ' ' + data[1];
                            sEmmTyp = data[2];
                            sEmm = data[3] + ' ' + data[4];
                            sOrigEmm = sEmm;
                            //prüfen ob Card Key enthalten
                            if ((vCardCode != "") && (data[3].Contains(vCardCode)))
                            {
                                //sCard = "1";
                                sCard = CountStrings(data[3], vCardCode).ToString();
                            }
                            else
                            {
                                sCard = "";
                            }
                            //prüfen ob Box Key enthalten
                            if ((vBoxCode != "") && (data[3].Contains(vBoxCode)))
                            {
                                //sBox = "1";
                                sCard = CountStrings(data[3], vBoxCode).ToString();
                            }
                            else
                            {
                                sBox = "";
                            }
                            //Typ auslesen
                            sTyp = data[3].Substring(16, 2);
                            //Längenangabe extrahieren
                            sLength = data[3].Substring(4, 2);
                            LengthInfo info = vLengthList.Find(x => x.Length == sLength);
                            sLength = sLength + " hex / " + int.Parse(sLength, System.Globalization.NumberStyles.HexNumber);
                            if (KeyCheckBox.Checked)
                            {
                                if (vCardCode != "") { sEmm = sEmm.Replace(vCardCode, "xxxxxxxx"); }
                                if (vBoxCode != "") { sEmm = sEmm.Replace(vBoxCode, "xxxxxxxx"); }
                            }
                            //sDescription = CreateDescription(sEmm);
                            if (info != null) { sDescription = info.Text; } else { sDescription = ""; }

                            //EMM formatieren mit Leerzeichen
                            if (vCamType == OSCAM)
                            {
                                sEmm = sEmm.Insert(18, " ");
                                sEmm = sEmm.Insert(16, " ");
                            }
                            else
                            {
                                sEmm = sEmm.Insert(10, " ");
                            }
                            sEmm = sEmm.Insert(8, " ");
                            sEmm = sEmm.Insert(6, " ");
                            sEmm = sEmm.Insert(4, " ");
                            ListViewItem listItem = listView1.Items.Add(new ListViewItem(new string[] { Convert.ToString(lineNr), Convert.ToString(Count), sDatum, sEmmTyp, sTyp, sCard, sBox, sLength, sDescription, sEmm, sOrigEmm }));
                            listItem.BackColor = Color.White; //Hintergrund weiß zeichnen 
                            vDoubles.Add(data[3]);
                            vDoubleCount.Add(1);
                        }
                        else
                        {
                            if (doubleCheckBox.Checked)
                            {
                                vDoubleCount[vDoubles.IndexOf(data[3])] += 1;
                                lastDate = new DateTime(Convert.ToInt32(data[0].Substring(0, 4)), Convert.ToInt32(data[0].Substring(5, 2)), Convert.ToInt32(data[0].Substring(8, 2)),
                                                         Convert.ToInt32(data[1].Substring(0, 2)), Convert.ToInt32(data[1].Substring(3, 2)), Convert.ToInt32(data[1].Substring(6, 2)));

                            }
                        }
                    }
                    lineNr++;
                }
            }
            //wenn keine Doubles dann die Anzahl aktualisieren
            if (doubleCheckBox.Checked)
            {
                int count = 0;
                foreach (ListViewItem item in listView1.Items)
                {
                    item.SubItems[1].Text = vDoubleCount[count].ToString();
                    count++;
                }
            }
            //Zeit ausgeben erster Eintrag bis jetzt
            if (firstDate != DateTime.MinValue)
            {
                Int64 lTicks = (DateTime.Now).Subtract(firstDate).Ticks;
                TimeSpan ts = TimeSpan.FromTicks(lTicks);
                TimeStripLabel.Text = "Dauer: " + ts.Days + " Tage " + ts.Hours + " Stunden " + ts.Minutes + " Minuten (" + Math.Round(ts.TotalHours, 2).ToString() + " h)";
            }
            else
            {
                TimeStripLabel.Text = "Dauer: --";
            }
            //Zeit ausgeben erster Eintrag bis letzter Eintrag
            if ((firstDate != DateTime.MinValue) && (lastDate != DateTime.MinValue))
            {
                Int64 lTicks = (lastDate).Subtract(firstDate).Ticks;
                TimeSpan ts = TimeSpan.FromTicks(lTicks);
                LogTimeStripLabel.Text = "Logdauer: " + ts.Days + " Tage " + ts.Hours + " Stunden " + ts.Minutes + " Minuten (" + Math.Round(ts.TotalHours, 2).ToString() + " h)";
            }
            else
            {
                LogTimeStripLabel.Text = "Logdauer: --";
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0){ return; }

            string Copydata = "";
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                Copydata += item.SubItems[2].Text + "  " + item.SubItems[3].Text + "  " + item.SubItems[10].Text + '\n';
            }
            System.Windows.Forms.Clipboard.SetDataObject(Copydata, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string CreateDescription(string aEmm)
        {
            //Unterscheidung 02er, 07er
            switch (aEmm.Substring(16, 2))
            {
                case "02": return aEmm.Substring(16, 2) + "er L:" + aEmm.Substring(20, 2) + " " + aEmm.Substring(26, 2) + " " + aEmm.Substring(28, 2); 
                case "07": return aEmm.Substring(16, 2) + "er L:" + aEmm.Substring(18, 2); 
                default: return aEmm.Substring(16, 2) + "er unbekannt"; 
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private void analyseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0) { return; }

            string data = listView1.SelectedItems[0].SubItems[9].Text;
            data = data.Replace(" blocked", ""); //störendes Ende entfernen
            data = data.Replace(" ", ""); //Leerzeichen entfernen
            Analyse lDlg = new Analyse(data);
            lDlg.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private void kopierenlängenbegrenztToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Copydata = "";
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                switch (item.SubItems[4].Text){
                    case "02": Copydata += FormatBytes(item.SubItems[10].Text.Substring(0, 30)) + String.Format(" ({0} mal)", item.SubItems[1].Text) + '\n'; break;
                    case "07": Copydata += FormatBytes(item.SubItems[10].Text.Substring(0, 30)) + String.Format(" ({0} mal)", item.SubItems[1].Text) + '\n'; break;
                    default: Copydata += FormatBytes(item.SubItems[10].Text) + '\n'; break;
                }
            }
            System.Windows.Forms.Clipboard.SetDataObject(Copydata, true);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string FormatBytes(string aBytes)
        {
            string result = "";
            for (int i = 0; i < aBytes.Length; i++)
            {
                if ((i >0 ) &&((i % 2) == 0)) { result += " "; }
                result += aBytes[i];
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private void kopierenverixtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Copydata = "";
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                switch (item.SubItems[4].Text)
                {
                    case "02": {
                        string data = item.SubItems[10].Text;
                        var aStringBuilder = new StringBuilder(data);
                        aStringBuilder.Remove(8, 8);
                        aStringBuilder.Insert(8, "xxxxxxxx");
                        aStringBuilder.Remove(30, data.Length -30);
                        for (int i = 0; i < data.Length - 30; i++) { aStringBuilder.Insert(30, "x"); }
                        data = aStringBuilder.ToString();
                        Copydata += FormatBytes(data) + String.Format(" ({0} mal)", item.SubItems[1].Text) + '\n'; 
                        break; 
                    }
                    case "07": {
                        string data = item.SubItems[10].Text;
                        var aStringBuilder = new StringBuilder(data);
                        aStringBuilder.Remove(8, 8);
                        aStringBuilder.Insert(8, "xxxxxxxx");
                        aStringBuilder.Remove(24, 8);
                        aStringBuilder.Insert(24, "xxxxxxxx");
                        aStringBuilder.Remove(38, data.Length - 38);
                        //Todo hier muss noch der Rest verixt werden
                        data = aStringBuilder.ToString() + BuildEmms(data.Substring(38, data.Length - 38));
                        Copydata += FormatBytes(data) + String.Format(" ({0} mal)", item.SubItems[1].Text) + '\n'; 
                        break;
                    }
                    default: Copydata += FormatBytes(item.SubItems[10].Text) + '\n'; break;
                }
            }
            System.Windows.Forms.Clipboard.SetDataObject(Copydata, true);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string BuildEmms(string aMultiEmm)
        {
            int len = 0;
            string result = "";
            result += aMultiEmm.Substring(0, 2); //"Sub-EMM ID (00 bis 04 möglich)"
            if (aMultiEmm.Substring(2, 8) != "00000000")
            {
                result += "xxxxxxxx"; //Box ID
            }
            else {
                result += "00000000";
            }
            result += aMultiEmm.Substring(10, 2); // "Anzahl der noch folgenden Bytes im Sub-EMM (Hex); Länge Payload"
            result += aMultiEmm.Substring(12, 2); // "90 fixer Wert"
            result += aMultiEmm.Substring(14, 2); // "Anzahl der noch folgenden Bytes im Sub-EMM (Hex); Länge Payload"
            len = int.Parse(aMultiEmm.Substring(14, 2), System.Globalization.NumberStyles.HexNumber) * 2;
            result += aMultiEmm.Substring(16, 2); // "EMM-Typisierung; User-Verschlüsselung"
            result += aMultiEmm.Substring(18, 2); // "Key-Index?"
            len -= 4; //4 Zeichen weg
            for (int i = 20; i < len; i++) { result += 'x'; }

            //wenn Zeichen übrig sind, dann nächstes EMM
            aMultiEmm = aMultiEmm.Remove(0, len + 20);
            if ((aMultiEmm != "") && aMultiEmm.Length >= 60)
            { //minimale EMM Länge
                result += BuildEmms(aMultiEmm);
            }
            else
            {
                result += "xxxx"; //aMultiEmm;
            }

            return result;
        }

        /// <summary>
        /// Längeninformation aus externem File lesen
        /// </summary>
        /// <returns></returns>
        private void ReadLengthInfo(int camType){
            string EXE = Assembly.GetExecutingAssembly().GetName().Name;
            string LogFile = "";
            switch (camType)
            {
                case OSCAM: LogFile = new FileInfo(EXE + ".oscam").FullName.ToString(); break;
                case CCAM: LogFile = new FileInfo(EXE + ".ccam").FullName.ToString(); break;
            }
            if (!System.IO.File.Exists(LogFile)) { return; } //Exit wenn File nicht existiert
            string[] lines = System.IO.File.ReadAllLines(LogFile);
            foreach (string line in lines)
            {
                // Use a tab to indent each line of the file.
                LengthRichTextBox.AppendText(line + "\n");
                //unsere Liste aufbauen
                string[] data = line.Split(new Char[] { ' ','\t' });
                string lText = line.Substring(line.IndexOf(':')+1);
                vLengthList.Add(new LengthInfo(data[0], lText));
            }
        }

        /// <summary>
        /// Zählen wie oft ein Substring in einem String vorkommt
        /// </summary>
        /// <returns></returns>
        private static int CountStrings(string str, string regexStr)
        {
            Regex regex = new Regex(regexStr);
            return regex.Matches(str).Count;
        }

        /// <summary>
        /// Suche ausführen, alle Zeilen markieren die den Suchtext enthalten
        /// </summary>
        /// <returns></returns>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (SearchTextBox.Text != "")
            {
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    if (listView1.Items[i].SubItems[10].Text.ToUpper().Contains(SearchTextBox.Text.ToUpper()))
                    {
                        //listView1.Items[i].SubItems[10].BackColor = Color.CadetBlue;
                        listView1.Items[i].BackColor = Color.CadetBlue;
                    }
                    else
                    {
                        listView1.Items[i].BackColor = Color.White;
                    }
                }

            }
            else {
                for (int i = 0; i < listView1.Items.Count; i++) { listView1.Items[i].BackColor = Color.White; }
            }
        }

        /// <summary>
        /// Statistik Form anzeigen
        /// </summary>
        /// <returns></returns>
        private void statistikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0) { return; }

            StatistikFrm lFrm = new StatistikFrm(listView1.SelectedItems[0].SubItems[10].Text, TextBox.Lines);
            lFrm.ShowDialog();
        }

        /// <summary>
        /// Checksumme berechnen
        /// </summary>
        /// <returns></returns>
        private string CalcCheckSum(string EmmLine)
        {
            int CheckSum = 0;
            for (int i = 0; i < EmmLine.Length; i++)
            {
                CheckSum += Convert.ToInt32(EmmLine[i]);
            }
            return CheckSum.ToString();
        }

        /// <summary>
        /// vergangene Stunden berechnen
        /// </summary>
        /// <returns></returns>
        private string CalcDatum(string aDatum)
        {
            Int64 xDiff = ((DateTime.Now).Subtract(new DateTime(Convert.ToInt32(aDatum.Substring(0, 4)), Convert.ToInt32(aDatum.Substring(5, 2)), Convert.ToInt32(aDatum.Substring(8, 2)),
                                                                Convert.ToInt32(aDatum.Substring(11, 2)), Convert.ToInt32(aDatum.Substring(14, 2)), Convert.ToInt32(aDatum.Substring(17, 2))))).Ticks;

            return Math.Round(TimeSpan.FromTicks(xDiff).TotalHours, 2).ToString();
        }

        /// <summary>
        /// Pärchen Zeile hinzufügen
        /// </summary>
        /// <returns></returns>
        private void AddPaarLine(string aLine, Int32 aCount)
        {
            string sDatum;
            string sTyp = "";
            string sLength = "";
            string sEmm = "";

            string[] data = aLine.Split(new Char[] { ' ', '\t' });
            sDatum = data[0] + ' ' + data[1];
            sEmm = data[3] + ' ' + data[4];
            //Typ auslesen
            sTyp = data[3].Substring(16, 2);
            //Längenangabe extrahieren
            sLength = data[3].Substring(4, 2);
            LengthInfo info = vLengthList.Find(x => x.Length == sLength);
            sLength = sLength + " hex / " + int.Parse(sLength, System.Globalization.NumberStyles.HexNumber);

            Int64 xDiff = ((DateTime.Now).Subtract(new DateTime(Convert.ToInt32(data[0].Substring(0, 4)), Convert.ToInt32(data[0].Substring(5, 2)), Convert.ToInt32(data[0].Substring(8, 2)),
                                                                Convert.ToInt32(data[1].Substring(0, 2)), Convert.ToInt32(data[1].Substring(3, 2)), Convert.ToInt32(data[1].Substring(6, 2))))).Ticks;

            //EMM formatieren mit Leerzeichen
            string sOrigEmm = sEmm;
            if (vCardCode != "") { sEmm = sEmm.Replace(vCardCode, "xxxxxxxx"); }
            if (vCamType == OSCAM)
            {
                sEmm = sEmm.Insert(18, " ");
                sEmm = sEmm.Insert(16, " ");
            }
            else
            {
                sEmm = sEmm.Insert(10, " ");
            }
            sEmm = sEmm.Insert(8, " ");
            sEmm = sEmm.Insert(6, " ");
            sEmm = sEmm.Insert(4, " ");
            PaarListView.Items.Add(new ListViewItem(new string[] { sDatum, CalcDatum(sDatum), aCount.ToString(), 
                                                                   sTyp, sLength, CalcCheckSum(sEmm), sEmm, sOrigEmm }));
        }

        /// <summary>
        /// Analyse der Pärchen
        /// </summary>
        /// <returns></returns>
        private void analyzeButton_Click(object sender, EventArgs e)
        {
            List<string> vSingles = new List<string>();
            PaarListView.Items.Clear();
            foreach (string line in TextBox.Lines)
            {
                if (line.Length > 10) { vSingles.Add(line); } //versuchsweise mal keine doppelten entfernen

                //string[] data = line.Split(new Char[] { ' ', '\t' });
                //if (data.Count() == 11)
                //{
                //    if (vSingles.Count == 0)
                //    {
                //        vSingles.Add(line);
                //    }
                //    else
                //    {
                //        //nur hinzufügen wenn nicht gleich dem letzen Item
                //        if (! vSingles[vSingles.Count-1].Contains(data[7]))
                //        {
                //            vSingles.Add(line);
                //        }
                //    }
                //}
            }
            //jetzt sind nur mehr abwechselnde Zeilen vorhanden
            EDoubles myDouble = new EDoubles();
            for (int i = 0; i < vSingles.Count; i++)
            {
                if (!myDouble.Add(RemoveDoubleSpaceCharacters(vSingles[i])))
                {
                    //Einträge auslesen
                    //Zeile 1
                    AddPaarLine(myDouble.Line1, myDouble.Count1);
                    //Zeile 2
                    AddPaarLine(myDouble.Line2, myDouble.Count2);
                    //leerZeile
                    PaarListView.Items.Add(new ListViewItem(new string[] { "", "", "", "", "", "" }));
                    myDouble.Clear();
                    myDouble.Add(RemoveDoubleSpaceCharacters(vSingles[i]));
                }
            }
            //letztes Pärchen ausgeben
            if (myDouble.Count1 > 0)
            {
                //Zeile 1
                AddPaarLine(myDouble.Line1, myDouble.Count1);
            }
            if (myDouble.Count2 > 0){
                //Zeile 2
                AddPaarLine(myDouble.Line2, myDouble.Count2);
            }
            if (ColorCheckBox.Checked) { DoColourListView(); }
        }

        /// <summary>
        /// Pärchen kopieren
        /// </summary>
        /// <returns></returns>
        private void inDieZwischenablageKopierenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Copydata = "";
            for (int i = 0; i < PaarListView.Items.Count; i++)
            {
                if (PaarListView.Items[i].SubItems[0].Text != "")
                {
                    Copydata += PaarListView.Items[i].SubItems[0].Text + "  " + PaarListView.Items[i].SubItems[6].Text.Substring(0, 25) + " (" + PaarListView.Items[i].SubItems[2].Text + " mal)" + Environment.NewLine;
                }
                else
                {
                    Copydata += Environment.NewLine;
                }

            }
            System.Windows.Forms.Clipboard.SetDataObject(Copydata, true);
        }

        /// <summary>
        /// Removes double space characters.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        private string RemoveDoubleSpaceCharacters(string text)
        {
            return System.Text.RegularExpressions.Regex.Replace(text, "[ ]+", " ");
        }

        /// <summary>
        /// Single EMM kopieren
        /// </summary>
        /// <returns></returns>
        private void singleEMMInDieZwischenablageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Copydata = "";

            if (PaarListView.SelectedItems.Count <= 0) { return; } //wenn nichts selektiert dann verlassen

            //blocked und Leerzeichen entfernen
            string[] data = PaarListView.SelectedItems[0].SubItems[7].Text.Split(new Char[] { ' ', '\t' });

            Copydata = data[0] + Environment.NewLine;

            System.Windows.Forms.Clipboard.SetDataObject(Copydata, true);

        }

        /// <summary>
        /// Pärchen einfärben
        /// </summary>
        /// <returns></returns>
        private void DoColourListView()
        {
            //Farben einlesen
            Color[] Farben = new Color[10];
            IniFile lIni = new IniFile();
            for (int i = 0; i < 10; i++)
            {
                string data = lIni.Read(i.ToString(), "Color");
                string[] color = data.Split(new Char[] { ',' });
                if (color.Length == 3)
                {
                    Farben[i] = Color.FromArgb(Convert.ToInt32(color[0]), Convert.ToInt32(color[1]), Convert.ToInt32(color[2]));
                }
                else
                {
                    Farben[i] = Color.White;
                }
            }
            // Listen aufbauen
            List<string> lDoubles = new List<string>();
            List<int> lDoubleCount = new List<int>();
            for (int i = 0; i < PaarListView.Items.Count; i++)
            {
                if(PaarListView.Items[i].SubItems[5].Text != ""){ 
                    if(! lDoubles.Contains(PaarListView.Items[i].SubItems[5].Text)){
                        lDoubles.Add(PaarListView.Items[i].SubItems[5].Text);
                        lDoubleCount.Add(1);
                    }else{
                        lDoubleCount[lDoubles.IndexOf(PaarListView.Items[i].SubItems[5].Text)] += 1;
                    }
                }
            }

            for (int i = lDoubles.Count - 1; i >= 0; i--)
            {
                if (lDoubleCount[i] == 1) { lDoubles.RemoveAt(i); }  //Elemente die nur einmal vorkommen löschen
            }
            //Zeilen einfärben
            for (int i = 0; i < PaarListView.Items.Count; i++)
            {
                if ((lDoubles.IndexOf(PaarListView.Items[i].SubItems[5].Text) >= 0) && (lDoubles.IndexOf(PaarListView.Items[i].SubItems[5].Text) < 10))
                {
                    PaarListView.Items[i].BackColor = Farben[lDoubles.IndexOf(PaarListView.Items[i].SubItems[5].Text)];
                }
            }
        }

        /// <summary>
        /// anhand der 1. Zeile den CamType ermitteln
        /// </summary>
        /// <returns></returns>
        private void DetectCamType()
        {
            if (TextBox.Lines.Count() > 0)
            {
                string[] data = RemoveDoubleSpaceCharacters(TextBox.Lines[0]).Split(new Char[] { ' ', '\t' });
                if ((data.Length > 3) && (data[3].StartsWith("8270"))) { vCamType = OSCAM; } else { vCamType = CCAM; }
            }

            if (vCamType == CCAM) { TypeStripLabel.Text = "Typ: CCAM"; } else { TypeStripLabel.Text = "Typ: OSCAM"; }
        }

        private void überToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox lbox = new AboutBox();
            lbox.ShowDialog();
        }

    }
}
