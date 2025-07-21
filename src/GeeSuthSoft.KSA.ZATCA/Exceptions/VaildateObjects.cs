using GeeSuthSoft.KSA.ZATCA.Dto;

namespace GeeSuthSoft.KSA.ZATCA.Exceptions;

public static class VaildateObjects
{
    public static void ValiDateZatcaResponse(this ShareInvoiceResponseDto? shareInvoiceResponseDto)      
    {
        if(shareInvoiceResponseDto == null)
            throw new GeeSuthSoftZatcaException("Zatca Response Faild to Parse or is null");


        if (shareInvoiceResponseDto.validationResults?.errorMessages?.Length != 0)
        {
            throw new GeeSuthSoftZatcaBusinessException(shareInvoiceResponseDto.validationResults.errorMessages);
        }
        
        
    }
}