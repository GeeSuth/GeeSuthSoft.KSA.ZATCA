using GeeSuthSoft.KSA.ZATCA.Dto;
using GeeSuthSoft.KSA.ZATCA.Enums;
using GeeSuthSoft.KSA.ZATCA.Generators;
using GeeSuthSoft.KSA.ZATCA.Helper;
using GeeSuthSoft.KSA.ZATCA.Services;
using GeeSuthSoft.KSA.ZATCA.Xml.RootPaths;
using GeeSuthSoft.KSA.ZATCA.XunitTest.Shared;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeeSuthSoft.KSA.ZATCA.XunitTest
{
    public class SignInvoiceTest : IClassFixture<ServiceProviderFixture>
    {
        private readonly IZatcaOnboardingService _zatcaOnboardingService;

        public SignInvoiceTest(ServiceProviderFixture fixture)
        {
            _zatcaOnboardingService = fixture.ServiceProvider.GetRequiredService<IZatcaOnboardingService>();
        }

        [Fact]
        public async Task GenerateSignedInvoiceTestAsync()
        {


            var csrGenerationDto = ConstValue.CompanyTemplateTest.CrsCompanyInfo();

            // Act
            var resultCrs = _zatcaOnboardingService.GenerateCsr(csrGenerationDto, false);

            // Assert
            Assert.NotNull(resultCrs);
            Assert.NotNull(resultCrs.Csr);
            Assert.NotNull(resultCrs.PrivateKey);
            Assert.NotEmpty(resultCrs.Csr);
            Assert.NotEmpty(resultCrs.PrivateKey);



            var resultCSID = await _zatcaOnboardingService.GetCSIDAsync(resultCrs.Csr, "12345");

            Assert.NotNull(resultCSID);


            Assert.NotNull(resultCSID.BinarySecurityToken);
            Assert.NotNull(resultCSID.RequestID);
            Assert.NotNull(resultCSID.Secret);



            var resultPCSID = await _zatcaOnboardingService.GetPCSIDAsync(
                CsidComplianceRequestId: resultCSID.RequestID,
                CsidBinarySecurityToken: resultCSID.BinarySecurityToken,
                CsidSecret: resultCSID.Secret);


            var invoiceObject = ConstValue.InvoicesTemplateTest.GetSimpleInvoice();

            GeneratorInvoice generatorInvoice = new GeneratorInvoice(invoiceObject,
                Encoding.UTF8.GetString(Convert.FromBase64String(resultPCSID.BinarySecurityToken)),
                resultCrs.PrivateKey);

            var signeed = generatorInvoice.GetSignedInvoiceResult();

            //SignInvoice signInvoice = new SignInvoice();

            //var signed = signInvoice.GenerateSignedInvoice(invoiceObject,
            //    BinaryToken: resultPCSID.BinarySecurityToken,
            //    Secret: resultPCSID.Secret);


            Assert.NotNull(signeed);
            Assert.NotNull(signeed.Base64SignedInvoice);
            Assert.NotNull(signeed.RequestApi);

        }
    }
}
