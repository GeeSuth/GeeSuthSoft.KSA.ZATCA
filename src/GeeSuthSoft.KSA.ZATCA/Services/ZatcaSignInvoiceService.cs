using System.Text;
using GeeSuthSoft.KSA.ZATCA.Dto;
using GeeSuthSoft.KSA.ZATCA.Exceptions;
using GeeSuthSoft.KSA.ZATCA.Generators;
using GeeSuthSoft.KSA.ZATCA.Helper;
using GeeSuthSoft.KSA.ZATCA.Xml.RootPaths;
using Microsoft.Extensions.Logging;

namespace GeeSuthSoft.KSA.ZATCA.Services;

public class ZatcaSignInvoiceService (ILogger<ZatcaInvoiceService> logger,
     IZatcaApiConfig zatcaApiConfig) : 
     LoggerHelper(zatcaApiConfig , logger: logger), IZatcaSignInvoiceService
{
    public SignedInvoiceResult GetSignedInvoice(SignedInvoiceRequestDto InvoiceSign)
    {
        try
        {

            GeneratorInvoice ig = new(
                InvoiceSign.Invoice,
                Encoding.UTF8.GetString(Convert.FromBase64String(InvoiceSign.BinaryToken)),
                InvoiceSign.Secret
            );

            return ig.GetSignedInvoiceXML();
                
        }
        catch (Exception ex)
        {
            LogZatcaInfo($"Error creating signed invoice Id {InvoiceSign.Invoice.ID} : {ex.Message}");
            throw new GeeSuthSoftZatcaUnExpectedException(ex);
        }
    }
}