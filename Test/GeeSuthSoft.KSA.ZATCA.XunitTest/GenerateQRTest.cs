namespace GeeSuthSoft.KSA.ZATCA.XunitTest
{
    public class GenerateQRTest
    {

        string vName = "مؤسسة سالم ناصر احمد";
        string vTax = "300056289500003";
        decimal vVatAmount = 15;
        decimal vTotalAmount = 115;


        [Fact]
        public void GenerateBitmapImageTest()
        {
            var QrImage = Qr.GetImage(vName, vTax, new DateTime(2022, 1, 1), vVatAmount, vTotalAmount);

            Assert.NotNull(QrImage);

        }



        [Fact]
        public void GenerateBase64InUrlTest()
        {
            var QrImage = Qr.GetBase64InUrl(vName, vTax, new DateTime(2022, 1, 1), vVatAmount, vTotalAmount);

            Assert.NotNull(QrImage);
            Assert.StartsWith("data:image/png;base64,", QrImage);

        }
    }
}