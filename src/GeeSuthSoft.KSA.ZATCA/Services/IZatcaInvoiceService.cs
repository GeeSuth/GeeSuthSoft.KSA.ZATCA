using GeeSuthSoft.KSA.ZATCA.Dto;

namespace GeeSuthSoft.KSA.ZATCA.Services
{
    public interface IZatcaInvoiceService
    {
        Task<ServerResult> ComplianceCheck(string ccsidBinaryToken, string ccsidSecret, ZatcaRequestApi requestApi);
        Task<HttpResponseMessage> SendInvoiceToZatcaApi(ZatcaRequestApi zatcaRequestApi, string PCSIDBinaryToken, string PCSIDSecret, bool IsClearance);
    }
}
