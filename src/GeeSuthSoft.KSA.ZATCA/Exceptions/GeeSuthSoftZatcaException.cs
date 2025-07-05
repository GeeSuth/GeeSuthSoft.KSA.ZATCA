using GeeSuthSoft.KSA.ZATCA.Dto;
using System.Text;

namespace GeeSuthSoft.KSA.ZATCA.Exceptions;

// Base exception for all ZATCA-related errors
public class GeeSuthSoftZatcaException : Exception
{
    public GeeSuthSoftZatcaException(string message) : base(message) { }

    public GeeSuthSoftZatcaException(string message, Exception innerException) : base(message, innerException) { }
}

// Exception for incorrect configuration
public class GeeSuthSoftZatcaInCorrectConfigException : GeeSuthSoftZatcaException
{
    public GeeSuthSoftZatcaInCorrectConfigException(string message)
        : base($"GS Incorrect configuration: {message}") { }
}

public class GeeSuthSoftZatcaWorngUseException : GeeSuthSoftZatcaException
{
    public GeeSuthSoftZatcaWorngUseException(string message)
        : base($"GS Wrong Use: {message}") { }
}

// Exception for unexpected errors
public class GeeSuthSoftZatcaUnExpectedException : GeeSuthSoftZatcaException
{
    public GeeSuthSoftZatcaUnExpectedException(Exception ex)
        : base($"GS Zatca Unexpected Error: {ex.Message}", ex) { }
}

// Exception for business rule errors
public class GeeSuthSoftZatcaBusinessException : GeeSuthSoftZatcaException
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