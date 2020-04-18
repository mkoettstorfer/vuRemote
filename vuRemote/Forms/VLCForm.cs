using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace vuRemote
{
    public partial class VLCForm : Form
    {
        private string vStream;

        public VLCForm(string AStream)
        {
            InitializeComponent();
            vStream = AStream;
        }

        private void VLCForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
