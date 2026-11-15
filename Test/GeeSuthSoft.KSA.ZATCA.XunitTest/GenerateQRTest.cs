using System.Runtime.InteropServices;

namespace GeeSuthSoft.KSA.ZATCA.XunitTest
{
    // Qr is obsolete, but its public API is still supported.
#pragma warning disable CS0618
#pragma warning disable CA1416 // GetImage is guarded by the Windows runtime check below.
    public class GenerateQRTest
    {

        string vName = "مؤسسة سالم ناصر احمد";
        string vTax = "300056289500003";
        decimal vVatAmount = 15;
        decimal vTotalAmount = 115;


        [Fact]
        public void GenerateBitmapImageTest()
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // GetImage returns System.Drawing.Bitmap, which requires Windows/GDI+.
                Assert.Throws<PlatformNotSupportedException>(() =>
                    Qr.GetImage(vName, vTax, new DateTime(2022, 1, 1), vVatAmount, vTotalAmount));
                return;
            }

            using (var qrImage = Qr.GetImage(vName, vTax, new DateTime(2022, 1, 1), vVatAmount, vTotalAmount))
            {
                Assert.NotNull(qrImage);
                Assert.True(qrImage.Width > 0);
                Assert.True(qrImage.Height > 0);
            }
        }



        [Fact]
        public void GenerateBase64InUrlTest()
        {
            var QrImage = Qr.GetBase64InUrl(vName, vTax, new DateTime(2022, 1, 1), vVatAmount, vTotalAmount);

            Assert.NotNull(QrImage);
            Assert.StartsWith("data:image/png;base64,", QrImage);

        }
    }
#pragma warning restore CS0618
#pragma warning restore CA1416
}
