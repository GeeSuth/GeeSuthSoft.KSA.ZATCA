using GeeSuthSoft.KSA.ZATCA.Enums;
using GeeSuthSoft.KSA.ZATCA.External;
using GeeSuthSoft.KSA.ZATCA.Helper;
using GeeSuthSoft.KSA.ZATCA.Models;
using GeeSuthSoft.KSA.ZATCA.Services;
using GeeSuthSoft.KSA.ZATCA.XunitTest.ConstValue;
using GeeSuthSoft.KSA.ZATCA.XunitTest.Shared;
using Microsoft.Extensions.DependencyInjection;

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
        internal async Task ComplianceCheckTest()
        {


            var onboardingResult = new OnboardingResult();

            var csrGenerationDto = CompanyTemplateTest.CrsCompanyInfo();

            // Act
            var resultGenerateCsr = _zatcaOnboardingService.GenerateCsr(csrGenerationDto);

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



            var ZatcaResult = await _zatcaOnboardingService.GetCSIDAsync(onboardingResult.GeneratedCsr);

            Assert.NotNull(ZatcaResult);


            Assert.NotNull(ZatcaResult.BinarySecurityToken);
            Assert.NotNull(ZatcaResult.RequestID);
            Assert.NotNull(ZatcaResult.Secret);



            var resultPCSID = await _zatcaOnboardingService.GetPCSIDAsync(
                CsidComplianceRequestId: ZatcaResult.RequestID, 
                CsidBinarySecurityToken: ZatcaResult.BinarySecurityToken, 
                CsidSecret: ZatcaResult.Secret);


            var invoiceObject = InvoicesTemplateTest.GetSimpleInvoice();

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
                signed.RequestApi.ZatcaRequestApiDto());


            Assert.NotNull(result);

            Assert.Equal(ZatcaReportingStatus.REPORTED, result.ReportingStatus);
            //Assert.Equal(ZatcaValidationResults.PASS, result.ValidationResults.Status);
        }
    }
}
