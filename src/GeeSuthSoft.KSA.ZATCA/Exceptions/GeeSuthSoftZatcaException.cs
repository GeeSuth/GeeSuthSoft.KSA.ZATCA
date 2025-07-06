using System.Text;
using GeeSuthSoft.KSA.ZATCA.Models;

namespace GeeSuthSoft.KSA.ZATCA.Exceptions;

// Base exception for all ZATCA-related errors
internal class GeeSuthSoftZatcaException : Exception
{
    public GeeSuthSoftZatcaException(string message) : base(message) { }

    public GeeSuthSoftZatcaException(string message, Exception innerException) : base(message, innerException) { }
}

// Exception for incorrect configuration
internal class GeeSuthSoftZatcaInCorrectConfigException : GeeSuthSoftZatcaException
{
    public GeeSuthSoftZatcaInCorrectConfigException(string message)
        : base($"GS Incorrect configuration: {message}") { }
}

internal class GeeSuthSoftZatcaWorngUseException : GeeSuthSoftZatcaException
{
    public GeeSuthSoftZatcaWorngUseException(string message)
        : base($"GS Wrong Use: {message}") { }
}

// Exception for unexpected errors
internal class GeeSuthSoftZatcaUnExpectedException : GeeSuthSoftZatcaException
{
    public GeeSuthSoftZatcaUnExpectedException(Exception ex)
        : base($"GS Zatca Unexpected Error: {ex.Message}", ex) { }
}

// Exception for business rule errors
internal class GeeSuthSoftZatcaBusinessException : GeeSuthSoftZatcaException
{
    public GeeSuthSoftZatcaBusinessException(List<DetailInfo> detailInfo)
        : base($"GS Zatca Business Errors:\n{FormatDetails(detailInfo)}") { }

    public GeeSuthSoftZatcaBusinessException(InfoMessages[] detailInfo)
        : base($"GS Zatca Business Errors:\n{FormatDetails(detailInfo)}") { }

    private static string FormatDetails(IEnumerable<dynamic> details)
    {
        var sb = new StringBuilder();
        foreach (var detail in details)
        {
            sb.AppendLine($"{detail.Code ?? detail.code} / {detail.Message ?? detail.message}");
        }
        return sb.ToString();
    }
}