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
    public partial class EPGDetail : Form
    {
        private string vURL;
        private string vAddTimerURL;
        private string vDeleteTimerURL;
        private string vserviceRef;
        private Boolean vDayLightSaving;

        public EPGDetail(string channelNo, string tvtId, string channelName)
        {
            InitializeComponent();
            //Variablen anlegen
            IniFile lIni = new IniFile();
            var lBaseUrl = "http://" + lIni.Read("IP", "Receiver");
            vURL = lBaseUrl + "/web/epgservice?sRef=" + tvtId;
            vAddTimerURL = lBaseUrl + "/web/timeraddbyeventid?sRef={serviceRef}&eventid={eventID}&justplay={justplay}";
            //vDeleteTimerURL = lBaseUrl + "/DeleteTimer/";
            //Name setzen
            this.Text = "EPG - " + channelName;
            this.vserviceRef = tvtId;

            vDayLightSaving = TimeZoneInfo.Local.IsDaylightSavingTime(DateTime.Now);
            //Columns anlegen
            listView1.Columns.Add("1", "Icon", 40);
            listView1.Columns.Add("2", "Programm", 260);
            listView1.Columns.Add("3", "Zeit", 100);
            listView1.Columns.Add("4", "TimerNr.", 0);
            //listView1.Columns.Add("4", "tvtvId", 60);
            //listView1.Columns.Add("5", "progId", 60);

            createDetailEPG();
        }

        //an eine Webadresse einen REquest schicken und das Ergebnis als String zurückliefern
        private string SendWebRequest(Uri aURL)
        {
            string lresponseFromServer = "";

            WebRequest lRequest = WebRequest.Create(aURL);
            ((HttpWebRequest)lRequest).UserAgent = "Eigenbau";
            lRequest.Method = "GET";
            lRequest.ContentType = "text/html";
            //Stream ldataStream = lrequest.GetRequestStream();
            // Set the 'Timeout' property in Milliseconds.
            lRequest.Timeout = 10000;
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

        private string PrintDayOfWeek(DayOfWeek day)
        {
            switch (day)
            {
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

        private void createDetailEPG()
        {
            Uri lUri = new Uri(vURL);
            string lResponse = SendWebRequest(lUri);
            if (lResponse == "") { return; }
            //XML Elemente durchgehen
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(lResponse);
            XmlElement root = doc.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("./e2event");
            try
            {
                string title = "";
                //string catIcon = "";
                string zeit = "";
                string startTime = "";
                string progId = "";
                //float zeitPzt = 0;
                string timerNo = "";
                string text = "";
                DateTime UnixEpochStart;
                DateTime UnixEpochEnd;
                foreach (XmlNode i in nodes)
                {
                    title = i["e2eventtitle"].InnerText;
                    //if (i["progress"] != null) { zeitPzt = (float)Convert.ToDouble(i["progress"].InnerText); } else { zeitPzt = 0; }
                    if (i["e2eventid"] != null) { progId = i["e2eventid"].InnerText; } else { progId = ""; }
                    //if (i["catIcon"] != null) { 
                    //    catIcon = i["catIcon"].InnerText;
                    //    catIcon = catIcon.Replace("/icon/", "");
                    //} else { 
                    //    catIcon = ""; 
                    //}
                    if (i["e2eventstart"] != null)
                    {
                        startTime = i["e2eventstart"].InnerText;
                        UnixEpochStart = new DateTime(Convert.ToInt64(i["e2eventstart"].InnerText) * 10000000); //Ticks
                        UnixEpochStart = UnixEpochStart.AddYears(1969);
                        UnixEpochStart = UnixEpochStart.AddDays(-1); //16.5. aus irgend einem Grund muss ein tag abgezogen werden
                        if (vDayLightSaving) { UnixEpochStart = UnixEpochStart.AddHours(2); } else { UnixEpochStart = UnixEpochStart.AddHours(1); }// Time Offset
                        zeit = String.Format("{0} {1:00}:{2:00}", PrintDayOfWeek(UnixEpochStart.DayOfWeek), UnixEpochStart.Hour, UnixEpochStart.Minute);
                    }
                    else
                    {
                        zeit = "";
                        startTime = "";
                        UnixEpochStart = new DateTime();
                    }
                    if (i["e2eventduration"] != null)
                    {
                        UnixEpochEnd = UnixEpochStart.AddSeconds(Convert.ToInt64(i["e2eventduration"].InnerText)); // Dauer zu beginnzeit addieren
                        zeit += " - " + String.Format("{0:00}:{1:00}", UnixEpochEnd.Hour, UnixEpochEnd.Minute);
                    }
                    if (i["e2eventdescription"] != null) { text = i["e2eventdescription"].InnerText; } else { text = ""; }
                    if (i["e2eventdescriptionextended"] != null) { text += "\n" + i["e2eventdescriptionextended"].InnerText; } 
                    //if (i["timerNo"] != null) { timerNo = i["timerNo"].InnerText; } else { timerNo = ""; }
                    ListViewItem listItem = listView1.Items.Add(new ListViewItem(new string[] { "", title, zeit, progId, startTime, timerNo, text }));
                    //Kategorie Icon auswählen
                    //if (catIcon != ""){ (listItem).ImageIndex = Convert.ToInt16(catIcon); } else { (listItem).ImageIndex = 0; }

                }
            }
            catch
            {
            }
        }

        private void OnAddTimerClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0)
            {
                return;
            }
            //Timer hinzufügen
            string vTimerURL = vAddTimerURL;
            vTimerURL = vTimerURL.Replace("{serviceRef}", vserviceRef);
            vTimerURL = vTimerURL.Replace("{eventID}", listView1.SelectedItems[0].SubItems[3].Text);
            vTimerURL = vTimerURL.Replace("{justplay}","0");
            Uri lUri = new Uri(vTimerURL);
            string lresponseFromServer = SendWebRequest(lUri); 
            //den Response noch bearbeiten: wenn Timer gesetzt kommt, dann auch den Eintrag auf Rec umstellen
            if (lresponseFromServer.IndexOf(" zugefügt") > 0)
            {
                //Timer gesetzt
                MessageBox.Show("Timer wurde gesetzt");
                listView1.SelectedItems[0].ImageIndex = 4; //Rec Icon anzeigen
            }
            else
            {
                //Fehler
                MessageBox.Show("Timer konnte nicht gesetzt werden");
            }
        }

        private void contextMenu_Opening(object sender, CancelEventArgs e)
        {
            if (listView1.SelectedItems[0].SubItems[5].Text != "")
            {
                //Aufname Icon gefunden, nur löschen aktiv schalten
                timerHinzufuegen.Visible = false;
            }
            else
            {
                //kein Aufname Icon gefunden, daher nur Aufnahme aktiv
                timerHinzufuegen.Visible = true;
            }

        }

        private void timerLoeschen_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0)
            {
                return;
            }
            //Timer löschen
            Uri lUri = new Uri(vDeleteTimerURL + listView1.SelectedItems[0].SubItems[4].Text);
            string lresponseFromServer = SendWebRequest(lUri);
            //den Response noch bearbeiten: wenn Timer gesetzt kommt, dann auch den Eintrag auf Rec umstellen
            if (lresponseFromServer.IndexOf("Timer gel&ouml;scht!") > 0)
            {
                //Timer gesetzt
                MessageBox.Show("Timer wurde gelöscht");
                listView1.SelectedItems[0].ImageIndex = 0; //Icon löschen
            }
            else
            {
                //Fehler
                MessageBox.Show("Timer konnte nicht gelöscht werden");
            }
        }

        //EPG Text laden und anzeigen
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0)
            {
                return;
            }
            //Zeit + Info ausgeben
            richTextBox.Text = listView1.SelectedItems[0].SubItems[2].Text + "\n" + listView1.SelectedItems[0].SubItems[6].Text; 
        }
    }
}
