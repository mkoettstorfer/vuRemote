using System;
using System.IO;
using System.Net;
using System.Web;
using System.Windows.Forms;
using System.Xml;

namespace vuRemote
{
    public partial class EPGUserControl : UserControl
    {
        private string vURL;
        private readonly string vAddTimerURL;
        private readonly string vDeleteTimerURL;
        private string vserviceRef;
        private readonly Boolean vDayLightSaving;
        private readonly string vBaseUrl;

        public EPGUserControl()
        {
            InitializeComponent();
            //Variablen anlegen
            IniFile lIni = new IniFile();
            vBaseUrl = "http://" + lIni.Read("IP", "Receiver");
            vAddTimerURL = vBaseUrl + "/web/timeraddbyeventid?sRef={serviceRef}&eventid={eventID}&justplay={justplay}";
            vDayLightSaving = TimeZoneInfo.Local.IsDaylightSavingTime(DateTime.Now);
            //Columns anlegen
            listView1.Columns.Add("1", "Icon", 40);
            listView1.Columns.Add("2", "Programm", 260);
            listView1.Columns.Add("3", "Zeit", 100);
            listView1.Columns.Add("4", "TimerNr.", 0);
            //listView1.Columns.Add("4", "tvtvId", 60);
            //listView1.Columns.Add("5", "progId", 60);

            //auf den 1. Eintrag stellen
            if (this.listView1.Items.Count > 0)
            {
                this.listView1.Items[0].Focused = true;
                this.listView1.Items[0].Selected = true;
            }
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

        public void UpdateDetailEPG(string tvtId, string channelName)
        {
            //Initialisierung
            this.vserviceRef = tvtId;
            vURL = vBaseUrl + "/web/epgservice?sRef=" + tvtId;

            //Liste leeren
            listView1.Items.Clear();

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
                    //Titel auslesen und HTMLKonvertierung durchführen
                    title = i["e2eventtitle"].InnerText;
                    StringWriter myWriter = new StringWriter();
                    HttpUtility.HtmlDecode(title, myWriter);
                    title = myWriter.ToString();
                    if (i["e2eventid"] != null) { progId = i["e2eventid"].InnerText; } else { progId = ""; }
                      if (i["e2eventstart"] != null)
                    {
                        startTime = i["e2eventstart"].InnerText;
                        UnixEpochStart = new DateTime(Convert.ToInt64(i["e2eventstart"].InnerText) * 10000000); //Ticks
                        UnixEpochStart = UnixEpochStart.AddYears(1969);
                        UnixEpochStart = UnixEpochStart.AddDays(-1); //16.5. aus irgend einem Grund muss ein tag abgezogen werden
                        if (vDayLightSaving) { UnixEpochStart = UnixEpochStart.AddHours(2); } else { UnixEpochStart = UnixEpochStart.AddHours(1); }// Time Offset
                        zeit = String.Format("{0} {1:00}:{2:00}", DateTools.PrintDayOfWeek(UnixEpochStart.DayOfWeek), UnixEpochStart.Hour, UnixEpochStart.Minute);
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
                }
            }
            catch
            {
            }
        }

    }
}
