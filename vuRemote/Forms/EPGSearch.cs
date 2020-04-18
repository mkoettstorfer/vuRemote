using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Xml;

namespace vuRemote
{
    public partial class EPGSearch : Form
    {
        private readonly string vURL;
        private readonly string vAddTimerURL;
        private readonly string vDeleteTimerURL;
        private readonly Boolean vDayLightSaving;

        public EPGSearch(string aSearchString)
        {
            InitializeComponent();
            //Variablen anlegen
            IniFile lIni = new IniFile();
            var lBaseUrl = "http://" + lIni.Read("IP", "Receiver");
            vURL = lBaseUrl + "/web/epgsearch?search=" + aSearchString;
            vAddTimerURL = lBaseUrl + "/web/timeraddbyeventid?sRef={serviceRef}&eventid={eventID}&justplay={justplay}";
            //vDeleteTimerURL = lBaseUrl + "/DeleteTimer/";
            //Name setzen
            this.Text = "EPG - " + aSearchString;

            vDayLightSaving = TimeZoneInfo.Local.IsDaylightSavingTime(DateTime.Now);
            //Columns anlegen
            listView1.Columns.Add("1", "Icon", 40);
            listView1.Columns.Add("2", "Kanal", 100);
            listView1.Columns.Add("3", "Programm", 260);
            listView1.Columns.Add("4", "Zeit", 120);
            //listView1.Columns.Add("5", "tvtvId", 0);
            //listView1.Columns.Add("5", "progId", 60);

            //Icons einlesen
            ReadImages();

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


        private string PrintDate(DateTime aDate)
        {
            return aDate.Day.ToString() + "." + aDate.Month.ToString() + ".";
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
                string zeit = "";
                string startTime = "";
                string progId = "";
                string text = "";
                string tvtId = "";
                string programm = "";
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
                        zeit = String.Format("{0} {1}  {2:00}:{3:00}", DateTools.PrintDayOfWeek(UnixEpochStart.DayOfWeek), PrintDate(UnixEpochStart), UnixEpochStart.Hour, UnixEpochStart.Minute);
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
                    if (i["e2eventservicename"] != null) { programm = i["e2eventservicename"].InnerText; } else { programm = ""; }
                    if (i["e2eventservicereference"] != null) { tvtId = i["e2eventservicereference"].InnerText; } else { tvtId = ""; }
                    ListViewItem listItem = listView1.Items.Add(new ListViewItem(new string[] { "", programm, title, zeit, progId, startTime, text, tvtId }));
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

        private void OnAddTimerClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0)
            {
                return;
            }
            //Timer hinzufügen
            string vTimerURL = vAddTimerURL;
            //vTimerURL = vTimerURL.Replace("{serviceRef}", vserviceRef); //Todo. muss gesetzt werden
            vTimerURL = vTimerURL.Replace("{eventID}", listView1.SelectedItems[0].SubItems[3].Text);
            vTimerURL = vTimerURL.Replace("{justplay}","0");
            Uri lUri = new Uri(vTimerURL);
            string lresponseFromServer = SendWebRequest(lUri); 
            //den Response noch bearbeiten: wenn Timer gesetzt kommt, dann auch den Eintrag auf Rec umstellen
            if (lresponseFromServer.IndexOf(" added") > 0)
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

        private void listView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            //Hintergrund zeichen
            Color lFarbe = Color.Silver;
            Rectangle rect = new Rectangle(e.Bounds.Left + 1, e.Bounds.Top + 1, e.Bounds.Width - 2, e.Bounds.Height - 2);
            SolidBrush lbr = new SolidBrush(lFarbe);
            e.Graphics.FillRectangle(lbr, rect);
            //Text ausgeben
            StringFormat string_format = new StringFormat();
            string_format.Alignment = StringAlignment.Center;
            string_format.LineAlignment = StringAlignment.Center;
            e.Graphics.DrawString(listView1.Columns[e.ColumnIndex].Text, listView1.Font, Brushes.Black, e.Bounds);
        }

        private void DrawBackGround(DrawListViewSubItemEventArgs e)
        {
            //Rectangle rect = new Rectangle(e.Bounds.Left + 1, e.Bounds.Top + 1, e.Bounds.Width - 2, e.Bounds.Height - 2);
            Rectangle rect = new Rectangle(e.Bounds.Left, e.Bounds.Top, e.Bounds.Width, e.Bounds.Height);
            SolidBrush lbr = new SolidBrush(Color.Blue);
            e.Graphics.FillRectangle(lbr, rect);
        }

        private void DrawCell(string Text, Boolean selected, DrawListViewSubItemEventArgs e)
        {
            //Ausgabeformat
            StringFormat string_format = new StringFormat();
            string_format.Trimming = StringTrimming.EllipsisWord;
            string_format.FormatFlags |= StringFormatFlags.NoWrap;
            string_format.LineAlignment = StringAlignment.Center;
            if (selected)
            {
                DrawBackGround(e);
                e.Graphics.DrawString(Text, e.Item.ListView.Font, Brushes.White, e.Bounds, string_format);
            }
            else
            {
                e.Graphics.DrawString(Text, e.Item.ListView.Font, Brushes.Black, e.Bounds, string_format);
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
                    // Draw the Channel Name
                    DrawCell(item.SubItems[1].Text, selected, e);
                    break;
                case 2:
                    // Draw the Programm Name 
                    DrawCell(item.SubItems[2].Text, selected, e);
                    break;
                case 3:
                    // Draw Start/End Time
                    DrawCell(item.SubItems[3].Text, selected, e);
                    break;
            }
            // Draw the focus rectangle if appropriate.   
            e.Graphics.ResetTransform();
            if (lvw.FullRowSelect)
            {
                e.DrawFocusRectangle(e.Item.Bounds);
            }
        }

        // Kanal Logos einlesen
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

    }
}
