using GeeSuthSoft.KSA.ZATCA.Xml.RootPaths;

namespace GeeSuthSoft.KSA.ZATCA.Dto;

public class ShareInvoiceResponseDto
{
    public ValidationResults? validationResults { get; set; }
    public string? reportingStatus { get; set; }
    public string? clearedInvoice { get; set; }
    public string? clearanceStatus { get; set; }
    
    public SignedInvoiceResult? SignedInvoiceResult { get; set; }
}

public class ValidationResults
{
    public InfoMessages[]? infoMessages { get; set; }
    public InfoMessages[]? warningMessages { get; set; }
    public InfoMessages[]? errorMessages { get; set; }
    public string? status { get; set; }
}

public class InfoMessages
{
    public string? type { get; set; }
    public string? code { get; set; }
    public string? category { get; set; }
    public string? message { get; set; }
    public string? status { get; set; }
}

public class ShareInvoiceRequestDto
{
    public Invoice invoiceObject { get; set; } = null!;
    public bool IsClearance { get; set; }
    public PCSIDInfoDto tokens { get; set; } = null!;
    
}

public record ShareReadyInvoiceRequestDto(ZatcaRequestApi zatcaRequestApi, string BinaryToken, string PCSIDSecret);