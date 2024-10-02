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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeeSuthSoft.KSA.ZATCA.XunitTest
{
    public class ComplianceTest : IClassFixture<ServiceProviderFixture>
    {

        private readonly IZatcaOnboardingService _zatcaOnboardingService;
        private readonly IZatcaInvoiceService _zatcaInvoiceService;

        public ComplianceTest(ServiceProviderFixture fixture)
        {
            _zatcaOnboardingService = fixture.ServiceProvider.GetRequiredService<IZatcaOnboardingService>();
            _zatcaInvoiceService = fixture.ServiceProvider.GetRequiredService<IZatcaInvoiceService>();
        }

        [Fact]
        public async Task ComplianceCheckTest()
        {


            var onboardingResult = new OnboardingResultDto();

            var csrGenerationDto = ConstValue.CompanyTemplateTest.CrsCompanyInfo();

            // Act
            var resultGenerateCsr = _zatcaOnboardingService.GenerateCsr(csrGenerationDto, false);

            // Assert
            Assert.NotNull(resultGenerateCsr);
            Assert.NotNull(resultGenerateCsr.Csr);
            Assert.NotNull(resultGenerateCsr.PrivateKey);
            Assert.NotEmpty(resultGenerateCsr.Csr);
            Assert.NotEmpty(resultGenerateCsr.PrivateKey);

            onboardingResult.GeneratedCsr = resultGenerateCsr.Csr;
            onboardingResult.PrivateKey = resultGenerateCsr.PrivateKey;


            Assert.NotNull(onboardingResult.GeneratedCsr);
            Assert.NotNull(onboardingResult.PrivateKey);

            Assert.NotEmpty(onboardingResult.GeneratedCsr);
            Assert.NotEmpty(onboardingResult.PrivateKey);



            var ZatcaResult = await _zatcaOnboardingService.GetCSIDAsync(onboardingResult.GeneratedCsr, "12345");

            Assert.NotNull(ZatcaResult);


            Assert.NotNull(ZatcaResult.BinarySecurityToken);
            Assert.NotNull(ZatcaResult.RequestID);
            Assert.NotNull(ZatcaResult.Secret);



            var resultPCSID = await _zatcaOnboardingService.GetPCSIDAsync(
                CsidComplianceRequestId: ZatcaResult.RequestID, 
                CsidBinarySecurityToken: ZatcaResult.BinarySecurityToken, 
                CsidSecret: ZatcaResult.Secret);


            var invoiceObject = ConstValue.InvoicesTemplateTest.GetSimpleInvoice();

            SignInvoice signInvoice = new SignInvoice();

            var signed = signInvoice.GenerateSignedInvoice(invoiceObject,
                BinaryToken: resultPCSID.BinarySecurityToken,
                Secret: onboardingResult.PrivateKey);


            Assert.NotNull(signed);
            Assert.NotNull(signed.Base64SignedInvoice);
            Assert.NotNull(signed.RequestApi);




            var result = await _zatcaInvoiceService.ComplianceCheck(
                ccsidBinaryToken: ZatcaResult.BinarySecurityToken,
                ccsidSecret: ZatcaResult.Secret,
                requestApi:signed.RequestApi);


            Assert.NotNull(result);

            Assert.Equal("REPORTED", result.ReportingStatus);
            Assert.Equal("PASS", result.ValidationResults.Status);
        }
    }
}
