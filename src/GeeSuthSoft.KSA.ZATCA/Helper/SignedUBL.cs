using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeeSuthSoft.KSA.ZATCA.Helper
{
    internal class SignedUBL(string invoiceHash, string signedPropertiesHash, string signatureValue, string certificateContent, string signatureTimestamp, string publicKeyHashing, string issuerName, string serialNumber)
    {
        public string InvoiceHash { get; set; } = invoiceHash;
        public string SignedPropertiesHash { get; set; } = signedPropertiesHash;
        public string SignatureValue { get; set; } = signatureValue;
        public string CertificateContent { get; set; } = certificateContent;
        public string SignatureTimestamp { get; set; } = signatureTimestamp;
        public string PublicKeyHashing { get; set; } = publicKeyHashing;
        public string IssuerName { get; set; } = issuerName;
        public string SerialNumber { get; set; } = serialNumber;

        public override string ToString()
        {
            string stringUBLExtension = SharedUtilities.ReadResource("ZatcaDataUbl.xml");
            stringUBLExtension = stringUBLExtension.
                Replace("INVOICE_HASH", InvoiceHash).
                Replace("SIGNED_PROPERTIES", SignedPropertiesHash).
                Replace("SIGNATURE_VALUE", SignatureValue).
                Replace("CERTIFICATE_CONTENT", CertificateContent).
                Replace("SIGNATURE_TIMESTAMP", SignatureTimestamp).
                Replace("PUBLICKEY_HASHING", PublicKeyHashing).
                Replace("ISSUER_NAME", IssuerName).
                Replace("SERIAL_NUMBER", SerialNumber);

            return stringUBLExtension;
        }
    }
}
