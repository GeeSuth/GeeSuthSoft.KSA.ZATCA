﻿using System.Net;
using System.Net.Http.Json;
using System.Text;
using GeeSuthSoft.KSA.ZATCA.Enums;
using GeeSuthSoft.KSA.ZATCA.Exceptions;
using GeeSuthSoft.KSA.ZATCA.External;
using GeeSuthSoft.KSA.ZATCA.Generators;
using GeeSuthSoft.KSA.ZATCA.Helper;
using GeeSuthSoft.KSA.ZATCA.Models;
using GeeSuthSoft.KSA.ZATCA.Xml.RootPaths;
using Microsoft.Extensions.Logging;

namespace GeeSuthSoft.KSA.ZATCA.Services;

public class ZatcaShareService(IZatcaInvoiceService _zatcaInvoiceService,
    IZatcaApiConfig zatcaApiConfig,
    ILogger<ZatcaOnboardingService> logger) : LoggerHelper(zatcaApiConfig , logger: logger) , IZatcaShareService
{
    
    public async ValueTask<GsShareInvoiceResponseDto> ShareInvoiceWithZatcaAsync(GsInvoiceDto invoice, 
        bool IsClearance,
        GsPCSIDInfoDto pcsidInfoDto)
    {
        try
        {
            var invoiceObject = invoice.ToInvoice();
            var tokens = pcsidInfoDto.ToTokens();
            if (!tokens.isValid)
            {
                throw new GeeSuthSoftZatcaInCorrectConfigException("Tokens are invalid");
            }
            
            LogZatcaInfo($"Sharing Invoice : {invoiceObject.ID}");
        
            var signedInvoice = new GeneratorInvoice(invoiceObject,
                Encoding.UTF8.GetString(Convert.FromBase64String(tokens.BinaryToken)),
                tokens.privateKey
            ).GetSignedInvoiceResult();
        
            LogZatcaInfo($"Signed Invoice : {invoiceObject.ID} , Hashed : {signedInvoice.InvoiceHash}");
        
            var result = await _zatcaInvoiceService.SendInvoiceToZatcaApi(
                signedInvoice.RequestApi.ZatcaRequestApiDto(),
                PCSIDBinaryToken: tokens.BinaryToken,
                PCSIDSecret : tokens.PCSIDSecret,
                IsClearance);


            LogZatcaInfo($"Sharing Invoice Response Status: {result.StatusCode}");
            if (result.StatusCode != HttpStatusCode.OK)
            {
                LogZatcaInfo($"Sharing Invoice Id: {invoiceObject.ID} Response Not 200_OK Error Response : {await result.Content.ReadAsStringAsync()}");
            }

            //result.EnsureSuccessStatusCode();
            
            var response = await result.Content.ReadFromJsonAsync<ShareInvoiceResponse>();
            response.ValiDateZatcaResponse();
            
            response.SignedInvoiceResult = signedInvoice;
            return response.ToShareInvoiceResponse();
        }
        catch (Exception ex)
        {
            LogZatcaError(ex,"Error with sharing invoice, the ZATCA response not as expected");
            throw new GeeSuthSoftZatcaUnExpectedException(ex);
        }
    }
}