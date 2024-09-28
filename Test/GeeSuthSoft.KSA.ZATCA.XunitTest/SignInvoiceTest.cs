using GeeSuthSoft.KSA.ZATCA.Dto;
using GeeSuthSoft.KSA.ZATCA.Enums;
using GeeSuthSoft.KSA.ZATCA.Generators;
using GeeSuthSoft.KSA.ZATCA.Helper;
using GeeSuthSoft.KSA.ZATCA.Services;
using GeeSuthSoft.KSA.ZATCA.Xml.RootPaths;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeeSuthSoft.KSA.ZATCA.XunitTest
{
    public class SignInvoiceTest
    {
        [Fact]
        public async Task GenerateSignedInvoiceTestAsync()
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

            var resultCSID = await onboardingService.GetCSIDAsync(onboardingResult.GeneratedCsr, "12345");

            Assert.NotNull(resultCSID);


            Assert.NotNull(resultCSID.BinarySecurityToken);
            Assert.NotNull(resultCSID.RequestID);
            Assert.NotNull(resultCSID.Secret);



            var resultPCSID = await onboardingService.GetPCSIDAsync(
                CsidComplianceRequestId: resultCSID.RequestID,
                CsidBinarySecurityToken: resultCSID.BinarySecurityToken,
                CsidSecret: resultCSID.Secret);


            var invoiceObject = ConstValue.InvoicesTemplateTest.GetSimpleInvoice();

            GeneratorInvoice generatorInvoice = new GeneratorInvoice(invoiceObject,
                Encoding.UTF8.GetString(Convert.FromBase64String(resultPCSID.BinarySecurityToken)),
                onboardingResult.PrivateKey);

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
