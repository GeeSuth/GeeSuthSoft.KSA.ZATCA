using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeeSuthSoft.KSA.ZATCA.WinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_show_Click(object sender, EventArgs e)
        {
            var QrImage = ZATCA.Qr.Qr.GetImage("بسم الله الرحمن الرحيم", "0000000000009", new DateTime(2022, 1, 1), 15, 115);
            pic_new.Image = QrImage;


            txt_base64.Text= ZATCA.Qr.Qr.GetBase64("احمد", "0000000000009", new DateTime(2022, 1, 1), 15, 115);

            linkOpenInBroswer.Click += LinkOpenInBroswer_Click;
            
        }

        private void LinkOpenInBroswer_Click(object sender, EventArgs e)
        {
            //This will not working if your pc has firewall untill you grant permisson
            Process.Start("explorer", ZATCA.Qr.Qr.GetBase64InUrl("احمد", "0000000000009", new DateTime(2022, 1, 1), 15, 115));
        }
    }
}
