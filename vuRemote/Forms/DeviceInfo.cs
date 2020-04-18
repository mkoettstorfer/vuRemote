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
    public partial class DeviceInfo : Form
    {
        private string vBaseURL;

        public DeviceInfo()
        {
            InitializeComponent();
            IniFile lIni = new IniFile();
            var lip = lIni.Read("IP", "Receiver");
            vBaseURL = "http://" + lip + "/web/about";
            ReadDeviceInfo();
        }

        private void ReadDeviceInfo()
        {
            Uri lUri = new Uri(vBaseURL);
            string lResponse = SendWebRequest(lUri);

            //wenn keine Daten gefunden, dann exit
            if (lResponse == "") { return; }
            //Bug im XML, hier muss ein Tag korrigiert werden
            lResponse = lResponse.Replace("deviceInfoInfoTable", "deviceInfoTable");
            //XML Elemente durchgehen
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(lResponse);
            XmlElement root = doc.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("./e2about");
            try
            {
                foreach (XmlNode i in nodes)
                {
                    if (i["e2enigmaversion"] != null)
                    {
                        listView1.Items.Add(new ListViewItem(new string[] { "Enigma Version", i["e2enigmaversion"].InnerText }));
                    }
                    if (i["e2imageversion"] != null)
                    {
                        listView1.Items.Add(new ListViewItem(new string[] { "Image", i["e2imageversion"].InnerText }));
                    }
                    if (i["e2webifversion"] != null)
                    {
                        listView1.Items.Add(new ListViewItem(new string[] { "WebIf", i["e2webifversion"].InnerText }));
                    }
                    if (i["e2model"] != null)
                    {
                        listView1.Items.Add(new ListViewItem(new string[] { "Modell", i["e2model"].InnerText }));
                    }
                    if (i["e2lanip"] != null)
                    {
                        listView1.Items.Add(new ListViewItem(new string[] { "IP", i["e2lanip"].InnerText }));
                    }
                    if (i["e2lanmac"] != null)
                    {
                        listView1.Items.Add(new ListViewItem(new string[] { "MAC", i["e2lanmac"].InnerText }));
                    }
                    if (i["e2landhcp"] != null)
                    {
                        listView1.Items.Add(new ListViewItem(new string[] { "DHCP", i["e2landhcp"].InnerText }));
                    }
                    if (i["e2langw"] != null)
                    {
                        listView1.Items.Add(new ListViewItem(new string[] { "Gateway", i["e2langw"].InnerText }));
                    }
                }
                XmlNodeList tunernodes = root.SelectNodes("./e2about/e2tunerinfo/e2nim");
                foreach (XmlNode i in tunernodes)
                {
                    string lTuner = "";
                    if (i["name"] != null)
                    {
                        lTuner = i["name"].InnerText + ": ";
                    }
                    if (i["type"] != null)
                    {
                        listView1.Items.Add(new ListViewItem(new string[] { "Tuner", lTuner + i["type"].InnerText }));
                    }
                }
                XmlNodeList hddnodes = root.SelectNodes("./e2about/e2hddinfo");
                foreach (XmlNode i in hddnodes)
                {
                    if (i["model"] != null)
                    {
                        listView1.Items.Add(new ListViewItem(new string[] { "Festplatte", i["model"].InnerText }));
                    }
                    if (i["capacity"] != null)
                    {
                        listView1.Items.Add(new ListViewItem(new string[] { "Kapazität", i["capacity"].InnerText }));
                    }
                    if (i["free"] != null)
                    {
                        listView1.Items.Add(new ListViewItem(new string[] { "Freier Speicher", i["free"].InnerText }));
                    }
                }

            }
            catch
            {
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

        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
