using GeeSuthSoft.KSA.ZATCA.Xml.RootPaths;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using GeeSuthSoft.KSA.ZATCA.Dto;
using GeeSuthSoft.KSA.ZATCA.Extensions;
using GeeSuthSoft.KSA.ZATCA.Helper;

namespace GeeSuthSoft.KSA.ZATCA.Generators
{
    internal class GeneratorInvoice(Invoice invoiceObject, string x509CertificateContent, string ecSecp256K1Privkeypem)
    {
        private Invoice InvoiceObject { get; } = invoiceObject;
        private string X509CertificateContent { get; } = x509CertificateContent;
        private string EcSecp256k1Privkeypem { get; } = ecSecp256K1Privkeypem;


        public SignedInvoiceResult GetSignedInvoiceResult()
        {
            return GetSignedInvoiceXML();
        }


        public SignedInvoiceResult GetSignedInvoiceXML()
        {
            SignedInvoiceResult result = new();
                
            string CleanInvoice = InvoiceObject.GetCleanInvoiceXML(false);
            result.InvoiceHash = SharedUtilities.GetBase64InvoiceHash(CleanInvoice);

                

            if (InvoiceObject.InvoiceTypeCode.Name.StartsWith("02"))
            {

                byte[] certificateBytes = Encoding.UTF8.GetBytes(X509CertificateContent);
                X509Certificate2 parsedCertificate = new(certificateBytes);

                string SignatureTimestamp = DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss");
                string PublicKeyHashing = Convert.ToBase64String(Encoding.UTF8.GetBytes(SharedUtilities.HashSha256AsString(X509CertificateContent)));
                string IssuerName = parsedCertificate.IssuerName.Name;
                string SerialNumber = SharedUtilities.GetSerialNumberForCertificateObject(parsedCertificate);
                string SignedPropertiesHash = SharedUtilities.GetSignedPropertiesHash(SignatureTimestamp, PublicKeyHashing, IssuerName, SerialNumber);


                string SignatureValue = SharedUtilities.GetDigitalSignature( result.InvoiceHash, EcSecp256k1Privkeypem);

                SignedUBL signedUBL = new(result. InvoiceHash,
                    SignedPropertiesHash,
                    SignatureValue,
                    X509CertificateContent,
                    SignatureTimestamp,
                    PublicKeyHashing,
                    IssuerName,
                    SerialNumber);

                result.Base64QrCodeContent = QrCodeGenerator.GenerateQRCodeContent(InvoiceObject, signedUBL);

                string stringXMLQrCode = SharedUtilities.ReadResource("ZatcaDataQr.xml").Replace("TLV_QRCODE_STRING", result.Base64QrCodeContent);
                string stringXMLSignature = SharedUtilities.ReadResource("ZatcaDataSignature.xml");
                string stringUBLExtension = signedUBL.ToString();

                int profileIDIndex = CleanInvoice.IndexOf("<cbc:ProfileID>", StringComparison.Ordinal);
                CleanInvoice = CleanInvoice.Insert(profileIDIndex - 6, stringUBLExtension);

                int AccountingSupplierPartyIndex = CleanInvoice.IndexOf("<cac:AccountingSupplierParty>", StringComparison.Ordinal);

                CleanInvoice = CleanInvoice.Insert(AccountingSupplierPartyIndex - 6, stringXMLQrCode);

                AccountingSupplierPartyIndex = CleanInvoice.IndexOf("<cac:AccountingSupplierParty>", StringComparison.Ordinal);

                CleanInvoice = CleanInvoice.Insert(AccountingSupplierPartyIndex - 6, stringXMLSignature);

            }

            byte[] bytes = Encoding.UTF8.GetBytes(CleanInvoice);
            result.Base64SignedInvoice = Convert.ToBase64String(bytes);

            // TODO: need to investigate cause it's repeated with above once.
            result.RequestApi = new ZatcaRequestApi()
            {
                invoiceHash = result. InvoiceHash,
                uuid = InvoiceObject.UUID,
                invoice = result.Base64SignedInvoice
            };

            string SellerIdentification = InvoiceObject.AccountingSupplierParty.Party.PartyTaxScheme.CompanyID.ToString();
            string IssueDate = InvoiceObject.IssueDate.Replace("-", "");
            string IssueTime = InvoiceObject.IssueTime.Replace(":", "");
            string InvoiceNumber = new string(InvoiceObject.ID.Value.ToString().Select(c => Path.GetInvalidFileNameChars().Contains(c) ? '_' : c).ToArray());

            result.XmlFileName = $"{SellerIdentification}_{IssueDate}{IssueTime}_{InvoiceNumber}.xml";

            return result;
        }

    }
}
