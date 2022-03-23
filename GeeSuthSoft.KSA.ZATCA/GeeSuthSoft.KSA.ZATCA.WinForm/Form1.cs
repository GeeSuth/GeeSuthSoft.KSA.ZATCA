using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            var QrImage = ZATCA.Qr.QrCodeGenerate.GetImage("عبداللاه", "000000000000003", DateTime.Now, 15, 115);
            PicQr.Image = QrImage;
        }
    }
}
