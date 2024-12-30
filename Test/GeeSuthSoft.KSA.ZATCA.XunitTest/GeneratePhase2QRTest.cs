
using System.Text;
using GeeSuthSoft.KSA.ZATCA.Generators;


namespace GeeSuthSoft.KSA.ZATCA.XunitTest;

public class GeneratePhase2QRTest 
{
    [Fact]
    public void TestGeneratePhase2QR()
    {
                    var invoiceObject = ConstValue.InvoicesTemplateTest.GetSimpleInvoice();

            GeneratorInvoice generatorInvoice = new GeneratorInvoice(invoiceObject,
                Encoding.UTF8.GetString(Convert.FromBase64String(ConstValue.AuthTest.PCSIDBinaryToken)),
                ConstValue.AuthTest.CrsPrivateKey);

            var signeed = generatorInvoice.GetSignedInvoiceResult();

            Assert.NotNull(signeed);
            Assert.NotNull(signeed.Base64SignedInvoice);
            Assert.NotNull(signeed.Base64QrCode);
    }
}
