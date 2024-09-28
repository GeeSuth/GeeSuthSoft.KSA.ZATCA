using GeeSuthSoft.KSA.ZATCA.Dto;
using GeeSuthSoft.KSA.ZATCA.Enums;
using GeeSuthSoft.KSA.ZATCA.Generators;
using GeeSuthSoft.KSA.ZATCA.Helper;
using GeeSuthSoft.KSA.ZATCA.Services;
using GeeSuthSoft.KSA.ZATCA.Xml.RootPaths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeeSuthSoft.KSA.ZATCA.XunitTest
{
    public class ComplianceTest
    {
        [Fact]
        public async Task ComplianceCheckTest()
        {


            var onboardingResult = new OnboardingResultDto();


            var csrGenerationDto = ConstValue.CompanyTemplateTest.CrsCompanyInfo();

            var csrGenerator = new GeneratorCsr();
            var (generatedCsr, privateKey, errorMessages)
                = csrGenerator.GenerateCsrAndPrivateKey(csrGenerationDto, EnvironmentType.NonProduction, false);


            onboardingResult.GeneratedCsr = generatedCsr;
            onboardingResult.PrivateKey = privateKey;


            Assert.NotNull(onboardingResult.GeneratedCsr);
            Assert.NotNull(onboardingResult.PrivateKey);

            Assert.NotEmpty(onboardingResult.GeneratedCsr);
            Assert.NotEmpty(onboardingResult.PrivateKey);


            ZatcaOnboardingService onboardingService = new ZatcaOnboardingService();

            var ZatcaResult = await onboardingService.GetCSIDAsync(onboardingResult.GeneratedCsr, "12345");

            Assert.NotNull(ZatcaResult);


            Assert.NotNull(ZatcaResult.BinarySecurityToken);
            Assert.NotNull(ZatcaResult.RequestID);
            Assert.NotNull(ZatcaResult.Secret);



            var resultPCSID = await onboardingService.GetPCSIDAsync(
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


            ZatcaInvoiceService zatcaInvoiceService = new ZatcaInvoiceService();

            var result = await zatcaInvoiceService.ComplianceCheck(
                ccsidBinaryToken: ZatcaResult.BinarySecurityToken,
                ccsidSecret: ZatcaResult.Secret,
                requestApi:signed.RequestApi);


            Assert.NotNull(result);

            Assert.Equal("REPORTED", result.ReportingStatus);
            Assert.Equal("PASS", result.ValidationResults.Status);
        }
    }
}
