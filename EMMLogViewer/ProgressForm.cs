using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace EMMLogViewer
{
    public partial class ProgressForm : Form
    {
        private BackgroundWorker worker;

        public ProgressForm()
        {
            InitializeComponent();
        }

        public void SetProgress(string aProgressText, int aPercent)
        {
            ProgressText.Text = aProgressText;
            progressBar1.Value = aPercent;
            Application.DoEvents();
        }

        private void ProgressForm_Load(object sender, EventArgs e)
        {
            this.CenterToParent();
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
 
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);

            progressBar1.Value = progressBar1.Minimum;
            worker.RunWorkerAsync(progressBar1.Value);
            Application.DoEvents(); // damit die Form refresht wird
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            int percentFinished = (int)e.Argument;
            while (!worker.CancellationPending && percentFinished < 100)
            {
                percentFinished++;
                worker.ReportProgress(percentFinished);
                Application.DoEvents(); // damit die Form refresht wird
                System.Threading.Thread.Sleep(50);
            }
            e.Result = percentFinished;
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }
 
        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //MessageBox.Show("Asynchroner Thread kam bis zum Wert:"+e.Result.ToString());
            //btnStartEnd.Text = "Starten";
        }
    }
}
