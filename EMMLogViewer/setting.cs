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
    public partial class setting : Form
    {
        public setting() 
        {
            InitializeComponent();
            IniFile lIni = new IniFile();
            ipAddressControl.Text = lIni.Read("IP", "Receiver");
            LogFileTextBox.Text = lIni.Read("LOG", "Receiver");
            CardTextBox.Text = lIni.Read("CARD", "Receiver");
            SerialTextBox.Text = lIni.Read("BOX", "Receiver");
            UserTextBox.Text = lIni.Read("USER", "Receiver");
            PasswortTextBox.Text = lIni.Read("PWD", "Receiver");

            maskedTextBox.Mask = "99/99/9999";
            maskedTextBox.ValidatingType = typeof(System.DateTime);
            if (lIni.Read("VALID", "Receiver") != "")
            {
                maskedTextBox.Text = lIni.Read("VALID", "Receiver");
            }
        }

        // speichern wenn OK gedrückt
        private void OkButton_Click(object sender, EventArgs e)
        {
            IniFile lIni = new IniFile();
            lIni.Write("IP", ipAddressControl.Text, "Receiver");
            lIni.Write("LOG", LogFileTextBox.Text, "Receiver");
            lIni.Write("CARD", CardTextBox.Text, "Receiver");
            lIni.Write("BOX", SerialTextBox.Text, "Receiver");
            lIni.Write("USER", UserTextBox.Text, "Receiver");
            lIni.Write("PWD", PasswortTextBox.Text, "Receiver");
            if (maskedTextBox.Text != ""){
                lIni.Write("VALID", maskedTextBox.Text, "Receiver");
            }
        }

        private void maskedTextBox_TypeValidationCompleted(object sender, TypeValidationEventArgs e)
        {
            //if (!e.IsValidInput)
            //{
            //    toolTip1.ToolTipTitle = "Ungültiges Datum";
            //    toolTip1.Show("Das Datum muss im Format dd/mm/yyyy eingegeben werden.", maskedTextBox, 0, -20, 5000);
            //}
    
        }

    }
}
