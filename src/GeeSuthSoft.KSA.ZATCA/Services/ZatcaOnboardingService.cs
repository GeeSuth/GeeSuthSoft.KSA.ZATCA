using GeeSuthSoft.KSA.ZATCA.Dto;
using GeeSuthSoft.KSA.ZATCA.Enums;
using GeeSuthSoft.KSA.ZATCA.Generators;
using GeeSuthSoft.KSA.ZATCA.Helper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GeeSuthSoft.KSA.ZATCA.Services
{

    public class ZatcaOnboardingService : IZatcaOnboardingService
    {

        private readonly IZatcaApiConfig _zatcaApiConfig;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<ZatcaOnboardingService> _logger;

        public ZatcaOnboardingService(IZatcaApiConfig zatcaApiConfig, 
                                    IHttpClientFactory httpClientFactory,
                                    ILogger<ZatcaOnboardingService> logger)
        {
            _zatcaApiConfig = zatcaApiConfig ?? throw new ArgumentNullException(nameof(zatcaApiConfig));
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public CsrGenerationResultDto GenerateCsr(CsrGenerationDto csrGenerationDto,
            bool pemFormat = false) 
        {
            try {
                var csrGenerator = new GeneratorCsr();
                var (generatedCsr, privateKey, errorMessages)
                    = csrGenerator.GenerateCsrAndPrivateKey(csrGenerationDto, _zatcaApiConfig.Environment, pemFormat);

                return new CsrGenerationResultDto()
                {
                    Csr = generatedCsr,
                    PrivateKey = privateKey
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating CSR");
                throw;
            }
        }

        /// <summary>
        /// Using to Onboarding Device
        /// </summary>
        /// <param name="GeneratedCsr"></param>
        /// <param name="otp"></param>
        /// <returns></returns>
        public async Task<ZatcaResultDto> GetCSIDAsync(string GeneratedCsr, string? otp = "12345")
        {

            try
            {
                using (var _httpClient = _httpClientFactory.CreateClient())
                {
                    var jsonContent = JsonConvert.SerializeObject(new { csr = GeneratedCsr });

                    _httpClient.DefaultRequestHeaders.Clear();
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    _httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en"));
                    _httpClient.DefaultRequestHeaders.Add("OTP", otp);
                    _httpClient.DefaultRequestHeaders.Add("Accept-Version", "V2");

                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    var response = await _httpClient.PostAsync(_zatcaApiConfig.ComplianceCSIDUrl, content);

                    response.EnsureSuccessStatusCode();

                    var resultContent = await response.Content.ReadAsStringAsync();
                    var zatcaResult = JsonConvert.DeserializeObject<ZatcaResultDto>(resultContent);

                    return zatcaResult;
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error occurred while sending request to ZATCA API");
                throw;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error occurred while deserializing ZATCA API response");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while getting CSID");
                throw;
            }


        }


        /// <summary>
        /// Using to Onboarding Device
        /// </summary>
        /// <param name="ComplanceZatcaResponse"></param>
        /// <returns></returns>
        public async Task<ZatcaResultDto> GetPCSIDAsync(string CsidComplianceRequestId,string CsidBinarySecurityToken, string CsidSecret)
        {
            try
            {
                using (var _httpClient = _httpClientFactory.CreateClient())
                {
                    var jsonContent = JsonConvert.SerializeObject(new { compliance_request_id = CsidComplianceRequestId });

                    _httpClient.DefaultRequestHeaders.Clear();
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    _httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en"));
                    _httpClient.DefaultRequestHeaders.Add("Accept-Version", "V2");
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(Encoding.ASCII.GetBytes($"{CsidBinarySecurityToken}:{CsidSecret}")));

                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    var response = await _httpClient.PostAsync(_zatcaApiConfig.ProductionCSIDUrl, content);

                    response.EnsureSuccessStatusCode();

                    var resultContent = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ZatcaResultDto>(resultContent) ?? throw new Exception("ZATCA returned unexpected data");
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error occurred while sending request to ZATCA API for PCSID");
                throw;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Error occurred while deserializing ZATCA API response for PCSID");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while getting PCSID");
                throw;
            }

        }




    }
}
