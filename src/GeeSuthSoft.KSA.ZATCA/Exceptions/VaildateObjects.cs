using GeeSuthSoft.KSA.ZATCA.Models;

namespace GeeSuthSoft.KSA.ZATCA.Exceptions;

public static class VaildateObjects
{
    internal static void ValiDateZatcaResponse(this ShareInvoiceResponse? shareInvoiceResponseDto)      
    {
        if(shareInvoiceResponseDto == null)
            throw new GeeSuthSoftZatcaException("Zatca Response Faild to Parse or is null");


        if (shareInvoiceResponseDto.validationResults?.errorMessages?.Length != 0)
        {
            throw new GeeSuthSoftZatcaBusinessException(shareInvoiceResponseDto.validationResults.errorMessages);
        }
        
        
    }
}