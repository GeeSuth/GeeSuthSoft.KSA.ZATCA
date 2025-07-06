using Newtonsoft.Json;

namespace GeeSuthSoft.KSA.ZATCA.Models;

internal class DetailInfo
{

    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("code")]
    public string Code { get; set; }

    [JsonProperty("category")]
    public string Category { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

}