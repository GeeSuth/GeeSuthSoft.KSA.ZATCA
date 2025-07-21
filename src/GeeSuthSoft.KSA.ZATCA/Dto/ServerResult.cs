using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeeSuthSoft.KSA.ZATCA.Dto
{
    public class ServerResult
    {
        [JsonProperty("requestType")]
        public string RequestType { get; set; }

        [JsonProperty("requestUrl")]
        public string RequestUrl { get; set; }

        [JsonProperty("statusCode")]
        public string StatusCode { get; set; }

        [JsonProperty("clearanceStatus")]
        public string ClearanceStatus { get; set; }

        [JsonProperty("reportingStatus")]
        public string ReportingStatus { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("uuid")]
        public string UUID { get; set; }

        [JsonProperty("reasonPhrase")]
        public string ReasonPhrase { get; set; }

        [JsonProperty("isSuccessStatusCode")]
        public string IsSuccessStatusCode { get; set; }

        [JsonProperty("validationResults")]
        public ValidationResult ValidationResults { get; set; }

        [JsonProperty("errorMessages")]
        public List<DetailInfo> ErrorMessages { get; set; }

        [JsonProperty("errors")]
        public List<DetailInfo> Errors { get; set; }

        [JsonProperty("warningMessages")]
        public List<DetailInfo> WarningMessages { get; set; }

        [JsonProperty("warnings")]
        public List<DetailInfo> Warnings { get; set; }

        [JsonProperty("clearedInvoice")]
        public string ClearedInvoice { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("invoiceHash")]
        public string InvoiceHash { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("qrBuyertStatus")]
        public string QrBuyerStatus { get; set; }

        [JsonProperty("qrSellertStatus")]
        public string QrSellerStatus { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

    }
}
