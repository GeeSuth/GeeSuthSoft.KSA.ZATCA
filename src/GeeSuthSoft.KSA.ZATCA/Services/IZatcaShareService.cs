using GeeSuthSoft.KSA.ZATCA.Models;
using GeeSuthSoft.KSA.ZATCA.Xml.RootPaths;

namespace GeeSuthSoft.KSA.ZATCA.Services;

public interface IZatcaShareService
{
    ValueTask<GsShareInvoiceResponseDto> ShareInvoiceWithZatcaAsync(GsInvoiceDto invoice, bool IsClearance,GsPCSIDInfoDto tokens);
    
}
