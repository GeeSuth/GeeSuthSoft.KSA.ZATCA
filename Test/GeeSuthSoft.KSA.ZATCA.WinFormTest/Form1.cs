namespace GeeSuthSoft.KSA.ZATCA.WinFormTest
{
    public partial class Form1 : Form
    {

        string vName = "مؤسسة سالم ناصر احمد";
        string vTax = "300056289500003";
        decimal vVatAmount = 15;
        decimal vTotalAmount = 115;

        public Form1()
        {
            InitializeComponent();
        }

        private void btn_loadQR_Click(object sender, EventArgs e)
        {
            var QrImage = Qr.GetImage(vName, vTax, new DateTime(2022, 1, 1), vVatAmount, vTotalAmount);
            pic.Image = QrImage;

        }
    }
}
