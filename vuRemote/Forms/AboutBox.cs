using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace vuRemote
{
    public partial class AboutBox : Form
    {
        private string TopCaption = "About " + Application.ProductName;

        public AboutBox()
        {
            InitializeComponent();
            this.productVersionLabel.Text = this.productVersionLabel.Text + Properties.Resources.Version;
        }

        public AboutBox(string TopCaption, string Link)
        {
            InitializeComponent();
            this.productNameLabel.Text = Application.ProductName.Length <= 0 ? "{Product Name}" : Application.ProductName;

            this.TopCaption = TopCaption;
            this.linkLabel.Text = Link;
            this.productNameLabel.Text = this.productNameLabel.Text + Application.ProductVersion;
        }

        private void topPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawIcon(Icon.ExtractAssociatedIcon(Application.ExecutablePath), 20, 8);
            e.Graphics.DrawString(TopCaption, new Font("Segoe UI", 14f), Brushes.Azure, new PointF(70, 10));
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(this.linkLabel.Text);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:kRemote@gibtsdo.net?subject=kRemote " + Properties.Resources.Version);
        }

    }
}
