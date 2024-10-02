using GeeSuthSoft.KSA.ZATCA.Dto;

namespace GeeSuthSoft.KSA.ZATCA.Services
{
    public interface IZatcaOnboardingService
    {
        CsrGenerationResultDto GenerateCsr(CsrGenerationDto csrGenerationDto, bool pemFormat = false);
        Task<ZatcaResultDto> GetCSIDAsync(string GeneratedCsr, string? otp = "12345");
        Task<ZatcaResultDto> GetPCSIDAsync(string CsidComplianceRequestId, string CsidBinarySecurityToken, string CsidSecret);
    }
}
