using GeeSuthSoft.KSA.ZATCA.Dto;
using GeeSuthSoft.KSA.ZATCA.Xml.RootPaths;

namespace GeeSuthSoft.KSA.ZATCA.Services;

public interface IZatcaSignInvoiceService
{
    SignedInvoiceResult GetSignedInvoice(SignedInvoiceRequestDto InvoiceSign);
}