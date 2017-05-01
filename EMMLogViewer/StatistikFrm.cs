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
    public partial class StatistikFrm : Form
    {
        private string vSearchString;
        private string[] vLines;
        private Int32 vCount = 0;
        private string vFirst = "";
        private string vLast;

        public StatistikFrm(string aSearchString, string[] aLines)
        {
            InitializeComponent();
            vSearchString = aSearchString;
            vLines = aLines;
            CalcStat();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Statistik berechnen
        private void CalcStat(){
            DateTime lastDate = new DateTime(0);
            List<Int64> DateList = new List<Int64>();

            foreach (string line in vLines)
            {
                string[] data = line.Split(new Char[] { ' ','\t' });
                if ((data.Count() >= 7) && (vSearchString.Contains(data[7])))
                {
                    vCount++;
                    //0 = datum 1 = zeit
                    if (vFirst == "") { vFirst = data[0] + " " + data[1]; }
                    vLast = data[0] + " " + data[1];

                    DateTime newDate = new DateTime(Convert.ToInt32(data[0].Substring(0, 4)), Convert.ToInt32(data[0].Substring(5, 2)), Convert.ToInt32(data[0].Substring(8, 2)),
                                                    Convert.ToInt32(data[1].Substring(0, 2)), Convert.ToInt32(data[1].Substring(3, 2)), Convert.ToInt32(data[1].Substring(6, 2)));
                    if (vCount > 1)
                    {
                        Int64 lSeconds = newDate.Subtract(lastDate).Ticks;
                        DateList.Add(lSeconds);
                    }
                    //Datum sichern
                    lastDate = newDate;
                }
            }
            DateList.Sort(); // für Statistik sortieren
            labelCount.Text = "Anzahl: " + vCount.ToString();
            Int64 xDiff = ((DateTime.Now).Subtract(new DateTime(Convert.ToInt32(vFirst.Substring(0, 4)), Convert.ToInt32(vFirst.Substring(5, 2)), Convert.ToInt32(vFirst.Substring(8, 2)),
                                                                Convert.ToInt32(vFirst.Substring(11, 2)), Convert.ToInt32(vFirst.Substring(14, 2)), Convert.ToInt32(vFirst.Substring(17, 2))))).Ticks;
            labelStart.Text = "Von Datum: " + vFirst +" (vor " + Math.Round(TimeSpan.FromTicks(xDiff).TotalHours, 2).ToString() + " h)";
            labelEnde.Text = "Bis Datum: " + vLast;
            //Median ermitteln
            if (DateList.Count > 0)
            {
                Int64 lSeconds = 0;
                if ((DateList.Count % 2) == 0)
                {
                    lSeconds = DateList[DateList.Count / 2] + DateList[(DateList.Count / 2) + 1];
                }
                else
                {
                    lSeconds = DateList[DateList.Count / 2];
                }
                TimeSpan ts = TimeSpan.FromTicks(lSeconds);
                labelMedian.Text = "Signalabstand (Median): " + Math.Round(ts.TotalMinutes, 2).ToString() + " min";
            }
            //Mittelwert ermitteln
            if (DateList.Count > 0)
            {
                Int64 lSeconds = 0;
                for (int i = 0; i< DateList.Count; i++){ lSeconds += DateList[i];}
                lSeconds = lSeconds / DateList.Count;
                TimeSpan ts = TimeSpan.FromTicks(lSeconds);
                labelMittel.Text = "Signalabstand (Mittel): " + Math.Round(ts.TotalMinutes, 2).ToString() + " min";
            }
            //kürzeste Dauer
            if (DateList.Count > 0)
            {
                Int64 lSeconds = DateList[0];
                TimeSpan ts = TimeSpan.FromTicks(lSeconds);
                labelShort.Text = "Signalabstand (kurz): " + Math.Round(ts.TotalMinutes, 2).ToString() + " min";
            }
            //längste Dauer
            if (DateList.Count > 0)
            {
                Int64 lSeconds = DateList[DateList.Count-1];
                TimeSpan ts = TimeSpan.FromTicks(lSeconds);
                labelLong.Text = "Signalabstand (lang): " + Math.Round(ts.TotalMinutes, 2).ToString() + " min";
            }

        }
    }
}
