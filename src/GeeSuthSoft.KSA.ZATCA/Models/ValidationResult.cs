using Newtonsoft.Json;

namespace GeeSuthSoft.KSA.ZATCA.Models;

internal class ValidationResult
{
    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("infoMessages")]
    public List<DetailInfo> InfoMessages { get; set; }

    [JsonProperty("warningMessages")]
    public List<DetailInfo> WarningMessages { get; set; }

    [JsonProperty("errorMessages")]
    public List<DetailInfo> ErrorMessages { get; set; }

}