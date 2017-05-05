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


namespace vuRemote
{
    public partial class RemoteForm : Form
    {
        private RemoteSender vSender;

        public RemoteForm()
        {
            InitializeComponent();
            vSender = new RemoteSender();
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
            setting lSettings = new setting();
            lSettings.ShowDialog();
        }

        private void ControlButton_Click(object sender, EventArgs e)
        {
            ImageButton button;
            if (sender is ImageButton)
            {
                button = (ImageButton)sender;
                string lCode = "";
                Boolean blong = false;
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

    }
}
