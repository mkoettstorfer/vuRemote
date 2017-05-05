using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EMMLogViewer
{
    public partial class Analyse : Form
    {
        public const int OSCAM = 0;
        public const int CCAM = 1;

        private int vCamType = OSCAM;
        private string vEmmAnalyse;

        /// <summary>
        /// Übergebenes EMM speichern und Analyse starten, dabei zwischen Oscam und CCam unterscheiden
        /// </summary>
        /// <returns></returns>
        public Analyse(string aEmmAnalyse)
        {
            InitializeComponent();
            vEmmAnalyse = aEmmAnalyse;

            listView1.Columns.Add("1", "Code", 100);
            listView1.Columns.Add("2", "Beschreibung", listView1.Size.Width - 100 - 4);

            //CAM Typ bestimmen
            if (vEmmAnalyse.Substring(0, 4) == "8200") { vCamType = CCAM; } else { vCamType = OSCAM; }
            DoAnalyze();
        }

        /// <summary>
        /// Grundaufbau analysieren und auageben
        /// </summary>
        /// <returns></returns>
        private void DoAnalyze()
        {
            if (vCamType == CCAM)
            {
                listView1.Items.Add(new ListViewItem(new string[] { vEmmAnalyse.Substring(0, 4), "82 00 CCAM" }));
            }
            else
            {
                listView1.Items.Add(new ListViewItem(new string[] { vEmmAnalyse.Substring(0, 4), "82 70 OSCAM" }));
            }
            listView1.Items.Add(new ListViewItem(new string[] { vEmmAnalyse.Substring(4, 2), "Anzahl der noch folgenden Bytes im EMM (Hex); Länge" }));
            switch (vEmmAnalyse.Substring(6, 2))
            {
                case "41": {
                    listView1.Items.Add(new ListViewItem(new string[] { "41", "EMM gerichtet an Smartcard (unique)" }));
                    if (vCamType == OSCAM)
                    {
                        listView1.Items.Add(new ListViewItem(new string[] { vEmmAnalyse.Substring(8, 8), "Seriennummer der Smartcard" }));
                    }
                    switch (vEmmAnalyse.Substring(16, 2)) {
                        case "02": Analyze02(vEmmAnalyse.Substring(18, vEmmAnalyse.Length - 18)); break;
                        case "07": Analyze07(vEmmAnalyse.Substring(18, vEmmAnalyse.Length - 18)); break;
                        default: listView1.Items.Add(new ListViewItem(new string[] { vEmmAnalyse.Substring(16, 2), "unbekannte EMM-Typisierung" })); break;
                    }
                    break;
                }
                case "01": {
                    listView1.Items.Add(new ListViewItem(new string[] { "01", "EMM gerichtet an mehrere Smartcards (shared/global)" }));  
                    break; 
                }
                case "C1": {
                    listView1.Items.Add(new ListViewItem(new string[] { "C1", "EMM gerichtet an Receiver/CAM" })); 
                    break; 
                }
                default:
                    {
                        listView1.Items.Add(new ListViewItem(new string[] { vEmmAnalyse.Substring(6, 2), "unbekannte EMM" }));
                        break; 
                    }
            }
        }

        /// <summary>
        /// EMM in Byte Blöcke zerlegen
        /// </summary>
        /// <returns></returns>
        private string GroupBytes(string EMM)
        {
            string result = "";
            for (int i = 0; i < EMM.Length; i++){
                result += EMM[i];
                if (i%2 != 0) result += " ";
            }
            return result;
        }

        /// <summary>
        /// 02er EMM analysieren
        /// </summary>
        /// <returns></returns>
        private void Analyze02(string aEmmAnalyseTyp)
        {
            listView1.Items.Add(new ListViewItem(new string[] { aEmmAnalyseTyp.Substring(0, 2), "00 fixer Wert" }));
            listView1.Items.Add(new ListViewItem(new string[] { aEmmAnalyseTyp.Substring(2, 2), "Anzahl der noch folgenden Bytes im EMM (Hex); Länge" }));
            listView1.Items.Add(new ListViewItem(new string[] { aEmmAnalyseTyp.Substring(4, 2), "90 fixer Wert" }));
            listView1.Items.Add(new ListViewItem(new string[] { aEmmAnalyseTyp.Substring(6, 2), "Anzahl der noch folgenden Bytes im EMM (Hex); Länge" }));
            switch (aEmmAnalyseTyp.Substring(8, 2))
            {
                case "44": listView1.Items.Add(new ListViewItem(new string[] { "44", "EMM-Typisierung; User-Verschlüsselung" })); break;
                case "60": listView1.Items.Add(new ListViewItem(new string[] { "60", "EMM-Typisierung; System-Verschlüsselung" })); break;
                case "40": listView1.Items.Add(new ListViewItem(new string[] { "40", "EMM-Typisierung; System-Verschlüsselung" })); break;
                default: listView1.Items.Add(new ListViewItem(new string[] { aEmmAnalyseTyp.Substring(8, 2), "unbekannte EMM-Typisierung" })); break;
            }
            listView1.Items.Add(new ListViewItem(new string[] { aEmmAnalyseTyp.Substring(10, 2), "Key-Index?;" }));
            listView1.Items.Add(new ListViewItem(new string[] { "verschl. Daten", GroupBytes(aEmmAnalyseTyp.Substring(12, aEmmAnalyseTyp.Length - 12)) })); 

        }

        /// <summary>
        /// 07er EMM analysieren
        /// </summary>
        /// <returns></returns>
        private void Analyze07(string aEmmAnalyseTyp)
        {
            listView1.Items.Add(new ListViewItem(new string[] { aEmmAnalyseTyp.Substring(0, 2), "Anzahl der noch folgenden Bytes im EMM (Hex); Länge" }));
            listView1.Items.Add(new ListViewItem(new string[] { aEmmAnalyseTyp.Substring(2, 4), "05 31 fixer Wert" }));
            listView1.Items.Add(new ListViewItem(new string[] { aEmmAnalyseTyp.Substring(6, 8), "Seriennummer der Smartcard" }));
            listView1.Items.Add(new ListViewItem(new string[] { aEmmAnalyseTyp.Substring(14, 2), "2B fixer Wert" }));
            listView1.Items.Add(new ListViewItem(new string[] { aEmmAnalyseTyp.Substring(16, 2), "Anzahl der noch folgenden Bytes im EMM (Hex); Länge" }));
            listView1.Items.Add(new ListViewItem(new string[] { aEmmAnalyseTyp.Substring(18, 2), "05 fixer Wert; Nachfolgend bis zu 5 Sub-EMMs" }));

            //Sub Emm'S
            AnalyzeSubEmm(aEmmAnalyseTyp.Substring(20, aEmmAnalyseTyp.Length - 20));
        }

        /// <summary>
        /// 07er Sub EMM's analysieren
        /// </summary>
        /// <returns></returns>
        private void AnalyzeSubEmm(string aSubEMM)
        {
            int len = 0;
            listView1.Items.Add(new ListViewItem(new string[] { "", "" }));
            listView1.Items.Add(new ListViewItem(new string[] { "##Sub EMM", "##" }));
            listView1.Items.Add(new ListViewItem(new string[] { aSubEMM.Substring(0, 2), "Sub-EMM ID (00 bis 04 möglich)" }));
            listView1.Items.Add(new ListViewItem(new string[] { aSubEMM.Substring(2, 8), "CA-Serial/Box-Serial wenn gesetzt oder 00 00 00 00" }));
            listView1.Items.Add(new ListViewItem(new string[] { aSubEMM.Substring(10, 2), "Anzahl der noch folgenden Bytes im Sub-EMM (Hex); Länge Payload" }));
            listView1.Items.Add(new ListViewItem(new string[] { aSubEMM.Substring(12, 2), "90 fixer Wert" }));
            listView1.Items.Add(new ListViewItem(new string[] { aSubEMM.Substring(14, 2), "Anzahl der noch folgenden Bytes im Sub-EMM (Hex); Länge Payload" }));
            len = int.Parse(aSubEMM.Substring(14, 2), System.Globalization.NumberStyles.HexNumber) * 2;
            switch (aSubEMM.Substring(16, 2))
            {
                case "44": listView1.Items.Add(new ListViewItem(new string[] { "44", "EMM-Typisierung; User-Verschlüsselung" })); break;
                case "60": listView1.Items.Add(new ListViewItem(new string[] { "60", "EMM-Typisierung; System-Verschlüsselung" })); break;
                case "40": listView1.Items.Add(new ListViewItem(new string[] { "40", "EMM-Typisierung; System-Verschlüsselung" })); break;
                default: listView1.Items.Add(new ListViewItem(new string[] { aSubEMM.Substring(16, 2), "unbekannte EMM-Typisierung" })); break;
            }
            listView1.Items.Add(new ListViewItem(new string[] { aSubEMM.Substring(18, 2), "Key-Index?;" }));
            len -= 4; //4 Zeichen weg
            listView1.Items.Add(new ListViewItem(new string[] { "verschl. Daten", aSubEMM.Substring(20, len) })); 

            //wenn Zeichen übrig sind, dann nächstes EMM
            aSubEMM = aSubEMM.Remove(0, len + 20);
            if ((aSubEMM != "") && aSubEMM.Length >= 60)
            { //minimale EMM Länge
                AnalyzeSubEmm(aSubEMM);
            }
            else
            {
                listView1.Items.Add(new ListViewItem(new string[] { "", "" }));
                listView1.Items.Add(new ListViewItem(new string[] { aSubEMM, "Prüfsumme und EMM-Ende" }));
            }
            listView1.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent); 
        }

        private void Analyse_ResizeEnd(object sender, EventArgs e)
        {
            listView1.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent); 
        }

    }
}
