using GeeSuthSoft.KSA.ZATCA.Xml.RootPaths;

namespace GeeSuthSoft.KSA.ZATCA.Dto;

public class ShareInvoiceResponseDto
{
    public ValidationResults? validationResults { get; set; }
    public string? reportingStatus { get; set; }
    
    public SignedInvoiceResult? SignedInvoiceResult { get; set; }
}

public class ValidationResults
{
    public InfoMessages[]? infoMessages { get; set; }
    public object[]? warningMessages { get; set; }
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
    public required Invoice invoiceObject { get; set; } 
    public bool IsClearance { get; set; }
    public required PCSIDInfoDto tokens { get; set; }
    
}