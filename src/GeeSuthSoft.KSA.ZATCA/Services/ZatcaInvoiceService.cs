using GeeSuthSoft.KSA.ZATCA.Dto;
using GeeSuthSoft.KSA.ZATCA.Helper;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using GeeSuthSoft.KSA.ZATCA.Exceptions;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace GeeSuthSoft.KSA.ZATCA.Services
{

    public class ZatcaInvoiceService(
        IHttpClientFactory httpClientFactory,
        ILogger<ZatcaInvoiceService> logger,
        IZatcaApiConfig zatcaApiConfig)
        : LoggerHelper(zatcaApiConfig, logger: logger), IZatcaInvoiceService
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        private readonly ILogger<ZatcaInvoiceService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        private readonly IZatcaApiConfig _zatcaApiConfig = zatcaApiConfig ?? throw new ArgumentNullException(nameof(zatcaApiConfig));

        //private readonly LoggerHelper Zatcalogger;

        public async Task<ServerResult> ComplianceCheck(string ccsidBinaryToken, string ccsidSecret, ZatcaRequestApi requestApi)
        {
            LogZatcaInfo($"Compliance Check...{requestApi.uuid} Invoice");

            var client = _httpClientFactory.CreateClient();
            ConfigureHttpClient(client, ccsidBinaryToken, ccsidSecret);

            var content = new StringContent(JsonConvert.SerializeObject(requestApi), Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync(_zatcaApiConfig.ComplianceCheckUrl, content);
                var resultContent = await response.Content.ReadAsStringAsync();

                LogZatcaInfo($"Complianced Check...[{requestApi.uuid}] Invoice, ResponseCode: [{response.StatusCode}], ResponseBody: [{resultContent}]");
                //response.EnsureSuccessStatusCode();


                return JsonConvert.DeserializeObject<ServerResult>(resultContent);
            }
            catch (Exception ex)
            {
                LogZatcaError(ex, "Error during compliance check");
                throw new GeeSuthSoftZatcaUnExpectedException(ex);
            }
        }

        public async Task<HttpResponseMessage> SendInvoiceToZatcaApi(
           ZatcaRequestApi zatcaRequestApi,
           string pcsidBinaryToken,
           string pcsidSecret,
           bool isClearance,
           bool enableClearanceStatus = false)
        {
            LogZatcaInfo($"Sending Invoice...{zatcaRequestApi.uuid} Invoice " +
                         $"{(isClearance ? "(Standard - Clearance)" : $"(Simplified - Reporting, Clearance-Status: {(enableClearanceStatus ? "1" : "0")})")}");

            try
            {
                var client = _httpClientFactory.CreateClient();
                ConfigureHttpClient(client, pcsidBinaryToken, pcsidSecret, enableClearanceStatus);

                var content = new StringContent(JsonConvert.SerializeObject(zatcaRequestApi), Encoding.UTF8, "application/json");

                // Select the appropriate endpoint based on isClearance
                var endpoint = isClearance ? _zatcaApiConfig.ClearanceUrl : _zatcaApiConfig.ReportingUrl;

                var response = await client.PostAsync(endpoint, content);

                LogZatcaInfo($"Sent Invoice...{zatcaRequestApi.uuid} Invoice, Endpoint: {endpoint}, " +
                             $"ResponseCode: {response.StatusCode}, ResponseBody: {await response.Content.ReadAsStringAsync()}");

                return response;
            }
            catch (Exception ex)
            {
                LogZatcaError(ex, $"Error sending invoice {zatcaRequestApi.uuid} to ZATCA");
                throw new GeeSuthSoftZatcaUnExpectedException(ex);
            }
        }

        private void ConfigureHttpClient(HttpClient client, string binaryToken, string secret, bool enableClearanceStatus = false)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en"));
            client.DefaultRequestHeaders.Add("Accept-Version", "V2");
            client.DefaultRequestHeaders.Add("Clearance-Status", enableClearanceStatus ? "1" : "0");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(Encoding.ASCII.GetBytes($"{binaryToken}:{secret}")));
        }
    }
}
