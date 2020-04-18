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
using System.Web;
using System.Diagnostics;

namespace vuRemote
{
    public partial class ArchivList : Form
    {
        private readonly string vMediaURL;
        private readonly string vStreamUrl;
        private readonly string vMediaPlayer;
        private readonly string vDownloadUrl;
        private readonly string vDeleteUrl;
        private readonly string vRenameUrl;

        public ArchivList()
        {
            InitializeComponent();

            //Adresse einlesen
            IniFile lIni = new IniFile();
            var lip = lIni.Read("IP", "Receiver");
            vMediaURL = "http://" + lip + "/web/movielist";
            vStreamUrl = "http://" + lip + "/web/ts.m3u?file=";
            vMediaPlayer = lIni.Read("EXE", "Mediaplayer");
            vDownloadUrl = "http://" + lip + "/file?action=download&file=";
            vDeleteUrl = "http://" + lip + "/web/moviedelete?sRef=";
            vRenameUrl = "http://" + lip + "/web/movierename?sRef="; // + sRef + "&newname=" + newname);

            this.ArchiveListView.Clear();
            this.ArchiveListView.Columns.Add("1", "Kanal", 100);
            this.ArchiveListView.Columns.Add("2", "Titel", 200);
            this.ArchiveListView.Columns.Add("3", "Beschreibung", 200);
            this.ArchiveListView.Columns.Add("4", "Datum", 100);
            this.ArchiveListView.Columns.Add("5", "Dauer", 100);
            this.ArchiveListView.Columns.Add("6", "Größe", 100);

            GenerateArchivTable();
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

                
        private void GenerateArchivTable()
        {
            Uri lUri = new Uri(vMediaURL);
            string lresponseFromServer = SendWebRequest(lUri);
            if (lresponseFromServer == "") return;
            //Liste leeren
            ArchiveListView.Items.Clear();
            //XML Elemente durchgehen
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(lresponseFromServer);
            XmlElement root = doc.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("./e2movie");
            try
            {
                string title = "";
                string zeit = "";
                string startTime = "";
                string duration = "";
                string channelName = "";
                string filename = "";
                string lDesc = "";
                string lExtDesc = "";
                string lFilesize = "";
                string lServiceRef = "";
                DateTime UnixEpoch;
                foreach (XmlNode i in nodes)
                {
                    title = i["e2title"].InnerText;
                    if (i["e2servicename"] != null) { channelName = i["e2servicename"].InnerText; } else { channelName = ""; }

                    if (i["e2time"] != null)
                    {
                        startTime = i["e2time"].InnerText;
                        UnixEpoch = new DateTime(Convert.ToInt64(i["e2time"].InnerText) * 10000000); //Ticks
                        UnixEpoch = UnixEpoch.AddHours(1); //Timeoffset 1 Stunde addieren
                        UnixEpoch = UnixEpoch.AddYears(1969); //TimeOffset, das richtige Jahr auswählen
                        UnixEpoch = UnixEpoch.AddDays(-1); //16.5. aus irgend einem Grund muss ein tag abgezogen werden
                        zeit = String.Format("{0} {1:00}.{2:00}.{3:00} {4:00}:{5:00}", DateTools.PrintDayOfWeek(UnixEpoch.DayOfWeek), UnixEpoch.Day, UnixEpoch.Month, UnixEpoch.Year - 2000, UnixEpoch.Hour, UnixEpoch.Minute);
                    }
                    else
                    {
                        zeit = "";
                        startTime = "";
                    }
                    if (i["e2length"] != null) { duration = i["e2length"].InnerText + " min"; }
                    if (i["e2description"] != null) { lDesc = i["e2description"].InnerText; } else { lDesc = ""; }
                    if (i["e2descriptionextended"] != null) { lExtDesc = lDesc + "\n" + i["e2descriptionextended"].InnerText; }
                    if (i["e2filename"] != null) { filename = i["e2filename"].InnerText; } else { filename = ""; }
                    if (i["e2filesize"] != null) { 
                        lFilesize = i["e2filesize"].InnerText;
                        lFilesize = (Convert.ToInt64(lFilesize) / 1000000).ToString() + " MB";
                    } else {
                        lFilesize = ""; 
                    }
                    if (i["e2servicereference"] != null) {
                        lServiceRef = i["e2servicereference"].InnerText;
                    } else {
                        lServiceRef = ""; 
                    }
                    ListViewItem listItem = ArchiveListView.Items.Add(new ListViewItem(new string[] { channelName, title, lDesc, zeit, duration, lFilesize, filename, lExtDesc, lServiceRef }));
                }
            }
            catch
            {
            }
        }

        private void streamToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ArchiveListView.SelectedItems.Count <= 0)
            {
                return;
            }
            string stream = "\"" + vStreamUrl + ArchiveListView.SelectedItems[0].SubItems[6].Text + "&device=etc\"";

            try
            {
                Process.Start(vMediaPlayer, stream);
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

        private void ArchiveListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ArchiveListView.SelectedItems.Count <= 0)
            {
                return;
            }
            //Zeit + Info ausgeben
            textBox.Text = ArchiveListView.SelectedItems[0].SubItems[3].Text + "\n" + 
                           ArchiveListView.SelectedItems[0].SubItems[7].Text; 

        }

        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ArchiveListView.SelectedItems.Count <= 0) return;
            
            string file = vDownloadUrl + ArchiveListView.SelectedItems[0].SubItems[6].Text;

            try
            {
                Process.Start(file);
            }
            // Most specific:
            catch (Win32Exception ex)
            {
                MessageBox.Show("Datei konnet nicht heruntergeladen werden: " + ex.Message);
            }
            // Least specific:
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim öffnen der Datei: " + ex.Message);
            }

        }

        private void löschenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ArchiveListView.SelectedItems.Count <= 0) return;
            Uri lUri = new Uri(vDeleteUrl + ArchiveListView.SelectedItems[0].SubItems[8].Text);
            string lresponseFromServer = SendWebRequest(lUri);
            //den Response noch bearbeiten: wenn Aufnahme gelöscht, dann auch aus der Liste steichen
            if (lresponseFromServer.IndexOf("deleted successfully") > 0)
            {
                MessageBox.Show("Aufnahme wurde gelöscht");
                //Eintrag löschen
                ArchiveListView.Items.Remove(ArchiveListView.SelectedItems[0]);
            }
            else
            {
                //Fehler
                MessageBox.Show("Aufnahme konnte nicht gelöscht werden");
            }

        }

        private void umbenennenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string newName = "";

            if (ArchiveListView.SelectedItems.Count <= 0) return;

            RenameTitle lFrm = new RenameTitle();
            lFrm.ArchivTitle = ArchiveListView.SelectedItems[0].SubItems[1].Text;

            if ((lFrm.ShowDialog() == DialogResult.OK) && (lFrm.ArchivTitle != ""))
            {
                newName = HttpUtility.UrlEncode(lFrm.ArchivTitle);
                Uri lUri = new Uri(vRenameUrl + HttpUtility.UrlEncode(ArchiveListView.SelectedItems[0].SubItems[8].Text) + "&newname=" + newName);
                string lresponseFromServer = SendWebRequest(lUri);
                //den Response noch bearbeiten: 
                if (lresponseFromServer.IndexOf("renamed successfully") > 0)
                {
                    MessageBox.Show("Aufnahme wurde umbenannt");
                    //Eintrage neu laden
                    GenerateArchivTable();
                }
                else
                {
                    //Fehler
                    MessageBox.Show("Aufnahme konnte nicht umbenannt werden");
                }
            }
        }

    }
}
