using GeeSuthSoft.KSA.ZATCA.Enums;

namespace GeeSuthSoft.KSA.ZATCA.Dto;

public class ZatcaOptions
{
    public string? ZatcaBaseUrl { get; set; }
    public EnvironmentType Environment { get; set; }

    public bool LogRequestAndResponse { get; set; } = false;
}
