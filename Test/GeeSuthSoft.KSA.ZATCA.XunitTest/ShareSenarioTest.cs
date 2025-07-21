using System.Text;
using GeeSuthSoft.KSA.ZATCA.Generators;
using GeeSuthSoft.KSA.ZATCA.Services;
using GeeSuthSoft.KSA.ZATCA.XunitTest.ConstValue;
using GeeSuthSoft.KSA.ZATCA.XunitTest.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace GeeSuthSoft.KSA.ZATCA.XunitTest
{
    public class ShareSenarioTest : IClassFixture<ServiceProviderFixture>
    {


        private readonly IZatcaInvoiceService _zatcaInvoiceService;

        public ShareSenarioTest(ServiceProviderFixture fixture)
        {
            _zatcaInvoiceService = fixture.ServiceProvider.GetRequiredService<IZatcaInvoiceService>();
        }
        
        [Fact]
        public async Task ShareSignedInvoiceWithZatca()
        {
            // 1. Get Generate Invoice
            var invoiceObject = InvoicesTemplateTest.GetSimpleInvoice();
            if(invoiceObject == null ) Assert.Fail("Generate Invoice Is Failed!");


            // 2. Signed Invoice
            //SignInvoice signInvoice = new SignInvoice();

            //var signed = signInvoice.GenerateSignedInvoice(invoiceObject,
            //    BinaryToken: BinarySecurityToken,
            //    Secret: CrsPrivateKey);

            GeneratorInvoice generatorInvoice = new GeneratorInvoice(invoiceObject,
            Encoding.UTF8.GetString(Convert.FromBase64String(AuthTest.PCSIDBinaryToken)),
            AuthTest.CrsPrivateKey);

            var signeed = generatorInvoice.GetSignedInvoiceResult();

            if (signeed is null || signeed.RequestApi == null) Assert.Fail("Sign Invoice is Failed!");

            // 3. Check Compalice Invoice -- Seems not need in real senario.


            // 4. Share Invoice With Zatca

            var reportInvoiceZatca = await _zatcaInvoiceService.SendInvoiceToZatcaApi(signeed.RequestApi,
                PCSIDBinaryToken: AuthTest.PCSIDBinaryToken,
                PCSIDSecret: AuthTest.PCSIDSecret, false);


            Assert.NotNull(reportInvoiceZatca);

            Assert.True(reportInvoiceZatca.IsSuccessStatusCode);
        }
    }
}
