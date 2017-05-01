﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace vuRemote
{
    public partial class setting : Form
    {
        public setting() 
        {
            InitializeComponent();
            IniFile lIni = new IniFile();
            ipAddressControl.Text = lIni.Read("IP", "Receiver");
            sClientName.Text = lIni.Read("EXE", "Mediaplayer");
            try
            {
                intervall.Value = Convert.ToInt16(lIni.Read("INTERVALL", "Receiver"));
            }
            catch
            {
                intervall.Value = 0;
            }
        
        }

        // speichern wenn OK gedrückt
        private void OkButton_Click(object sender, EventArgs e)
        {
            IniFile lIni = new IniFile();
            lIni.Write("IP", ipAddressControl.Text, "Receiver");
            lIni.Write("EXE", sClientName.Text, "Mediaplayer");
            lIni.Write("INTERVALL", intervall.Value.ToString(), "Receiver");
        }

        //Dialog zum Suchen des Medienplayers öffnen
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Executables (*.exe)|*.exe|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                sClientName.Text = openFileDialog.FileName;
            }
        }

    }
}
