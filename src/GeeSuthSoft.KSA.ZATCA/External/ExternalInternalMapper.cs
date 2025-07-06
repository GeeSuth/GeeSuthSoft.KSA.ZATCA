using GeeSuthSoft.KSA.ZATCA.Models;
using GeeSuthSoft.KSA.ZATCA.Xml.RootPaths;

namespace GeeSuthSoft.KSA.ZATCA.External;

public static class ExternalInternalMapper
{
    internal static Invoice ToInvoice(this GsInvoiceDto invoiceDto)
    {

        return new Invoice();
    }

    internal static ZatcaRequestApi ZatcaRequestApi(this GsZatcaRequestApiDto requestDto)
    {
        
        return new ZatcaRequestApi();
    }
    
    internal static GsZatcaRequestApiDto ZatcaRequestApiDto(this ZatcaRequestApi requestDto)
    {
        return new GsZatcaRequestApiDto();
        
    }

    internal static GsCsrGenerationResultDto ToCsrGenerationResult(this CsrGenerationResult responseDto)
    {
        return new GsCsrGenerationResultDto();
        
    }
    
    internal static CsrGeneration ToCsrGenerationResult(this GsCsrGenerationDto responseDto)
    {
        return new CsrGeneration();
    }
    
    internal static PCSIDInfo ToTokens(this GsPCSIDInfoDto pcsidInfoDto)
    {

        return new PCSIDInfo();
    }

    internal static GsServerResultComplianceDto ToServerResult(this ServerResult serverResult)
    {
        
        return new GsServerResultComplianceDto();
    }

    internal static GsShareInvoiceResponseDto ToShareInvoiceResponse(this ShareInvoiceResponse invoiceResponseDto)
    {
        return new GsShareInvoiceResponseDto();
    }

    internal static GsZatcaResultDto ToZatcaResult(this ZatcaResult responseDto)
    {
        return new GsZatcaResultDto();
    }

    internal static SignedInvoiceResult TosignedInvoiceResult(this GsSignedInvoiceResultDto responseDto)
    {
        return new SignedInvoiceResult();
    }

}