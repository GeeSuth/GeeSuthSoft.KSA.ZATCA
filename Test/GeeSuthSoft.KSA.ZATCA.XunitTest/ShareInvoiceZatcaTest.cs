using GeeSuthSoft.KSA.ZATCA.Dto;
using GeeSuthSoft.KSA.ZATCA.Services;
using GeeSuthSoft.KSA.ZATCA.XunitTest.ConstValue;
using GeeSuthSoft.KSA.ZATCA.XunitTest.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace GeeSuthSoft.KSA.ZATCA.XunitTest;

public class ShareInvoiceZatcaTest(ServiceProviderFixture fixture) : IClassFixture<ServiceProviderFixture>
{
    private readonly IZatcaShareService _zatcaShareService = fixture.ServiceProvider.GetRequiredService<IZatcaShareService>();


    /*
    [Fact]
    public async Task ShareInvoicewithInitnalTest()
    {
        var tokens = new PCSIDInfoDto();
        
        
        // Arrange
        var csrGenerationDto = ConstValue.CompanyTemplateTest.CrsCompanyInfo();

        // Act
        var CsrGenerationResultDto = _zatcaOnboardingService.GenerateCsr(csrGenerationDto, false);
        
        
        Assert.NotNull(CsrGenerationResultDto.Csr);
        Assert.NotNull(CsrGenerationResultDto.PrivateKey);
        
        
        //tokens.privateKey = CsrGenerationResultDto.PrivateKey;
        
        /* ============================================================================================================ #1#
        
        
        var ZatcaResult = await _zatcaOnboardingService.GetCSIDAsync(CsrGenerationResultDto.Csr, "12345");

        Assert.NotNull(ZatcaResult);


        Assert.NotNull(ZatcaResult.BinarySecurityToken);
        Assert.NotNull(ZatcaResult.RequestID);
        Assert.NotNull(ZatcaResult.Secret);
        
        /* ============================================================================================================ #1#
        
        var resultPCSID = await _zatcaOnboardingService.GetPCSIDAsync(
            CsidComplianceRequestId: ZatcaResult.RequestID,
            CsidBinarySecurityToken: ZatcaResult.BinarySecurityToken,
            CsidSecret:  ZatcaResult.Secret);

        Assert.NotNull(resultPCSID.BinarySecurityToken);
        Assert.NotNull(resultPCSID.Secret);
        
        tokens.PCSIDSecret = resultPCSID.Secret;
        tokens.BinaryToken = resultPCSID.BinarySecurityToken;
        
        /* ============================================================================================================ #1#
        
     

        var resultShare = await _zatcaShareService.ShareInvoiceWithZatcaAsync(ConstValue.InvoicesTemplateTest.GetSimpleInvoice() ,
            true, tokens);
        
        Assert.True(resultShare);
    }
    */

    [Fact]
    public async Task ShareCorrectInvoiceTest()
    {
        var pcsidTokens = new PCSIDInfoDto
        {
            BinaryToken = AuthTest.PCSIDBinaryToken,
            PCSIDSecret = AuthTest.PCSIDSecret,
            privateKey = AuthTest.CrsPrivateKey
        };
      
        var resultShare = await _zatcaShareService.ShareInvoiceWithZatcaAsync(InvoicesTemplateTest.GetSimpleInvoice() ,
            true, pcsidTokens);
        
        Assert.True(resultShare.reportingStatus == "REPORTED");
    }
    
    [Fact]
    public Task ShareWrongInvoiceTest()
    {
        var pcsidTokens = new PCSIDInfoDto
        {
            BinaryToken = AuthTest.PCSIDBinaryToken,
            PCSIDSecret = AuthTest.PCSIDSecret,
            privateKey = AuthTest.CrsPrivateKey
        };
      
        Assert.ThrowsAnyAsync<NullReferenceException>(async () =>
        {
            await _zatcaShareService.ShareInvoiceWithZatcaAsync(new() ,
                true, pcsidTokens);

        });
        return Task.CompletedTask;
    }
}