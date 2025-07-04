using System.Net;
using System.Net.Http.Json;
using System.Text;
using GeeSuthSoft.KSA.ZATCA.Dto;
using GeeSuthSoft.KSA.ZATCA.Enums;
using GeeSuthSoft.KSA.ZATCA.Generators;
using GeeSuthSoft.KSA.ZATCA.Helper;
using GeeSuthSoft.KSA.ZATCA.Xml.RootPaths;
using Microsoft.Extensions.Logging;

namespace GeeSuthSoft.KSA.ZATCA.Services;

public class ZatcaShareService(IZatcaInvoiceService _zatcaInvoiceService,
    IZatcaApiConfig zatcaApiConfig,
    ILogger<ZatcaOnboardingService> logger) : LoggerHelper(zatcaApiConfig , logger: logger) , IZatcaShareService
{
    
    public async ValueTask<ShareInvoiceResponseDto> ShareInvoiceWithZatcaAsync(Invoice invoiceObject, 
        bool IsClearance,
        PCSIDInfoDto tokens)
    {
        try
        {
            LogZatcaInfo($"Sharing Invoice : {invoiceObject.ID}");
        
            var signedInvoice = new GeneratorInvoice(invoiceObject,
                Encoding.UTF8.GetString(Convert.FromBase64String(tokens.BinaryToken)),
                tokens.privateKey
            ).GetSignedInvoiceResult();
        
            LogZatcaInfo($"Signed Invoice : {invoiceObject.ID} , Hashed : {signedInvoice.InvoiceHash}");
        
            var result = await _zatcaInvoiceService.SendInvoiceToZatcaApi(
                zatcaRequestApi:signedInvoice.RequestApi,
                PCSIDBinaryToken: tokens.BinaryToken,
                PCSIDSecret : tokens.PCSIDSecret,
                IsClearance);


            LogZatcaInfo($"Sharing Invoice Response Status: {result.StatusCode}");
            if (result.StatusCode != HttpStatusCode.OK)
            {
                LogZatcaInfo($"Sharing Invoice Id: {invoiceObject.ID} Response Not 200_OK Error Response : {await result.Content.ReadAsStringAsync()}");
            }

            //result.EnsureSuccessStatusCode();
            
            var response = await result.Content.ReadFromJsonAsync<ShareInvoiceResponseDto>();
            response.SignedInvoiceResult = signedInvoice;
            return response;
        }
        catch (Exception ex)
        {
            LogZatcaError(ex,"Error with sharing invoice, the ZATCA response not as expected");
            throw;
        }
    }
}