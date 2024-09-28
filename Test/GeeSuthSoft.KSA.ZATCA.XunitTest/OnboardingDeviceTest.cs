using GeeSuthSoft.KSA.ZATCA.Dto;
using GeeSuthSoft.KSA.ZATCA.Enums;
using GeeSuthSoft.KSA.ZATCA.Generators;
using GeeSuthSoft.KSA.ZATCA.Helper;
using GeeSuthSoft.KSA.ZATCA.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GeeSuthSoft.KSA.ZATCA.XunitTest
{
    
    public class OnboardingDeviceTest
    {

        [Fact]
        public void Generate_Crs_Test()
        {

            var onboardingResult = new OnboardingResultDto();


            var csrGenerationDto = new CsrGenerationDto
            {
                CommonName = "TST-886431145-399999999900003",
                SerialNumber = "1-TST|2-TST|3-ed22f1d8-e6a2-1118-9b58-d9a8f11e445f",
                OrganizationIdentifier = "399999999900003",
                OrganizationUnitName = "Riyadh Branch",
                OrganizationName = "Maximum Speed Tech Supply LTD",
                CountryName = "SA",
                InvoiceType = "1100",
                LocationAddress = "RRRD2929",
                IndustryBusinessCategory = "Supply activities"
            };

            var csrGenerator = new GeneratorCsr();
            var (generatedCsr, privateKey, errorMessages)
                = csrGenerator.GenerateCsrAndPrivateKey(csrGenerationDto, EnvironmentType.NonProduction, false);


            onboardingResult.GeneratedCsr = generatedCsr;
            onboardingResult.PrivateKey = privateKey;


            Assert.NotNull(onboardingResult.GeneratedCsr);
            Assert.NotNull(onboardingResult.PrivateKey);

            Assert.NotEmpty(onboardingResult.GeneratedCsr);
            Assert.NotEmpty(onboardingResult.PrivateKey);
        }


        [Fact]
        public async Task Generate_Crs_And_GetCSID_Test()
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


            ZatcaOnboardingService api = new ZatcaOnboardingService();

            var result = await api.GetCSIDAsync(onboardingResult.GeneratedCsr, "12345");

            Assert.NotNull(result);


            Assert.NotNull(result.BinarySecurityToken);
            Assert.NotNull(result.RequestID);
            Assert.NotNull(result.Secret);

        }



        [Fact]
        public async Task Generate_Crs_And_CSID_Then_GetPCSID_Test()
        {

            //var onboardingResult = new OnboardingResultDto();


            var csrGenerationDto = ConstValue.CompanyTemplateTest.CrsCompanyInfo();
            var csrGenerator = new GeneratorCsr();



            var (generatedCsr, privateKey, errorMessages)
                = csrGenerator.GenerateCsrAndPrivateKey(csrGenerationDto, EnvironmentType.NonProduction, false);




            Assert.NotNull(generatedCsr);
            Assert.NotNull(privateKey);

            Assert.NotEmpty(generatedCsr);
            Assert.NotEmpty(privateKey);


            ZatcaOnboardingService api = new ZatcaOnboardingService();

            var ZatcaResult = await api.GetCSIDAsync(generatedCsr, "12345");

            Assert.NotNull(ZatcaResult);


            Assert.NotNull(ZatcaResult.BinarySecurityToken);
            Assert.NotNull(ZatcaResult.RequestID);
            Assert.NotNull(ZatcaResult.Secret);



            var resultPCSID = await api.GetPCSIDAsync(
                CsidComplianceRequestId: ZatcaResult.RequestID,
                CsidBinarySecurityToken: ZatcaResult.BinarySecurityToken,
                CsidSecret:  ZatcaResult.Secret);

            Assert.NotNull(resultPCSID.BinarySecurityToken);
            Assert.NotNull(resultPCSID.Secret);


        }




        [Fact]
        public async Task Generate_Crs_And_CSID_Then_GetPCSID_Then_Share_Invoice_ZATCA_Test()
        {

            //var onboardingResult = new OnboardingResultDto();


            var csrGenerationDto = ConstValue.CompanyTemplateTest.CrsCompanyInfo();
            var csrGenerator = new GeneratorCsr();



            var (generatedCsr, privateKey, errorMessages)
                = csrGenerator.GenerateCsrAndPrivateKey(csrGenerationDto, EnvironmentType.NonProduction, false);




            Assert.NotNull(generatedCsr);
            Assert.NotNull(privateKey);

            Assert.NotEmpty(generatedCsr);
            Assert.NotEmpty(privateKey);


            ZatcaOnboardingService api = new ZatcaOnboardingService();

            var ResultCSID = await api.GetCSIDAsync(generatedCsr, "12345");

            Assert.NotNull(ResultCSID);


            Assert.NotNull(ResultCSID.BinarySecurityToken);
            Assert.NotNull(ResultCSID.RequestID);
            Assert.NotNull(ResultCSID.Secret);



            var ResultPCSID = await api.GetPCSIDAsync(
                CsidComplianceRequestId: ResultCSID.RequestID,
                CsidBinarySecurityToken: ResultCSID.BinarySecurityToken,
                CsidSecret: ResultCSID.Secret);

            Assert.NotNull(ResultPCSID.BinarySecurityToken);
            Assert.NotNull(ResultPCSID.Secret);




            var invoiceObject = ConstValue.InvoicesTemplateTest.GetSimpleInvoice();

            GeneratorInvoice generatorInvoice = new GeneratorInvoice(invoiceObject,
            Encoding.UTF8.GetString(Convert.FromBase64String(ResultPCSID.BinarySecurityToken)),
            privateKey);

            var signeed = generatorInvoice.GetSignedInvoiceResult();

            ZatcaInvoiceService zatcaInvoiceService = new ZatcaInvoiceService();
            var reportInvoiceZatca = await zatcaInvoiceService.SendInvoiceToZatcaApi(signeed.RequestApi,
                PCSIDBinaryToken: ResultPCSID.BinarySecurityToken,
                PCSIDSecret: ResultPCSID.Secret, false);


            Assert.NotNull(reportInvoiceZatca);

            Assert.True(reportInvoiceZatca.IsSuccessStatusCode);

        }

    }
}
