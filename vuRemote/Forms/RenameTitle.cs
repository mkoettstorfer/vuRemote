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
    public partial class RenameTitle : Form
    {
        public RenameTitle()
        {
            InitializeComponent();
        }

        public string ArchivTitle
        {
            get { return Title.Text; }
            set { Title.Text = value; }
}
    }
}
