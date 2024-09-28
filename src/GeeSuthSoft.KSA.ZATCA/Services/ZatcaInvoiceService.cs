using GeeSuthSoft.KSA.ZATCA.Dto;
using GeeSuthSoft.KSA.ZATCA.Helper;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace GeeSuthSoft.KSA.ZATCA.Services
{
    public class ZatcaInvoiceService
    {
        private const string ComplianceCheckUrl = "https://gw-fatoora.zatca.gov.sa/e-invoicing/developer-portal/compliance/invoices";


        public async Task<ServerResult> ComplianceCheck(string ccsidBinaryToken, 
            string ccsidSecret, 
            ZatcaRequestApi requestApi)
        {
            using (var _httpClient = new HttpClient())
            {
                try
                {
                    _httpClient.DefaultRequestHeaders.Clear();
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    _httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en"));
                    _httpClient.DefaultRequestHeaders.Add("Accept-Version", "V2");
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(Encoding.ASCII.GetBytes($"{ccsidBinaryToken}:{ccsidSecret}")));

                    var content = new StringContent(JsonConvert.SerializeObject(requestApi), Encoding.UTF8, "application/json");

                    var response = await _httpClient.PostAsync(ComplianceCheckUrl, content);
                    response.EnsureSuccessStatusCode();

                    var resultContent = await response.Content.ReadAsStringAsync();
                    var serverResult = JsonConvert.DeserializeObject<ServerResult>(resultContent);

                    return serverResult;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during compliance check: {ex.Message}");
                    throw;
                }
            }

          
        }


        public async Task<HttpResponseMessage> SendInvoiceToZatcaApi(ZatcaRequestApi zatcaRequestApi,
                 string PCSIDBinaryToken,
                 string PCSIDSecret,
                 bool IsClearance)
        {
            // TODO: OtherHelper needs to improving should not working like that, Just for test
            OtherHelper otherHelper = new OtherHelper();

            using (var _httpClient = new HttpClient())
            {
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en"));
                _httpClient.DefaultRequestHeaders.Add("Clearance-Status", IsClearance ? "1" : "0");
                _httpClient.DefaultRequestHeaders.Add("Accept-Version", "V2");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", 
                    Convert.ToBase64String(Encoding.ASCII.GetBytes($"{PCSIDBinaryToken}:{PCSIDSecret}")));

                var jsonContent = JsonConvert.SerializeObject(zatcaRequestApi);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var url = IsClearance ? otherHelper.ClearanceUrl : otherHelper.ReportingUrl;

                return await _httpClient.PostAsync(url, content);
            }

            
        }

    }
}
