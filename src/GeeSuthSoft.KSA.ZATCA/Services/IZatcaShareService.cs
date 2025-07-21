using GeeSuthSoft.KSA.ZATCA.Dto;
using GeeSuthSoft.KSA.ZATCA.Xml.RootPaths;

namespace GeeSuthSoft.KSA.ZATCA.Services;

public interface IZatcaShareService
{
    ValueTask<ShareInvoiceResponseDto> ShareInvoiceWithZatcaAsync(ShareInvoiceRequestDto shareInvoiceRequestDto);
    
}
