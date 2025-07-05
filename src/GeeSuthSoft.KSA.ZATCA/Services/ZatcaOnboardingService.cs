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
using GeeSuthSoft.KSA.ZATCA.Exceptions;

namespace GeeSuthSoft.KSA.ZATCA.Services
{
    public class ZatcaOnboardingService(
        IZatcaApiConfig zatcaApiConfig,
        IHttpClientFactory httpClientFactory,
        ILogger<ZatcaOnboardingService> logger)
        : LoggerHelper(zatcaApiConfig, logger: logger), IZatcaOnboardingService
    {
        private readonly IZatcaApiConfig _zatcaApiConfig =
            zatcaApiConfig ?? throw new GeeSuthSoftZatcaInCorrectConfigException(nameof(zatcaApiConfig));

        private readonly IHttpClientFactory _httpClientFactory =
            httpClientFactory ?? throw new GeeSuthSoftZatcaInCorrectConfigException(nameof(httpClientFactory));

        private readonly ILogger<ZatcaOnboardingService> _logger =
            logger ?? throw new GeeSuthSoftZatcaInCorrectConfigException(nameof(logger));


        public CsrGenerationResultDto GenerateCsr(CsrGenerationDto csrGenerationDto,
            bool pemFormat = false)
        {
            try
            {
                LogZatcaInfo($"Generate CSR : {csrGenerationDto.CommonName}");
                
                var csrGenerator = new GeneratorCsr();
                var (generatedCsr, privateKey, errorMessages)
                    = csrGenerator.GenerateCsrAndPrivateKey(csrGenerationDto, _zatcaApiConfig.Environment, pemFormat);

                LogZatcaInfo($"Generated CSR : {generatedCsr}");
                
                return new CsrGenerationResultDto()
                {
                    Csr = generatedCsr,
                    PrivateKey = privateKey
                };
            }
            catch (GeeSuthSoftZatcaWorngUseException) {throw;}
            catch (Exception ex)
            {
                LogZatcaError(ex.Message);
                _logger.LogError(ex, "Error generating CSR");
                throw new GeeSuthSoftZatcaUnExpectedException(ex);
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
                LogZatcaInfo($"Get CSID : {GeneratedCsr}");
                
                using var _httpClient = _httpClientFactory.CreateClient();
                var jsonContent = JsonConvert.SerializeObject(new {csr = GeneratedCsr});

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en"));
                _httpClient.DefaultRequestHeaders.Add("OTP", otp);
                _httpClient.DefaultRequestHeaders.Add("Accept-Version", "V2");

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_zatcaApiConfig.ComplianceCSIDUrl, content);

                LogZatcaInfo($"Get CSID Response Status Code: {response.StatusCode}");
                response.EnsureSuccessStatusCode();

                var resultContent = await response.Content.ReadAsStringAsync();
                var zatcaResult = JsonConvert.DeserializeObject<ZatcaResultDto>(resultContent);

                return zatcaResult;
            }
            catch (HttpRequestException ex)
            {
                LogZatcaError(ex, "Error occurred while sending request to ZATCA API");
                throw new GeeSuthSoftZatcaUnExpectedException(ex);
            }
            catch (JsonException ex)
            {
                LogZatcaError(ex, "Error occurred while deserializing ZATCA API response");
                throw new GeeSuthSoftZatcaUnExpectedException(ex);
            }
            catch (Exception ex)
            {
                LogZatcaError(ex, "Unexpected error occurred while getting CSID");
                throw new GeeSuthSoftZatcaUnExpectedException(ex);
            }
        }


        /// <summary>
        /// Using to Onboarding Device
        /// </summary>
        /// <param name="ComplanceZatcaResponse"></param>
        /// <returns></returns>
        public async Task<ZatcaResultDto> GetPCSIDAsync(string CsidComplianceRequestId, string CsidBinarySecurityToken,
            string CsidSecret)
        {
            try
            {
                LogZatcaInfo($"Get PCSID By Compliance Request Id : {CsidComplianceRequestId}");

                
                using var _httpClient = _httpClientFactory.CreateClient();
                var jsonContent = JsonConvert.SerializeObject(new {compliance_request_id = CsidComplianceRequestId});

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en"));
                _httpClient.DefaultRequestHeaders.Add("Accept-Version", "V2");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(Encoding.ASCII.GetBytes($"{CsidBinarySecurityToken}:{CsidSecret}")));

                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_zatcaApiConfig.ProductionCSIDUrl, content);

                LogZatcaInfo($"Get PCSID Response Status Code: {response.StatusCode}");
                
                //response.EnsureSuccessStatusCode();

                var resultContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ZatcaResultDto>(resultContent) ??
                       throw new Exception("ZATCA returned unexpected data");
            }
            catch (HttpRequestException ex)
            {
                LogZatcaError(ex, "Error occurred while sending request to ZATCA API for PCSID");
                throw new GeeSuthSoftZatcaUnExpectedException(ex);
            }
            catch (JsonException ex)
            {
                LogZatcaError(ex, "Error occurred while deserializing ZATCA API response for PCSID");
                throw new GeeSuthSoftZatcaUnExpectedException(ex);
            }
            catch (Exception ex)
            {
                LogZatcaError(ex, "Unexpected error occurred while getting PCSID");
                throw new GeeSuthSoftZatcaUnExpectedException(ex);
            }
        }
    }
}