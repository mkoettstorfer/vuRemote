using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MSoft;
using System.Net;
using System.IO;
using System.Xml;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;


namespace vuRemote
{
    public partial class RemoteForm : Form
    {
        const Boolean NOW = true;
        const Boolean NEXT = false;

        //private string vURL;
        private readonly string vServiceURL;
        private readonly string vEPGURL;
        private readonly string vEPGURLNext;
        private readonly string vSenderURL;
        //private string vAddTimerURL;
        private DateTime vTime = DateTime.Now;
        private readonly string vUrlGoogle;
        private readonly string vUrlIMDb;
        private readonly string vGetChannelURL;
        private readonly string vGetAudioURL;
        private readonly string vSetAudioURL;
        private readonly string vStreamUrl;
        private readonly string vMediaPlayer;
        private readonly List<string> vServiceList = new List<string>();
        private readonly Boolean vDayLightSaving;
        private static Regex vRegEx;
        
        private readonly RemoteSender vSender;

        public RemoteForm()
        {
            InitializeComponent();
            vSender = new RemoteSender();

            //rechte Seite Elemente
            //Columns anlegen
            this.listView1.Columns.Add("1", "Icon", 48);
            this.listView1.Columns.Add("2", "Nr.", 30);
            this.listView1.Columns.Add("3", "Name", 160);
            this.listView1.Columns.Add("4", "Programm", 310);
            this.listView1.Columns.Add("5", "Zeit", 100);
            this.listView1.Columns.Add("6", "tvtvId", 0);
            this.listView1.Columns.Add("7", "startticks", 0);
            this.listView1.Columns.Add("8", "progress", 0);

            //this.listView1.Items.Add("1", "1", 0);
            //this.listView1.Items[listView1.Items.Count - 1].SubItems.Add("ORF");
            //this.listView1.Items[listView1.Items.Count - 1].SubItems.Add("Programm");

            //Icons einlesen
            ReadImages();
            //Adresse einlesen
            IniFile lIni = new IniFile();
            var lip = lIni.Read("IP", "Receiver");
            vServiceURL = "http://" + lip + "/web/getservices";
            vEPGURL = "http://" + lip + "/web/epgnow?bRef={REF}&stype=tv";
            vEPGURLNext = "http://" + lip + "/web/epgnext?bRef={REF}&stype=tv";
            //vEPGURL = "http://" + lip + "/web/epgmulti?bRef={REF}&stype=tv"; epgnext epgnownext
            vSenderURL = "http://" + lip + "/web/zap?sRef=";
            vUrlGoogle = lIni.Read("URL", "Webbroser");
            vUrlIMDb = "http://www.imdb.com/find?s=all&q=";
            vGetChannelURL = "http://" + lip + "/web/subservices";
            vGetAudioURL = "http://" + lip + "/web/getaudiotracks";
            vSetAudioURL = "http://" + lip + "/web/selectaudiotrack?id=";
            vStreamUrl = "http://" + lip + "/web/stream.m3u?ref=";
            vMediaPlayer = lIni.Read("EXE", "Mediaplayer");
            vDayLightSaving = TimeZoneInfo.Local.IsDaylightSavingTime(DateTime.Now);

            // http://www.discogs.com/help/forums/topic/189304
            // http://meinews.niuz.biz/regex-t248608.html?s=3e8c40fe6d48a1a36e66437f3a3ec944&
            vRegEx = new Regex(@"[\x01-\x08\x0B\x0C\x0E-\x1F]", RegexOptions.Compiled);

            timeBox.Text = String.Format("{0:00}:{1:00}", vTime.Hour, vTime.Minute);
            //Update Timer aktiv schalten
            updateTimer.Enabled = true;

            //Refresh Timer für das 1. Init scharf machen
            refreshTimer.Enabled = true;
            refreshTimer.Interval = 500;
            refreshTimer.Tick -= refreshTimer_Tick;
            refreshTimer.Tick += new EventHandler(InitList_Tick);
        }

        private void beendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void überToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox lbox = new AboutBox();
            lbox.ShowDialog();
        }

        private void einstellungenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingForm lSettings = new SettingForm();
            lSettings.ShowDialog();
        }

        private void ControlButton_Click(object sender, EventArgs e)
        {
            ImageButton button;
            if (sender is ImageButton)
            {
                button = (ImageButton)sender;
                string lCode = "";
                Boolean blong;
                //unterstützte Kommandos
                //1 2 3 4 5 6 7 8 9 0 p+ p- vol+ vol- mute info up down left right 
                //fav pip opt tvr epg archiv media exit ok stop rec play pause fback fforward 
                //help standby menu text
                switch (button.Tag.ToString())
                {
                    case "1": lCode = "2"; break;
                    case "2": lCode = "3"; break;
                    case "3": lCode = "4"; break;
                    case "4": lCode = "5"; break;
                    case "5": lCode = "6"; break;
                    case "6": lCode = "7"; break;
                    case "7": lCode = "8"; break;
                    case "8": lCode = "9"; break;
                    case "9": lCode = "10"; break;
                    case "0": lCode = "11"; break;
                    case "exit": lCode = "174"; break;
                    case "menu": lCode = "139"; break;
                    case "red": lCode = "398"; break;
                    case "green": lCode = "399"; break;
                    case "yellow": lCode = "400"; break;
                    case "blue": lCode = "401"; break;
                    case "up": lCode = "103"; break;
                    case "down": lCode = "108"; break;
                    case "previous": lCode = "412"; break;
                    case "next": lCode = "407"; break;
                    case "ok": lCode = "352"; break;
                    case "v+": lCode = "115"; break;
                    case "v-": lCode = "114"; break;
                    case "radio": lCode = "385"; break;
                    case "stop": lCode = "128"; break;
                    case "record": lCode = "167"; break;
                    case "tv": lCode = "377"; break;
                    case "forward": lCode = "208"; break;
                    case "pause": lCode = "119"; break;
                    case "play": lCode = "207"; break;
                    case "mute": lCode = "113"; break;
                    case "power": lCode = "116"; break;
                    case "text": lCode = "388"; break;
                    case "audio": lCode = "392"; break;
                    case "help": lCode = "138"; break;
                    case "subtitle": lCode = "370"; break;
                    case "epg": lCode = "358"; break;
                    case "channelup": lCode = "402"; break;
                    case "channeldown": lCode = "403"; break;

                    //unsicher
                    case "left": lCode = "l05"; break;
                    case "right": lCode = "106"; break;
                    case "home": lCode = "102"; break;
                    case "end": lCode = "107"; break;
                    case "videoaltlast": lCode = "393"; break;
                    case "rewind": lCode = "168"; break;
                }
                blong = (Control.ModifierKeys & Keys.Shift) > 0;
                vSender.SendCode(lCode, blong);
                switch (button.Tag.ToString())
                {
                    case "v+": readVolume(); break;
                    case "v-": readVolume(); break;
                    case "mute": readVolume(); break;
                }
            }
        }

        private void senderlisteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProgrammList lChannelForm = new ProgrammList(new Point((Location.X + Width), (Location.Y)));
            lChannelForm.Show(); 
        }

        private void aufnahkmeplanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TimerList lTimerListForm = new TimerList();
            lTimerListForm.ShowDialog();
        }

        private void wakeUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IniFile lIni = new IniFile();
            var lBaseUrl = "http://" + lIni.Read("IP", "Receiver");
            Uri lUri = new Uri(lBaseUrl + "/api/powerstate?newstate=0");
            string lResponse = SendWebRequest(lUri);
            if (lResponse == "") { return; }
        }

        //an eine Webadresse einen REquest schicken und das Ergebnis als String zurückliefern
        private string SendWebRequest(Uri aURL, int timeout = 10000, Boolean showEx = true)
        {
            string lresponseFromServer = "";

            WebRequest lRequest = WebRequest.Create(aURL);
            ((HttpWebRequest)lRequest).UserAgent = "Eigenbau";
            lRequest.Method = "GET";
            lRequest.ContentType = "text/html";
            //Stream ldataStream = lrequest.GetRequestStream();
            // Set the 'Timeout' property in Milliseconds.
            lRequest.Timeout = timeout;
            try
            {
                // Get the response.
                WebResponse lResponse = lRequest.GetResponse();

                // Get the stream containing content returned by the server.
                Stream ldataStream = lResponse.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader lreader = new StreamReader(ldataStream);
                // Read the content.
                lresponseFromServer = lreader.ReadToEnd();

                // Clean up the streams.
                lreader.Close();
                ldataStream.Close();
                lResponse.Close();
                return lresponseFromServer;
            }
            catch
            {
                //Timeout abfangen
                if (showEx) MessageBox.Show("Request timed out");
                return ""; //Programm verlassen

            }
        }

        private void archivToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArchivList lArchivListForm = new ArchivList();
            lArchivListForm.ShowDialog();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeviceInfo lDeviceInfo = new DeviceInfo();
            lDeviceInfo.ShowDialog();
        }

        private void readVolume()
        {
            IniFile lIni = new IniFile();
            //wenn noch keine IP hinterlegt, dann nicht abfragen
            if (lIni.Read("IP", "Receiver") == "") { return; }

            var lBaseUrl = "http://" + lIni.Read("IP", "Receiver");
            Uri lUri = new Uri(lBaseUrl + "/web/vol");
            string lResponse = SendWebRequest(lUri, 1000, false);
            if (lResponse == "") { return; }
            //XML Elemente durchgehen
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(lResponse);
            XmlElement root = doc.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("/e2volume");
            try
            {
                string volume = "";
                string muted = "";
                foreach (XmlNode i in nodes)
                {
                    volume = i["e2current"].InnerText;
                    muted = i["e2ismuted"].InnerText;
                }
                volumeLabel.Text = "Vol.: " + volume;
                if (muted.CompareTo("True") == 0) { volumeLabel.Text += " (muted)"; }
                statusStrip.Refresh();
            }
            catch
            {
            }

        }

        private void RemoteForm_Shown(object sender, EventArgs e)
        {
            //Lautstärke bestimmen
            readVolume();
        }

        private void webinterfaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IniFile lIni = new IniFile();
            //wenn noch keine IP hinterlegt, dann nicht abfragen
            if (lIni.Read("IP", "Receiver") == "") { return; }
            if (lIni.Read("WEBIF", "Web") == "") { return; }
            var lBaseUrl = "http://" + lIni.Read("IP", "Receiver") + ':' + lIni.Read("WEBIF", "Web");
            System.Diagnostics.Process.Start(lBaseUrl);
        }

        private void oscamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IniFile lIni = new IniFile();
            //wenn noch keine IP hinterlegt, dann nicht abfragen
            if (lIni.Read("IP", "Receiver") == "") { return; }
            if (lIni.Read("OSCAM", "Web") == "") { return; }
            var lBaseUrl = "http://" + lIni.Read("IP", "Receiver") + ':' + lIni.Read("OSCAM", "Web");
            System.Diagnostics.Process.Start(lBaseUrl);

        }

    
    // rechte Seite mit der neuen Liste

        //Liste das 1. Mal laden, wird zeitverzögert aufgerufen
        //damit das Fenster gleich sichtbar ist
        private void InitList_Tick(object sender, EventArgs e)
        {
            //disablen, damit wir nicht noch mal aufgerufen werden
            refreshTimer.Enabled = false;

            //Read Services --> löst auch gleich GenerateEPGList aus
            this.ReadServices();

            //aktuellen Kanal ermitteln
            ReadActChannel();

            //auf den 1. Eintrag stellen
            if (this.listView1.Items.Count > 0)
            {
                this.listView1.Items[0].Focused = true;
                this.listView1.Items[0].Selected = true;
            }

            //auf aktuellen Eintrag stellen
            toolStripStatusLabel_Click(sender, e);

            //Refresh Timer scharf machen
            IniFile lIni = new IniFile();
            Int16 intervall;
            try
            {
                intervall = Convert.ToInt16(lIni.Read("INTERVALL", "Receiver"));
            }
            catch
            {
                intervall = 0;
            }
            if (intervall > 0)
            {
                refreshTimer.Tick -= InitList_Tick;
                refreshTimer.Tick += new EventHandler(refreshTimer_Tick);
                refreshTimer.Enabled = true;
                refreshTimer.Interval = intervall * 60000;
            }
        }

        //Services ermitteln: Favouriten, ...
        private void ReadServices()
        {
            Uri lUri = new Uri(vServiceURL);
            string lresponseFromServer = SendWebRequest(lUri);

            //XML Elemente durchgehen
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(lresponseFromServer);
            XmlElement root = doc.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("./e2service");
            try
            {
                string serviceName;
                string serviceRef;
                foreach (XmlNode i in nodes)
                {
                    serviceRef = i["e2servicereference"].InnerText;
                    serviceName = i["e2servicename"].InnerText;
                    serviceBox.Items.Add(serviceName);
                    vServiceList.Add(serviceRef);
                }
                serviceBox.SelectedIndex = 0;
            }
            catch
            {
            }
        }

        //ungültige Zeichen aus XML entfernen
        public static string CleanInvalidXmlCharacters(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return xml;
            }
            else
            {
                return vRegEx.Replace(xml, string.Empty);
            }
        }

        //an eine WEbadresse einen REquest schicken und das Ergebnis als String zurückliefern
        private string SendWebRequest(Uri aURL)
        {
            string lresponseFromServer = "";

            WebRequest lRequest = WebRequest.Create(aURL);
            ((HttpWebRequest)lRequest).UserAgent = "Eigenbau";
            lRequest.Method = "GET";
            lRequest.ContentType = "text/html";
            //Stream ldataStream = lrequest.GetRequestStream();
            // Set the 'Timeout' property in Milliseconds.
            lRequest.Timeout = 30000;
            try
            {
                this.timeOutLabel.Text = "";
                // Get the response.
                WebResponse lResponse = lRequest.GetResponse();

                // Get the stream containing content returned by the server.
                Stream ldataStream = lResponse.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader lreader = new StreamReader(ldataStream);
                // Read the content.
                lresponseFromServer = lreader.ReadToEnd();

                // Clean up the streams.
                lreader.Close();
                ldataStream.Close();
                lResponse.Close();
                return CleanInvalidXmlCharacters(lresponseFromServer);
            }
            catch
            {
                //Timeout abfangen
                //MessageBox.Show("Request timed out");
                this.timeOutLabel.Text = "Request timed out";
                return ""; //Programm verlassen

            }
        }


        //private void GenerateChannelTable()
        //{
        //    Uri lUri = new Uri(vURL);
        //    string lresponseFromServer = SendWebRequest(lUri);

        //    //XML Elemente durchgehen
        //    XmlDocument doc = new XmlDocument();
        //    doc.LoadXml(lresponseFromServer);
        //    XmlElement root = doc.DocumentElement;
        //    XmlNodeList nodes = root.SelectNodes("./channel");
        //    try
        //    {
        //        string channelNr;
        //        string channelName;
        //        string tvtId;
        //        string channelIcon = "";
        //        foreach (XmlNode i in nodes)
        //        {
        //            channelNr = i["channelNumber"].InnerText;
        //            channelName = i["channelName"].InnerText;
        //            tvtId = i["tvtvId"].InnerText;
        //            if (i["channelIcon"] != null) { channelIcon = i["channelIcon"].InnerText; } else { channelIcon = ""; }
        //            listView1.Items.Add(new ListViewItem(new string[] { channelNr, "", channelName, channelIcon, tvtId })); 


        //        }
        //    }
        //    catch
        //    {
        //    }
        //}

        //Kanal Icons einlesen
        private void ReadImages()
        {
            channelImageList.Images.Clear();
            string[] bilder = System.IO.Directory.GetFiles(Application.StartupPath + "\\resources\\", "*.png");
            foreach (string item in bilder)
            {
                Image bild = new Bitmap(item);
                string key = item;
                key = key.Replace(Application.StartupPath + "\\resources\\", "");
                key = key.Replace(".png", "");
                channelImageList.Images.Add(key, bild);
            }
        }

        //EPG Liste laden
        private void GenerateEPGList(Boolean aNow)
        {
            string lresponseFromServer = "";
            string lUrl = "";
            if (aNow) { lUrl = vEPGURL.Replace("{REF}", vServiceList[serviceBox.SelectedIndex]); } else { lUrl = vEPGURLNext.Replace("{REF}", vServiceList[serviceBox.SelectedIndex]); }
            ProgressForm lProgress = new ProgressForm
            {
                StartPosition = FormStartPosition.CenterParent
            };
            lProgress.Show(this);
            lProgress.SetProgress("Lade Daten", 0);
            Uri lUri = new Uri(lUrl);

            //Daten anfordern
            lresponseFromServer = SendWebRequest(lUri);
            //wenn keine Daten gefunden, dann exit
            if (lresponseFromServer == "")
            {
                lProgress.Close();
                return;
            }
            //lProgress.SetProgress("Erzeuge Liste", 50);
            //XML Elemente durchgehen
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(lresponseFromServer);
            XmlElement root = doc.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("./e2event");
            //Liste leeren, sie könnte ja schon mal befüllt worden sein
            listView1.Items.Clear();
            try
            {
                string channelNr;
                string channelName;
                string tvtId = "";
                string programm = "";
                string zeit = "";
                string startTime = "";
                string progress = "";
                string text = "";
                DateTime UnixEpochStart;
                DateTime UnixEpochEnd;
                int channel = 1;
                Int64 duration = 0;
                foreach (XmlNode i in nodes)
                {
                    channelNr = channel.ToString();
                    channel++;
                    channelName = i["e2eventservicename"].InnerText;
                    tvtId = i["e2eventservicereference"].InnerText;
                    if ((i["e2eventtitle"] != null) && ((i["e2eventtitle"].InnerText).ToUpper() != "NONE")) { programm = i["e2eventtitle"].InnerText; } else { programm = ""; }
                    if ((i["e2eventstart"] != null) && ((i["e2eventstart"].InnerText).ToUpper() != "NONE"))
                    {
                        startTime = i["e2eventstart"].InnerText;
                        UnixEpochStart = new DateTime(Convert.ToInt64(i["e2eventstart"].InnerText) * 10000000); //Ticks
                        UnixEpochStart = UnixEpochStart.AddYears(1969);
                        UnixEpochStart = UnixEpochStart.AddDays(-1); //16.5. aus irgend einem Grund muss ein tag abgezogen werden
                        // je nach Sommer oder Winterzeit muss die Uhrzeit korrigiert werden
                        // Ticks uns UnixEpoch sind mindesten 1h unterschiedlich
                        if (vDayLightSaving) { UnixEpochStart = UnixEpochStart.AddHours(2); } else { UnixEpochStart = UnixEpochStart.AddHours(1); }
                        zeit = String.Format("{0} {1:00}:{2:00}", DateTools.PrintDayOfWeek(UnixEpochStart.DayOfWeek), UnixEpochStart.Hour, UnixEpochStart.Minute);
                    }
                    else
                    {
                        zeit = "";
                        startTime = "";
                        UnixEpochStart = new DateTime();
                    }
                    if ((i["e2eventduration"] != null) && ((i["e2eventduration"].InnerText).ToUpper() != "NONE"))
                    {
                        duration = Convert.ToInt64(i["e2eventduration"].InnerText);
                        UnixEpochEnd = UnixEpochStart.AddSeconds(duration); // Dauer zu beginnzeit addieren
                        zeit += " - " + String.Format("{0:00}:{1:00}", UnixEpochEnd.Hour, UnixEpochEnd.Minute);
                    }
                    else
                    {
                        duration = 0;
                    }
                    if (i["e2eventcurrenttime"] != null)
                    {
                        DateTime UnixEpochNow = new DateTime(Convert.ToInt64(i["e2eventcurrenttime"].InnerText) * 10000000); //Ticks;
                        UnixEpochNow = UnixEpochNow.AddYears(1969);
                        UnixEpochNow = UnixEpochNow.AddDays(-1); //16.5. aus irgend einem Grund muss ein tag abgezogen werden
                        if (vDayLightSaving) { UnixEpochNow = UnixEpochNow.AddHours(2); } else { UnixEpochNow = UnixEpochNow.AddHours(1); }
                        if (duration > 0)
                        {
                            progress = Math.Truncate(UnixEpochNow.Subtract(UnixEpochStart).TotalSeconds / duration * 100).ToString();
                        }
                        else
                        {
                            progress = "0";
                        }

                    }
                    else
                    {
                        progress = "";
                    }
                    if ((i["e2eventdescription"] != null) && ((i["e2eventdescription"].InnerText).ToUpper() != "NONE"))
                    {
                        text = i["e2eventdescription"].InnerText;
                    }
                    else { text = ""; }
                    if ((i["e2eventdescriptionextended"] != null) && ((i["e2eventdescriptionextended"].InnerText).ToUpper() != "NONE"))
                    {
                        text += "\n" + i["e2eventdescriptionextended"].InnerText;
                    }
                    ListViewItem listItem = listView1.Items.Add(new ListViewItem(new string[] { "", channelNr, channelName, programm, zeit, tvtId, startTime, progress, text }));
                    if (tvtId != "")
                    {
                        string key = tvtId.Replace(':', '_');
                        key = key.Remove(key.Length - 1, 1); //das letzte _ entfernen
                        //Icon aus der Liste suchen
                        (listItem).ImageIndex = channelImageList.Images.IndexOfKey(key);

                    }
                    else
                    {
                        (listItem).ImageIndex = channelImageList.Images.IndexOfKey("0");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Laden: " + ex.Message);
            }

            lProgress.SetProgress("Fertig ...", 100);
            lProgress.Close();
        }

        //Kanal ändern
        private void ChangeChannelClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0)
            {
                return;
            }

            //zu Sender in der Liste wechseln
            string channelID = listView1.SelectedItems[0].SubItems[5].Text;
            Uri lUri = new Uri(vSenderURL + channelID);
            SendWebRequest(lUri);
            //aktuellen Kanal abfragen
            ReadActChannel();
        }

        //unbekannte Zeichen aus dem Text entfernen
        private string ReplaceChars(string aText)
        {
            aText = aText.Replace("&quot;", "'");
            aText = aText.Replace("\\n", "\n"); //atv sendet manchmal Müll
            return WebUtility.HtmlDecode(aText); 
        }

        private void OnSelectedIndexChange(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0)
            {
                return;
            }
            //Zeit + Info ausgeben
            richTextBox.Text = listView1.SelectedItems[0].SubItems[3].Text + "\n" +
                               listView1.SelectedItems[0].SubItems[4].Text + "\n \n" +
                               ReplaceChars(listView1.SelectedItems[0].SubItems[8].Text);
            //EPG Vorschau updaten
            if (tabControl1.SelectedIndex == 1)
            {
                this.OnUpdateDetailEPG();
            }
        }

        private void OnUpdateDetailEPG()
        {
            epgUserControl.UpdateDetailEPG(listView1.SelectedItems[0].SubItems[5].Text,
                                          listView1.SelectedItems[0].SubItems[2].Text);
        }
        private void OnEPGClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0)
            {
                return;
            }
            //zu Sender EPG Info anzeigen
            string channelNr = listView1.SelectedItems[0].SubItems[1].Text;
            string tvtId = listView1.SelectedItems[0].SubItems[5].Text;
            string channelName = listView1.SelectedItems[0].SubItems[2].Text;
            EPGDetail lEpgForm = new EPGDetail(channelNr, tvtId, channelName);
            lEpgForm.ShowDialog();
        }

        //Timer hinzufügen
        //private void OnAddTimer(object sender, EventArgs e)
        //{
        //    if (listView1.SelectedItems.Count <= 0)
        //    {
        //        return;
        //    }
        //    //Timer hinzufügen
        //    string channelNr = listView1.SelectedItems[0].SubItems[1].Text;
        //    Uri lUri = new Uri(vAddTimerURL + channelNr + 
        //                       "&start-time=" + listView1.SelectedItems[0].SubItems[6].Text + 
        //                       "&end-time=" + Convert.ToString(Convert.ToInt64(listView1.SelectedItems[0].SubItems[6].Text) + 6000));
        //                       //"&tvtv-Id=" + listView1.SelectedItems[0].SubItems[5].Text);

        //    string lresponseFromServer = SendWebRequest(lUri);
        //}

        private void OnUpButtonClick(object sender, EventArgs e)
        {
            GenerateEPGList(NEXT);
        }

        private void OnDownButtonClick(object sender, EventArgs e)
        {
            GenerateEPGList(NOW);
        }

        //Google anzeigen
        private void OnGoogleClick(object sender, EventArgs e)
        {
            string title = listView1.SelectedItems[0].SubItems[3].Text;
            if (title.IndexOf("(") > 0)
            {
                int pos = title.IndexOf("(");
                int len = title.LastIndexOf(")") - pos + 1;
                if (len <= 0) { len = int.MaxValue; }
                title = title.Remove(pos, len);
            }
            title = title.Trim();
            title = title.Replace(" ", "+"); //Leerzeichen werden in + umgewandelt
            Process.Start(vUrlGoogle + title);
        }

        private void listView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            //Hintergrund zeichen
            Color lFarbe = Color.Silver;
            Rectangle rect = new Rectangle(e.Bounds.Left + 1, e.Bounds.Top + 1, e.Bounds.Width - 2, e.Bounds.Height - 2);
            SolidBrush lbr = new SolidBrush(lFarbe);
            e.Graphics.FillRectangle(lbr, rect);
            //Text ausgeben
            StringFormat string_format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            e.Graphics.DrawString(listView1.Columns[e.ColumnIndex].Text, listView1.Font, Brushes.Black, e.Bounds);
        }

        private void DrawSelectedBackGround(DrawListViewSubItemEventArgs e)
        {
            //Rectangle rect = new Rectangle(e.Bounds.Left + 1, e.Bounds.Top + 1, e.Bounds.Width - 2, e.Bounds.Height - 2);
            Rectangle rect = new Rectangle(e.Bounds.Left, e.Bounds.Top, e.Bounds.Width, e.Bounds.Height);
            SolidBrush lbr = new SolidBrush(Color.Blue);
            e.Graphics.FillRectangle(lbr, rect);
        }

        private void DrawBackGround(Boolean even, DrawListViewSubItemEventArgs e)
        {
            //Rectangle rect = new Rectangle(e.Bounds.Left + 1, e.Bounds.Top + 1, e.Bounds.Width - 2, e.Bounds.Height - 2);
            if (even)
            {
                Rectangle rect = new Rectangle(e.Bounds.Left, e.Bounds.Top, e.Bounds.Width, e.Bounds.Height);
                SolidBrush lbr = new SolidBrush(Color.FromArgb(0xed, 0xed, 0xed)); //Color.LightGray
                e.Graphics.FillRectangle(lbr, rect);
            }
        }

        private void DrawCell(Boolean even, string Text, Boolean selected, DrawListViewSubItemEventArgs e, Boolean actualProg)
        {
            //Ausgabeformat
            StringFormat string_format = new StringFormat
            {
                Trimming = StringTrimming.EllipsisWord
            };
            string_format.FormatFlags |= StringFormatFlags.NoWrap;
            string_format.LineAlignment = StringAlignment.Center;
            if (selected)
            {
                DrawSelectedBackGround(e);
                e.Graphics.DrawString(Text, e.Item.ListView.Font, Brushes.White, e.Bounds, string_format);
            }
            else
            {
                //das aktuelle Programm etwas anders zeichnen
                if (actualProg)
                {
                    DrawBackGround(even, e);
                    e.Graphics.DrawString(Text, e.Item.ListView.Font, Brushes.Black, e.Bounds, string_format);
                }
                else
                {
                    DrawBackGround(even, e);
                    e.Graphics.DrawString(Text, e.Item.ListView.Font, Brushes.Blue, e.Bounds, string_format);
                }
            }
        }

        private void listView1_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            // Get the ListView item and the ServerStatus object.    
            ListViewItem item = e.Item;
            ListView lvw = e.Item.ListView;
            Image logo = null;
            //Ausgabeformat
            //StringFormat string_format = new StringFormat();
            //string_format.Trimming = StringTrimming.EllipsisWord;
            //string_format.FormatFlags |= StringFormatFlags.NoWrap;
            //string_format.LineAlignment = StringAlignment.Center;
            //Zeichen Font
            //Font  myFont = new Font(lvw.Font, FontStyle.Regular);
            Boolean selected = !((e.ItemState & ListViewItemStates.Selected) == 0);
            if ((item).ImageIndex >= 0)
            {
                logo = channelImageList.Images[(item).ImageIndex];
            }
            // Draw.    
            switch (e.ColumnIndex)
            {
                case 0:
                    ImageAttributes imageAttributes = new ImageAttributes();
                    float[][] colorMatrixElements = {
                       new float[] {1,  0,  0,  0, 0},        // red scaling factor of 2
                       new float[] {0,  1,  0,  0, 0},        // green scaling factor of 1
                       new float[] {0,  0,  2,  0, 0},        // blue scaling factor of 1
                       new float[] {0,  0,  0,  1, 0},        // alpha scaling factor of 1
                       new float[] {.2f, .2f, .2f, 0, 1}};    // three translations of 0.2

                    ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);
                    imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                    // Draw logo.            
                    if (logo != null)
                    {
                        float scale = e.Bounds.Height / (float)logo.Height;
                        e.Graphics.ScaleTransform(scale, scale);
                        e.Graphics.TranslateTransform(e.Bounds.Left, e.Bounds.Top + (e.Bounds.Height - logo.Height * scale) / 2, System.Drawing.Drawing2D.MatrixOrder.Append);
                        if (selected)
                        {
                            //e.Graphics.DrawImage(logo, 0, 0);
                            //e.Graphics.DrawImage(logo, new Rectangle(0, 0, logo.Width, logo.Height), 0, 0, logo.Width, logo.Height, GraphicsUnit.Pixel, imageAttributes);
                            Bitmap logo2 = (Bitmap)logo.Clone();
                            Color c;
                            for (int i = 0; i < logo2.Width; i++)
                            {
                                for (int j = 0; j < logo2.Height; j++)
                                {
                                    c = logo2.GetPixel(i, j);
                                    logo2.SetPixel(i, j, Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B));
                                }
                            }
                            //Hintergrund blau zeichnen
                            //DrawBackGround(e);
                            //invertiertes Logo zeichnen
                            e.Graphics.DrawImage(logo2, 0, 0);
                        }
                        else
                        {
                            e.Graphics.DrawImage(logo, 0, 0);
                        }
                    }
                    break;
                case 1:
                    // Draw the Channel Nr. 
                    DrawCell(Converters.NumberIsEven(item.SubItems[1].Text), item.SubItems[1].Text, selected, e, toolStripStatusLabel.Text.IndexOf(item.SubItems[2].Text) < 0);
                    break;
                case 2:
                    // Draw the Channel Name 
                    DrawCell(Converters.NumberIsEven(item.SubItems[1].Text), item.SubItems[2].Text, selected, e, toolStripStatusLabel.Text.IndexOf(item.SubItems[2].Text) < 0);
                    break;
                case 3:
                    // Draw Programm Name  
                    DrawCell(Converters.NumberIsEven(item.SubItems[1].Text), item.SubItems[3].Text, selected, e, toolStripStatusLabel.Text.IndexOf(item.SubItems[2].Text) < 0);
                    break;
                case 4:
                    // Draw Start/End Time
                    int lProz = 0;
                    if (item.SubItems[7].Text != "")
                    {
                        lProz = Convert.ToInt16(item.SubItems[7].Text);
                    }

                    if (lProz > 0)
                    {
                        Color lFarbe = Color.Gray;
                        Rectangle rect = new Rectangle(e.Bounds.Left + 1, e.Bounds.Top + 1, e.Bounds.Width - 2, e.Bounds.Height - 3);
                        SolidBrush lbr = new SolidBrush(lFarbe);
                        //Rahmen zeichen
                        Pen lPen = new Pen(lFarbe);
                        e.Graphics.DrawRectangle(lPen, rect);
                        //Füllung berechnen und zeichen
                        int width = (e.Bounds.Width - 2) * lProz / 100;
                        Rectangle rectProz = new Rectangle(e.Bounds.Left + 1, e.Bounds.Top + 1, width, e.Bounds.Height - 3);
                        e.Graphics.FillRectangle(lbr, rectProz);
                    }
                    //Text ausgeben
                    //Color pen_color = Color.FromArgb(255, 255 - lFarbe.R, 255 - lFarbe.G, 255 - lFarbe.B);
                    Color pen_color = Color.Black;
                    using (SolidBrush br = new SolidBrush(pen_color))
                    {
                        StringFormat string_center = new StringFormat
                        {
                            Alignment = StringAlignment.Center,
                            LineAlignment = StringAlignment.Center
                        };
                        e.Graphics.DrawString(item.SubItems[4].Text, lvw.Font, br, e.Bounds, string_center);
                    }
                    break;

            }
            // Draw the focus rectangle if appropriate.   
            e.Graphics.ResetTransform();
            if (lvw.FullRowSelect)
            {
                e.DrawFocusRectangle(e.Item.Bounds);
            }
        }

        private void streamAnsehenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string stream = vStreamUrl + listView1.SelectedItems[0].SubItems[5].Text;
            try
            {
                Process.Start(vMediaPlayer, stream);
                //VLCForm lForm = new VLCForm(stream);
                //lForm.Show();
            }
            // Most specific:
            catch (Win32Exception ex)
            {
                MessageBox.Show("Streaming Client konnte nicht gefunden werden: " + ex.Message);
            }
            // Least specific:
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim öffnen des Streaming Clients: " + ex.Message);
            }
        }

        private void tvInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0) { return; }
            string lKanal = listView1.SelectedItems[0].SubItems[2].Text;
            IniFile lIni = new IniFile();
            var lChannel = lIni.Read(lKanal, "TVINFO");
            var lUrl = lIni.Read("TVINFO", "Webbroser");
            //Rückfallsebene, falls in der Ini nichts gefunden wird
            if (lChannel == "") lChannel = lKanal;
            Process.Start(lUrl + lChannel);
        }

        //Timer auslösen
        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            //sichern der aktuellen Position
            Int32 lIndex = -1;
            if (listView1.SelectedItems.Count > 0)
            {
                lIndex = listView1.SelectedItems[0].Index;
            }
            vTime = DateTime.Now;
            timeBox.Text = String.Format("{0:00}:{1:00}", vTime.Hour, vTime.Minute);
            this.GenerateEPGList(NOW);
            //setzen der neuen Position nach dem Reload
            if (lIndex != -1)
            {
                this.listView1.Items[lIndex].Focused = true;
                this.listView1.Items[lIndex].Selected = true;
            }
            ReadActChannel();
        }

        //Timer um die Zeit in der Combobox immer akutell zu halten
        private void updateTimer_Tick(object sender, EventArgs e)
        {
            vTime = DateTime.Now;
            timeBox.Text = String.Format("{0:00}:{1:00}", vTime.Hour, vTime.Minute);
        }

        //IMDb Daten anzeigen
        private void iMDbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string title = listView1.SelectedItems[0].SubItems[3].Text;
            if (title.IndexOf("(") > 0)
            {
                int pos = title.IndexOf("(");
                int len = title.LastIndexOf(")") - pos + 1;
                if (len <= 0) { len = int.MaxValue; }
                title = title.Remove(pos, len);
            }
            title = title.Trim();
            title = title.Replace(" ", "+"); //Leerzeichen werden in + umgewandelt
            Process.Start(vUrlIMDb + title);
        }

        //aktuelles Programm auslesen
        private void ReadActChannel()
        {
            Uri lUri = new Uri(vGetChannelURL);
            string lresponseFromServer = SendWebRequest(lUri);
            if (lresponseFromServer == "") return;
            //XML Elemente durchgehen
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(lresponseFromServer);
            XmlElement root = doc.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("./e2service");
            try
            {
                string title = "";
                foreach (XmlNode i in nodes)
                {
                    //var encoder = ASCIIEncoding.GetEncoding("us-ascii", new EncoderReplacementFallback(string.Empty), new DecoderExceptionFallback());
                    //title = encoder.GetString(encoder.GetBytes(i["e2servicename"].InnerText)); 

                    title = i["e2servicename"].InnerText;
                    //Unicode Steuerzeichen entfernen
                    title = ReplaceUnicodeChars(title);
                }
                toolStripStatusLabel.Text = "Aktueller Kanal: " + title;
                statusStrip.Refresh();
            }
            catch
            {
                toolStripStatusLabel.Text = "";
                statusStrip.Refresh();
            }
            ReadAudioDelayed();
        }

        //unbekannte Zeichen aus dem Titel entfernen
        private string ReplaceUnicodeChars(string aTitle)
        {
            aTitle = aTitle.Replace("\u0080", "");
            aTitle = aTitle.Replace("\u0081", "");
            aTitle = aTitle.Replace("\u0082", "");
            aTitle = aTitle.Replace("\u0083", "");
            aTitle = aTitle.Replace("\u0084", "");
            aTitle = aTitle.Replace("\u0085", "");
            aTitle = aTitle.Replace("\u0086", "");
            aTitle = aTitle.Replace("\u0087", "");
            aTitle = aTitle.Replace("\u0088", "");
            aTitle = aTitle.Replace("\u0089", "");
            aTitle = aTitle.Replace("\u008a", "");
            aTitle = aTitle.Replace("\u008b", "");
            aTitle = aTitle.Replace("\u008c", "");
            aTitle = aTitle.Replace("\u008d", "");
            aTitle = aTitle.Replace("\u008e", "");
            aTitle = aTitle.Replace("\u008f", "");
            aTitle = aTitle.Replace("\u0090", "");
            aTitle = aTitle.Replace("\u0091", "");
            aTitle = aTitle.Replace("\u0092", "");
            aTitle = aTitle.Replace("\u0093", "");
            aTitle = aTitle.Replace("\u0094", "");
            aTitle = aTitle.Replace("\u0095", "");
            aTitle = aTitle.Replace("\u0096", "");
            aTitle = aTitle.Replace("\u0097", "");
            aTitle = aTitle.Replace("\u0098", "");
            aTitle = aTitle.Replace("\u0099", "");
            aTitle = aTitle.Replace("\u009a", "");
            aTitle = aTitle.Replace("\u009b", "");
            aTitle = aTitle.Replace("\u009c", "");
            aTitle = aTitle.Replace("\u009d", "");
            aTitle = aTitle.Replace("\u009e", "");
            aTitle = aTitle.Replace("\u009f", "");
            aTitle = aTitle.Replace("&quot;", "'");
            return aTitle;
        }
        //Änderung des Bouquets
        private void serviceBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Kanalliste erstellen
            this.GenerateEPGList(NOW);

            //aktuellen Kanal ermitteln
            ReadActChannel();
        }

        //Hint
        private void upButton_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.nextButton, "nächstes Programm");
        }

        //Hint
        private void prevButton_MouseHover(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.prevButton, "laufendes Programm");
        }

        //
        private void ePGSucheToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0)
            {
                return;
            }
            //zu Sender EPG Info anzeigen
            string searchTitle = listView1.SelectedItems[0].SubItems[3].Text;
            EPGSearch lEpgForm = new EPGSearch(searchTitle);
            lEpgForm.ShowDialog();
        }

        //Audio Info einlesen
        private void ReadAudio()
        {
            Uri lUri = new Uri(vGetAudioURL);
            string lresponseFromServer = SendWebRequest(lUri);
            audioMenuStrip.Items.Clear();
            if (lresponseFromServer == "")
            {
                audioLabel.Text = "Audio: Stereo";
                return;
            }
            //XML Elemente durchgehen
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(lresponseFromServer);
            XmlElement root = doc.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("./e2audiotrack");
            try
            {
                foreach (XmlNode i in nodes)
                {
                    audioMenuStrip.Items.Add(i["e2audiotrackdescription"].InnerText);
                    if (i["e2audiotrackactive"].InnerText.ToUpper() == "TRUE")
                    {
                        audioLabel.Text = "Audio: " + i["e2audiotrackdescription"].InnerText;
                        ((ToolStripMenuItem)audioMenuStrip.Items[audioMenuStrip.Items.Count - 1]).Checked = true;
                    }
                    audioMenuStrip.Items[audioMenuStrip.Items.Count - 1].Tag = i["e2audiotrackid"].InnerText;
                    audioMenuStrip.Items[audioMenuStrip.Items.Count - 1].Click += SwitchAudio;
                }
            }
            catch
            {
            }
        }

        //Audio verzögert lesen
        private void ReadAudioDelayed()
        {
            audioLabel.Text = "Audio: --";
            audioTimer.Enabled = false;
            audioTimer.Interval = 2000;
            //audioTimer.Tick += audioTimer_Tick;
            audioTimer.Enabled = true;

        }

        private void audioTimer_Tick(object sender, EventArgs e)
        {
            audioTimer.Enabled = false;
            ReadAudio();
        }


        //auf anderen Audio Kanal umschalten
        private void SwitchAudio(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (sender as ToolStripMenuItem);
            string pid = item.Tag.ToString();
            Uri lUri = new Uri(vSetAudioURL + pid);
            string lresponseFromServer = SendWebRequest(lUri);
            //mir egal was zurückkommt, wir werten das nicht aus

            ReadAudio();
        }

        private void toolStripStatusLabel_Click(object sender, EventArgs e)
        {
            int lIndex = -1;
            int lLength = 0;
            string lChannel = toolStripStatusLabel.Text.Substring(17);
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                //Ist der Name im Kanal enthalten
                if (lChannel.IndexOf(listView1.Items[i].SubItems[2].Text) >= 0)
                {
                    //wie viele Zeichen wurden im Namen gefunden
                    if (listView1.Items[i].SubItems[2].Text.Length > lLength)
                    {
                        lIndex = i;
                        lLength = listView1.Items[i].SubItems[2].Text.Length;
                    }
                }
            }
            if (lIndex >= 0)
            {
                listView1.EnsureVisible(lIndex); //damit auf das Item gescrollt wird
                listView1.Items[lIndex].Focused = true;
                listView1.Items[lIndex].Selected = true;
                listView1.Select(); //Focus, damit das Item blau gezeichnet wird
            }
        }

        private void richTextBox_Resize(object sender, EventArgs e)
        {
            epgButton.Left = richTextBox.Size.Width - epgButton.Width - 19;
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //EPG Vorschau updaten
            if (tabControl1.SelectedIndex == 1)
            {
                this.OnUpdateDetailEPG();
            }
        }
    }
}
