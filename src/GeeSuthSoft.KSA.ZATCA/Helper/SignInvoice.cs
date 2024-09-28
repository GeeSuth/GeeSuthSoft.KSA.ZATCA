using GeeSuthSoft.KSA.ZATCA.Dto;
using GeeSuthSoft.KSA.ZATCA.Enums;
using GeeSuthSoft.KSA.ZATCA.Generators;
using GeeSuthSoft.KSA.ZATCA.Xml.RootPaths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeeSuthSoft.KSA.ZATCA.Helper
{
    [Obsolete("Need to re-wrire", false)]
    internal class SignInvoice
    {
        public SignedInvoiceResult GenerateSignedInvoice(Invoice invoiceObject, 
            string BinaryToken, 
            string Secret)
        {
            try
            {

                GeneratorInvoice ig = new(
                    invoiceObject,
                    Encoding.UTF8.GetString(Convert.FromBase64String(BinaryToken)),
                    Secret
                );

                ig.GetSignedInvoiceXML(out string invoiceHash, 
                    out string base64SignedInvoice, 
                    out string base64QrCode, 
                    out string XmlFileName, 
                    out ZatcaRequestApi requestApi);

                return new SignedInvoiceResult
                {
                    Base64SignedInvoice = base64SignedInvoice,
                    Base64QrCode = base64QrCode,
                    XmlFileName = XmlFileName,
                    RequestApi = requestApi
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating signed invoice: {ex.Message}");
                throw;
            }
        }
    }
}
