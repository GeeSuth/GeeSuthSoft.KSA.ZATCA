using GeeSuthSoft.KSA.ZATCA.Models;

namespace GeeSuthSoft.KSA.ZATCA.Services
{
    public interface IZatcaOnboardingService
    {
        GsCsrGenerationResultDto GenerateCsr(GsCsrGenerationDto csrGeneration, bool pemFormat = false);
        Task<GsZatcaResultDto> GetCSIDAsync(string GeneratedCsr, string? otp = "12345");
        Task<GsZatcaResultDto> GetPCSIDAsync(string CsidComplianceRequestId, string CsidBinarySecurityToken, string CsidSecret);
    }
}
