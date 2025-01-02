using GeeSuthSoft.KSA.ZATCA.Services;
using GeeSuthSoft.KSA.ZATCA.XunitTest.ConstValue;
using GeeSuthSoft.KSA.ZATCA.XunitTest.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace GeeSuthSoft.KSA.ZATCA.XunitTest;

public class GenerateCRsTest : IClassFixture<ServiceProviderFixture>
{

     private readonly IZatcaOnboardingService _zatcaOnboardingService;

        public GenerateCRsTest(ServiceProviderFixture fixture)
        {
            _zatcaOnboardingService = fixture.ServiceProvider.GetRequiredService<IZatcaOnboardingService>();
        }

    [Fact]
    public void Generate_Crs_Test()
    {
        
        var csrGenerationDto = CompanyTemplateTest.CrsCompanyInfo();
        var csrGenerationResultDto = _zatcaOnboardingService.GenerateCsr(csrGenerationDto);

        Assert.NotNull(csrGenerationResultDto);
        Assert.NotNull(csrGenerationResultDto.Csr);
        Assert.NotNull(csrGenerationResultDto.PrivateKey);
    }


    [Fact]
    public void Generate_Crs_Test_shouldFail()
    {

        var csrGenerationDto = CompanyTemplateTest.CrsCompanyInfo("");
        var exception = Assert.Throws<Exception>(() => _zatcaOnboardingService.GenerateCsr(csrGenerationDto));
        Assert.StartsWith("CSR configuration is not valid. Errors:", exception.Message, StringComparison.OrdinalIgnoreCase);

    }
}

