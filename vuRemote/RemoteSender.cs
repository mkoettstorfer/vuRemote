using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows.Forms;

namespace vuRemote
{
    class RemoteSender
    {
        private string vBaseURL;
        
        public RemoteSender()
        {
            IniFile lIni = new IniFile();
            var lip = lIni.Read("IP","Receiver");
            vBaseURL = "http://" + lip + ":80/api/remotecontrol?"; 
        }

        public void SendCode(string ACode, Boolean blong)
        {
            string sendUrl = vBaseURL;
            if (blong){ sendUrl += "type=long&";}
            Uri lUri = new Uri(sendUrl + "command=" + ACode);
            WebRequest vRequest;
            vRequest = WebRequest.Create(lUri);
            //vRequest.Credentials = CredentialCache.DefaultCredentials;
            ((HttpWebRequest)vRequest).UserAgent = "Eigenbau";
            vRequest.Method = "GET";
            //lrequest.ContentLength = byteArray.Length;
            vRequest.ContentType = "text/html";
            //Stream ldataStream = lrequest.GetRequestStream();
            // Set the 'Timeout' property in Milliseconds.
            vRequest.Timeout = 3000;
            try
            {
                // Get the response.
                WebResponse lResponse = vRequest.GetResponse();
                lResponse.Close();
                lResponse = null;
            }
            catch
            {
                //Timeout abfangen
                MessageBox.Show("Request timed out");
            }

        }

    }
}
