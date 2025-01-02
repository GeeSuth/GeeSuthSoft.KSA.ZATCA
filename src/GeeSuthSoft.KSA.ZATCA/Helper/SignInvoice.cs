using GeeSuthSoft.KSA.ZATCA.Dto;
using GeeSuthSoft.KSA.ZATCA.Generators;
using GeeSuthSoft.KSA.ZATCA.Xml.RootPaths;
using System.Text;

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

                return ig.GetSignedInvoiceXML();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($@"Error creating signed invoice: {ex.Message}");
                throw;
            }
        }
    }
}
