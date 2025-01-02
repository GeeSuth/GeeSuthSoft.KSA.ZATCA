using System.Text;
using GeeSuthSoft.KSA.ZATCA.Generators;
using GeeSuthSoft.KSA.ZATCA.Services;
using GeeSuthSoft.KSA.ZATCA.XunitTest.ConstValue;
using GeeSuthSoft.KSA.ZATCA.XunitTest.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace GeeSuthSoft.KSA.ZATCA.XunitTest
{
    
    public class OnboardingDeviceTest : IClassFixture<ServiceProviderFixture>
    {
        private readonly IZatcaOnboardingService _zatcaOnboardingService;
        private readonly IZatcaInvoiceService _zatcaInvoiceService;

        public OnboardingDeviceTest(ServiceProviderFixture fixture)
        {
            _zatcaOnboardingService = fixture.ServiceProvider.GetRequiredService<IZatcaOnboardingService>();
            _zatcaInvoiceService = fixture.ServiceProvider.GetRequiredService<IZatcaInvoiceService>();
        }

        [Fact]
        public void Generate_Csr_Test()
        {
            // Arrange
            var csrGenerationDto = CompanyTemplateTest.CrsCompanyInfo();

            // Act
            var result = _zatcaOnboardingService.GenerateCsr(csrGenerationDto);

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Csr);
            Assert.NotNull(result.PrivateKey);
            Assert.NotEmpty(result.Csr);
            Assert.NotEmpty(result.PrivateKey);
        }

        [Fact]
        public async Task Generate_Crs_And_GetCSID_Test()
        {

            // Arrange
            var csrGenerationDto = CompanyTemplateTest.CrsCompanyInfo();

            // Act
            var CsrGenerationResultDto = _zatcaOnboardingService.GenerateCsr(csrGenerationDto);


            Assert.NotNull(CsrGenerationResultDto.Csr);
            Assert.NotNull(CsrGenerationResultDto.PrivateKey);

            Assert.NotEmpty(CsrGenerationResultDto.Csr);
            Assert.NotEmpty(CsrGenerationResultDto.PrivateKey);


            var result = await _zatcaOnboardingService.GetCSIDAsync(CsrGenerationResultDto.Csr);

            Assert.NotNull(result);


            Assert.NotNull(result.BinarySecurityToken);
            Assert.NotNull(result.RequestID);
            Assert.NotNull(result.Secret);

        }



        [Fact]
        public async Task Generate_Crs_And_CSID_Then_GetPCSID_Test()
        {

            //var onboardingResult = new OnboardingResultDto();


            // Arrange
            var csrGenerationDto = CompanyTemplateTest.CrsCompanyInfo();

            // Act
            var CsrGenerationResultDto = _zatcaOnboardingService.GenerateCsr(csrGenerationDto);


            Assert.NotNull(CsrGenerationResultDto.Csr);
            Assert.NotNull(CsrGenerationResultDto.PrivateKey);

            Assert.NotEmpty(CsrGenerationResultDto.Csr);
            Assert.NotEmpty(CsrGenerationResultDto.PrivateKey);







            var ZatcaResult = await _zatcaOnboardingService.GetCSIDAsync(CsrGenerationResultDto.Csr);

            Assert.NotNull(ZatcaResult);


            Assert.NotNull(ZatcaResult.BinarySecurityToken);
            Assert.NotNull(ZatcaResult.RequestID);
            Assert.NotNull(ZatcaResult.Secret);



            var resultPCSID = await _zatcaOnboardingService.GetPCSIDAsync(
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


       

            // Arrange
            var csrGenerationDto = CompanyTemplateTest.CrsCompanyInfo();

            // Act
            var CsrGenerationResultDto = _zatcaOnboardingService.GenerateCsr(csrGenerationDto);


            Assert.NotNull(CsrGenerationResultDto.Csr);
            Assert.NotNull(CsrGenerationResultDto.PrivateKey);

            Assert.NotEmpty(CsrGenerationResultDto.Csr);
            Assert.NotEmpty(CsrGenerationResultDto.PrivateKey);


            var ResultCSID = await _zatcaOnboardingService.GetCSIDAsync(CsrGenerationResultDto.Csr);

            Assert.NotNull(ResultCSID);


            Assert.NotNull(ResultCSID.BinarySecurityToken);
            Assert.NotNull(ResultCSID.RequestID);
            Assert.NotNull(ResultCSID.Secret);



            var ResultPCSID = await _zatcaOnboardingService.GetPCSIDAsync(
                CsidComplianceRequestId: ResultCSID.RequestID,
                CsidBinarySecurityToken: ResultCSID.BinarySecurityToken,
                CsidSecret: ResultCSID.Secret);

            Assert.NotNull(ResultPCSID.BinarySecurityToken);
            Assert.NotNull(ResultPCSID.Secret);




            var invoiceObject = InvoicesTemplateTest.GetSimpleInvoice();

            GeneratorInvoice generatorInvoice = new GeneratorInvoice(invoiceObject,
            Encoding.UTF8.GetString(Convert.FromBase64String(ResultPCSID.BinarySecurityToken)),CsrGenerationResultDto.PrivateKey);

            var signeed = generatorInvoice.GetSignedInvoiceResult();


            var reportInvoiceZatca = await _zatcaInvoiceService.SendInvoiceToZatcaApi(signeed.RequestApi,
                PCSIDBinaryToken: ResultPCSID.BinarySecurityToken,
                PCSIDSecret: ResultPCSID.Secret, false);


            Assert.NotNull(reportInvoiceZatca);

            Assert.True(reportInvoiceZatca.IsSuccessStatusCode);

        }

    }
}
