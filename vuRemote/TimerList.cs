using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Xml;

namespace vuRemote
{
    public partial class TimerList : Form
    {
        private string vTimerURL;
        private string vDeleteTimerURL;
        private string vToggleTimerURL;
        private Boolean vDayLightSaving;

        public TimerList()
        {
            InitializeComponent();

            //Adresse einlesen
            IniFile lIni = new IniFile();
            var lip = lIni.Read("IP", "Receiver");
            vTimerURL = "http://" + lip + "/web/timerlist";
            vDeleteTimerURL = "http://" + lip + "/api/timerdelete?sRef="; //&begin={begin}&end={end}";
            vToggleTimerURL = "http://" + lip + "/api/timeronoff?sRef="; //&begin={begin}&end={end} 

            vDayLightSaving = TimeZoneInfo.Local.IsDaylightSavingTime(DateTime.Now);
            //Spalten generieren
            this.TimerListView.Clear();
            this.TimerListView.Columns.Add("1", "Icon", 48);
            this.TimerListView.Columns.Add("2", "Nr.", 30);
            this.TimerListView.Columns.Add("3", "Name", 120);
            this.TimerListView.Columns.Add("4", "Programm", 260);
            this.TimerListView.Columns.Add("5", "Zeit", 100);
            this.TimerListView.Columns.Add("6", "Aktiv", 50);
            //this.TimerListView.Columns.Add("7", "tvtvId", 0);
            //this.TimerListView.Columns.Add("8", "Timer", 0);
            //this.TimerListView.Columns.Add("9", "Icon Genre", 0);

            //Icons einlesen
            ReadImages();

            GenerateTimerTable();
            //GenerateSeriesTimerTable();
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
            lRequest.Timeout = 20000;
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
                MessageBox.Show("Request timed out");
                return ""; //Programm verlassen

            }
        }

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

        private void GenerateTimerTable()
        {
            Uri lUri = new Uri(vTimerURL);
            string lResponse = SendWebRequest(lUri);
            if (lResponse == "") { return; }
            //XML Elemente durchgehen
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(lResponse);
            XmlElement root = doc.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("./e2timer");

            try
            {
                string channelNr;
                string channelName;
                string tvtId = "";
                string programm = "";
                string zeit = "";
                string startTime = "";
                string endTime = "";
                string text = "";
                string active = "";
                DateTime UnixEpochStart;
                DateTime UnixEpochEnd;
                int channel = 1;
                Int64 duration = 0;
                foreach (XmlNode i in nodes)
                {
                    channelNr = channel.ToString();
                    channel++;
                    channelName = i["e2servicename"].InnerText;
                    tvtId = i["e2servicereference"].InnerText;
                    if (i["e2name"] != null) { programm = i["e2name"].InnerText; } else { programm = ""; }
                    //beginzeit
                    if ((i["e2timebegin"] != null) && ((i["e2timebegin"].InnerText).ToUpper() != "NONE"))
                    {
                        startTime = i["e2timebegin"].InnerText;
                        UnixEpochStart = new DateTime(Convert.ToInt64(i["e2timebegin"].InnerText) * 10000000); //Ticks
                        UnixEpochStart = UnixEpochStart.AddYears(1969);
                        UnixEpochStart = UnixEpochStart.AddDays(-1); //16.5. aus irgend einem Grund muss ein tag abgezogen werden
                        // je nach Sommer oder Winterzeit muss die Uhrzeit korrigiert werden
                        // Ticks uns UnixEpoch sind mindesten 1h unterschiedlich
                        if (vDayLightSaving) { UnixEpochStart = UnixEpochStart.AddHours(2); } else { UnixEpochStart = UnixEpochStart.AddHours(1); }
                        zeit = String.Format("{0} {1:00}:{2:00}", PrintDayOfWeek(UnixEpochStart.DayOfWeek), UnixEpochStart.Hour, UnixEpochStart.Minute);
                    }
                    else
                    {
                        zeit = "";
                        startTime = "";
                        UnixEpochStart = new DateTime();
                    }
                    //endezeit
                    if ((i["e2timeend"] != null) && ((i["e2timeend"].InnerText).ToUpper() != "NONE"))
                    {
                        endTime = i["e2timeend"].InnerText;
                    }
                    else
                    {
                        endTime = "";
                    }
                    //Dauer
                    if ((i["e2duration"] != null) && ((i["e2duration"].InnerText).ToUpper() != "NONE"))
                    {
                        duration = Convert.ToInt64(i["e2duration"].InnerText);
                        UnixEpochEnd = UnixEpochStart.AddSeconds(duration); // Dauer zu beginnzeit addieren
                        zeit += " - " + String.Format("{0:00}:{1:00}", UnixEpochEnd.Hour, UnixEpochEnd.Minute);
                    }
                    else
                    {
                        duration = 0;
                    }
                    //aktiv
                    if ((i["e2disabled"] != null) && ((i["e2disabled"].InnerText).ToUpper() != "1"))
                    {
                        active = "aktiv";
                    }
                    else
                    {
                        active = "inaktiv";
                    }

                    //if (i["e2eventcurrenttime"] != null)
                    //{
                    //    DateTime UnixEpochNow = new DateTime(Convert.ToInt64(i["e2eventcurrenttime"].InnerText) * 10000000); //Ticks;
                    //    UnixEpochNow = UnixEpochNow.AddYears(1969);
                    //    if (vDayLightSaving) { UnixEpochNow = UnixEpochNow.AddHours(2); } else { UnixEpochNow = UnixEpochNow.AddHours(1); }
                    //    if (duration > 0)
                    //    {
                    //        progress = Math.Truncate(UnixEpochNow.Subtract(UnixEpochStart).TotalSeconds / duration * 100).ToString();
                    //    }
                    //    else
                    //    {
                    //        progress = "0";
                    //    }

                    //}
                    //else
                    //{
                    //    progress = "";
                    //}
                    if (i["e2description"] != null) { text = i["e2description"].InnerText; } else { text = ""; }
                    if (i["e2descriptionextended"] != null) { text += "\n" + i["e2descriptionextended"].InnerText; }
                    //ListViewItem listItem = listView1.Items.Add(new ListViewItem(new string[] { "", channelNr, channelName, programm, zeit, tvtId, startTime, progress, text }));
                    //Element hinzufügen
                    //ListViewItem listItem = TimerListView.Items.Add(new ListViewItem(new string[] { "" /*channelIcon*/, channelNr, channelName, programm, tag + " " + zeit, tvtId, timerNo, catIcon }));
                    ListViewItem listItem = TimerListView.Items.Add(new ListViewItem(new string[] { "" /*channelIcon*/, channelNr, channelName, programm, zeit, active, tvtId, "", text, startTime, endTime }));

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
            catch
            {
            }


        }


        private void OnTimerDelete(object sender, EventArgs e)
        {
            if (TimerListView.SelectedItems.Count <= 0)
            {
                return;
            }
            //Timer löschen
            Uri lUri = new Uri(vDeleteTimerURL + TimerListView.SelectedItems[0].SubItems[6].Text + 
                              "&begin=" + TimerListView.SelectedItems[0].SubItems[9].Text +
                              "&end=" + TimerListView.SelectedItems[0].SubItems[10].Text);
            string lresponseFromServer = SendWebRequest(lUri);
            //den Response noch bearbeiten: wenn Timer gesetzt kommt, dann auch den Eintrag auf Rec umstellen
            if (lresponseFromServer.IndexOf("deleted successfully") > 0)
            {
                //Timer gesetzt
                MessageBox.Show("Timer wurde gelöscht");
                //Eintrag löschen
                TimerListView.Items.Remove(TimerListView.SelectedItems[0]);
            }
            else
            {
                //Fehler
                MessageBox.Show("Timer konnte nicht gelöscht werden");
            }
        }


        private string PrintDayOfWeek(DayOfWeek day)
        {
            switch (day){
                case DayOfWeek.Monday: 
                    return "Mo";
                case DayOfWeek.Tuesday:
                    return "Di";
                case DayOfWeek.Wednesday:
                    return "Mi";
                case DayOfWeek.Thursday:
                    return "Do";
                case DayOfWeek.Friday:
                    return "Fr";
                case DayOfWeek.Saturday:
                    return "Sa";
                case DayOfWeek.Sunday:
                    return "So";
            }
            return "  ";

        }

        private void TimerListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TimerListView.SelectedItems.Count <= 0)
            {
                return;
            }
            //Zeit + Info ausgeben
            textBox.Text = TimerListView.SelectedItems[0].SubItems[4].Text + "\n" + TimerListView.SelectedItems[0].SubItems[8].Text; 

        }

        private void timerDeaktivierenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TimerListView.SelectedItems.Count <= 0) return;
            
            //Timer aktivieren/deaktivieren
            Uri lUri = new Uri(vToggleTimerURL + TimerListView.SelectedItems[0].SubItems[6].Text +
                              "&begin=" + TimerListView.SelectedItems[0].SubItems[9].Text +
                              "&end=" + TimerListView.SelectedItems[0].SubItems[10].Text);
            string lresponseFromServer = SendWebRequest(lUri);
            //den Response noch bearbeiten: wenn Timer gesetzt kommt, dann auch den Eintrag auf Rec umstellen
            if (lresponseFromServer.IndexOf("deleted successfully") > 0)
            {
                //Timer gesetzt
                MessageBox.Show("Timer wurde gelöscht");
            }
            else
            {
                //Fehler
                MessageBox.Show("Timer konnte nicht gelöscht werden");
            }

        }
    }
}
