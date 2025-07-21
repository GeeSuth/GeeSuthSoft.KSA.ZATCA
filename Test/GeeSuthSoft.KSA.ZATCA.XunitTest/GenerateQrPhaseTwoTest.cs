using System.Drawing;
using System.Text;
using GeeSuthSoft.KSA.ZATCA.Dto;
using GeeSuthSoft.KSA.ZATCA.Extensions;
using GeeSuthSoft.KSA.ZATCA.Generators;
using GeeSuthSoft.KSA.ZATCA.Services;
using GeeSuthSoft.KSA.ZATCA.XunitTest.ConstValue;
using GeeSuthSoft.KSA.ZATCA.XunitTest.HelperTest;
using GeeSuthSoft.KSA.ZATCA.XunitTest.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace GeeSuthSoft.KSA.ZATCA.XunitTest;



  public class GenerateQrPhaseTwoTest(ServiceProviderFixture fixture) : IClassFixture<ServiceProviderFixture>
    {
        private readonly IZatcaOnboardingService _zatcaOnboardingService = fixture.ServiceProvider.GetRequiredService<IZatcaOnboardingService>();

        [Fact]
        public async Task GenerateQrFromSignedInvoice_CanOpenInQrBrowser_TestAsync()
        {


            var csrGenerationDto = CompanyTemplateTest.CrsCompanyInfo();

            // Act
            var resultCrs = _zatcaOnboardingService.GenerateCsr(csrGenerationDto);

            // Assert
            Assert.NotNull(resultCrs);
            Assert.NotNull(resultCrs.Csr);
            Assert.NotNull(resultCrs.PrivateKey);
            Assert.NotEmpty(resultCrs.Csr);
            Assert.NotEmpty(resultCrs.PrivateKey);



            var resultCSID = await _zatcaOnboardingService.GetCSIDAsync(resultCrs.Csr);

            Assert.NotNull(resultCSID);


            Assert.NotNull(resultCSID.BinarySecurityToken);
            Assert.NotNull(resultCSID.RequestID);
            Assert.NotNull(resultCSID.Secret);




            
            var pcsidRequest = new PCSIDRequestDto()
            {
                CsidComplianceRequestId = resultCSID.RequestID,
                CsidBinarySecurityToken = resultCSID.BinarySecurityToken,
                CsidSecret = resultCSID.Secret
            };

            var resultPCSID = await _zatcaOnboardingService.GetPCSIDAsync(pcsidRequest);


            var invoiceObject = InvoicesTemplateTest.GetSimpleInvoice();

            GeneratorInvoice generatorInvoice = new GeneratorInvoice(invoiceObject,
                Encoding.UTF8.GetString(Convert.FromBase64String(resultPCSID.BinarySecurityToken)),
                resultCrs.PrivateKey);

            var signeed = generatorInvoice.GetSignedInvoiceResult();
            

            Assert.NotNull(signeed);
            Assert.NotNull(signeed.Base64SignedInvoice);
            Assert.NotNull(signeed.RequestApi);


            var QrURLcanOpenInUObrowser = signeed.Base64QrCodeContent.GenerateImageDirectOpenInBrowser();

            Assert.NotNull(QrURLcanOpenInUObrowser);
            
            
            // Decode the Invoice
            var decodeObject = signeed.Base64QrCodeContent.DecodeToObject();
            Assert.NotNull(decodeObject);
            Assert.NotEmpty(decodeObject.QrCode.ArabicCompanyName);
            Assert.NotEmpty(decodeObject.QrCode.EnglishCompanyName);
            Assert.NotEmpty(decodeObject.QrCode.TaxNumber);


        }
        
    }