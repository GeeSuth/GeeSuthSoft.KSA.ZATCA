using GeeSuthSoft.KSA.ZATCA.Models;

namespace GeeSuthSoft.KSA.ZATCA.Services
{
    public interface IZatcaInvoiceService
    {
        Task<GsServerResultComplianceDto> ComplianceCheck(string ccsidBinaryToken, string ccsidSecret, GsZatcaRequestApiDto requestApiDto);
        Task<HttpResponseMessage> SendInvoiceToZatcaApi(GsZatcaRequestApiDto requestApiDto, string PCSIDBinaryToken, string PCSIDSecret, bool IsClearance);
    }
}
