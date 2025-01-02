using GeeSuthSoft.KSA.ZATCA.Dto;
using GeeSuthSoft.KSA.ZATCA.Helper;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;

namespace GeeSuthSoft.KSA.ZATCA.Services
{

    public class ZatcaInvoiceService(
        IHttpClientFactory httpClientFactory,
        ILogger<ZatcaInvoiceService> logger,
        IZatcaApiConfig zatcaApiConfig)
        : LoggerHelper(zatcaApiConfig , logger: logger), IZatcaInvoiceService
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
                
                LogZatcaInfo($"Complianced Check...{requestApi.uuid} Invoice, ResponseCode: {response.StatusCode}");
                response.EnsureSuccessStatusCode();

                var resultContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ServerResult>(resultContent);
            }
            catch (Exception ex)
            {
                LogZatcaError(ex, "Error during compliance check");
                throw;
            }
        }

        public async Task<HttpResponseMessage> SendInvoiceToZatcaApi(ZatcaRequestApi zatcaRequestApi,
            string pcsidBinaryToken, string pcsidSecret, bool isClearance)
        {
            LogZatcaInfo($"Sending Invoice...{zatcaRequestApi.uuid} Invoice");
            
           try {
            var client = _httpClientFactory.CreateClient();
            ConfigureHttpClient(client, pcsidBinaryToken, pcsidSecret, isClearance);

            var content = new StringContent(JsonConvert.SerializeObject(zatcaRequestApi), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(_zatcaApiConfig.ReportingUrl, content);
            
            LogZatcaInfo($"Sending Invoice...{zatcaRequestApi.uuid} Invoice, ResponseCode: {response.StatusCode}");

            response.EnsureSuccessStatusCode();
            return response;
           }
           catch (Exception ex)
           {
               LogZatcaError(ex, "Error during compliance check");
               throw;
           }
        }

        private void ConfigureHttpClient(HttpClient client, string binaryToken, string secret, bool? isClearance = null)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en"));
            client.DefaultRequestHeaders.Add("Accept-Version", "V2");
            
            if (isClearance.HasValue)
            {
                client.DefaultRequestHeaders.Add("Clearance-Status", isClearance.Value ? "1" : "0");
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(Encoding.ASCII.GetBytes($"{binaryToken}:{secret}")));
        }
    }
}
