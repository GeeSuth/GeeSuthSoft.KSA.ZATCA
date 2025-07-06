using GeeSuthSoft.KSA.ZATCA.Enums;

namespace GeeSuthSoft.KSA.ZATCA.Models;

public class ZatcaOptions
{
    public string? ZatcaBaseUrl { get; set; } = "https://gw-fatoora.zatca.gov.sa";
    public EnvironmentType Environment { get; set; } = EnvironmentType.NonProduction;

    public bool LogRequestAndResponse { get; set; } = false;
}
