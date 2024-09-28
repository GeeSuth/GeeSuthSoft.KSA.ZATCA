using GeeSuthSoft.KSA.ZATCA.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GeeSuthSoft.KSA.ZATCA.Services
{
    public class ZatcaOnboardingService
    {

        private const string ComplianceCSIDUrl = "https://gw-fatoora.zatca.gov.sa/e-invoicing/developer-portal/compliance";
        private const string ProductionCSIDUrl = "https://gw-fatoora.zatca.gov.sa/e-invoicing/developer-portal/production/csids";



        /// <summary>
        /// Using to Onboarding Device
        /// </summary>
        /// <param name="GeneratedCsr"></param>
        /// <param name="otp"></param>
        /// <returns></returns>
        public async Task<ZatcaResultDto> GetCSIDAsync(string GeneratedCsr, string? otp = "12345")
        {

            using (var _httpClient = new HttpClient())
            {
                var jsonContent = JsonConvert.SerializeObject(new { csr = GeneratedCsr });

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en"));
                _httpClient.DefaultRequestHeaders.Add("OTP", otp);
                _httpClient.DefaultRequestHeaders.Add("Accept-Version", "V2");

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(ComplianceCSIDUrl, content);

                response.EnsureSuccessStatusCode();

                var resultContent = await response.Content.ReadAsStringAsync();
                var zatcaResult = JsonConvert.DeserializeObject<ZatcaResultDto>(resultContent);

                return zatcaResult;
            }


        }


        /// <summary>
        /// Using to Onboarding Device
        /// </summary>
        /// <param name="ComplanceZatcaResponse"></param>
        /// <returns></returns>
        public async Task<ZatcaResultDto> GetPCSIDAsync(string CsidComplianceRequestId,string CsidBinarySecurityToken, string CsidSecret)
        {
            using (var _httpClient = new HttpClient())
            {

                var jsonContent = JsonConvert.SerializeObject(new { compliance_request_id = CsidComplianceRequestId });

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en"));
                _httpClient.DefaultRequestHeaders.Add("Accept-Version", "V2");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.ASCII.GetBytes($"{CsidBinarySecurityToken}:{CsidSecret}")));

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(ProductionCSIDUrl, content);

                response.EnsureSuccessStatusCode();

                var resultContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ZatcaResultDto>(resultContent) ?? throw new Exception("ZATCA return non expection data");

            }

        }

    }
}
