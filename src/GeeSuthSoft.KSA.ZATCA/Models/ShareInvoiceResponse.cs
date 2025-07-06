namespace GeeSuthSoft.KSA.ZATCA.Models;

internal class ShareInvoiceResponse
{
    public ValidationResults? validationResults { get; set; }
    public string? reportingStatus { get; set; }
    
    public SignedInvoiceResult? SignedInvoiceResult { get; set; }
}

internal class ValidationResults
{
    public InfoMessages[]? infoMessages { get; set; }
    public object[]? warningMessages { get; set; }
    public InfoMessages[]? errorMessages { get; set; }
    public string? status { get; set; }
}

internal class InfoMessages
{
    public string? type { get; set; }
    public string? code { get; set; }
    public string? category { get; set; }
    public string? message { get; set; }
    public string? status { get; set; }
}
