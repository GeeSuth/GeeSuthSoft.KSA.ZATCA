using GeeSuthSoft.KSA.ZATCA.Xml.RootPaths;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using GeeSuthSoft.KSA.ZATCA.Dto;
using GeeSuthSoft.KSA.ZATCA.Helper;

namespace GeeSuthSoft.KSA.ZATCA.Generators
{
    internal class GeneratorInvoice
    {
        public Invoice InvoiceObject { get; set; }
        public string X509CertificateContent { get; set; }
        public string EcSecp256k1Privkeypem { get; set; }

        public GeneratorInvoice(Invoice invoiceObject, string x509CertificateContent, string ecSecp256k1Privkeypem)
        {
            this.InvoiceObject = invoiceObject;

            this.X509CertificateContent = x509CertificateContent;
            this.EcSecp256k1Privkeypem = ecSecp256k1Privkeypem;

        }

        //public GeneratorInvoice(XmlDocument invoiceXml, string x509CertificateContent, string ecSecp256k1Privkeypem)
        //{
        //    try
        //    {
        //        using StringReader reader = new(invoiceXml.OuterXml);
        //        XmlSerializer serializer = new(typeof(Invoice));

        //        var clearedInvoiceObject = (Invoice)serializer.Deserialize(reader);

        //        this.InvoiceObject = clearedInvoiceObject;

        //        this.X509CertificateContent = x509CertificateContent;
        //        this.EcSecp256k1Privkeypem = ecSecp256k1Privkeypem;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        private string GetCleanInvoiceXML(bool applayXsl = true)
        {
            try
            {
                XmlSerializerNamespaces namespaces = new();
                namespaces.Add("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                namespaces.Add("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                namespaces.Add("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");

                var invoiceData = InvoiceObject.ObjectToXml(namespaces);

                //invoiceData = invoiceData.MoveLastAttributeToFirst();

                if (applayXsl) { invoiceData = invoiceData.ApplyXSLT(SharedUtilities.ReadResource("ZatcaDataInvoice.xsl"), true); }

                return invoiceData.ToFormattedXml();

            }
            catch (Exception)
            {
                //Console.WriteLine($"Error Get CleanInvoice XML: {ex.Message}");
                return null;
            }
        }

        public SignedInvoiceResult GetSignedInvoiceResult()
        {
            GetSignedInvoiceXML(out string invoiceHash, out string base64SignedInvoice, out string base64QrCode, out string xmlFileName, out ZatcaRequestApi requestApi);

            return new SignedInvoiceResult()
            {
                InvoiceHash = invoiceHash,
                Base64SignedInvoice = base64SignedInvoice,
                Base64QrCode = base64QrCode,
                XmlFileName = xmlFileName,
                RequestApi = requestApi
            };
        }


        public void GetSignedInvoiceXML(out string InvoiceHash, out string base64SignedInvoice, out string base64QrCode, out string XmlFileName, out ZatcaRequestApi requestApi)
        {
            try
            {
                string CleanInvoice = GetCleanInvoiceXML(false);
                InvoiceHash = SharedUtilities.GetBase64InvoiceHash(CleanInvoice);

                base64QrCode = "";

                if (InvoiceObject.InvoiceTypeCode.Name.StartsWith("02"))
                {

                    byte[] certificateBytes = Encoding.UTF8.GetBytes(X509CertificateContent);
                    X509Certificate2 parsedCertificate = new(certificateBytes);

                    string SignatureTimestamp = DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss");
                    string PublicKeyHashing = Convert.ToBase64String(Encoding.UTF8.GetBytes(SharedUtilities.HashSha256AsString(X509CertificateContent)));
                    string IssuerName = parsedCertificate.IssuerName.Name;
                    string SerialNumber = SharedUtilities.GetSerialNumberForCertificateObject(parsedCertificate);
                    string SignedPropertiesHash = SharedUtilities.GetSignedPropertiesHash(SignatureTimestamp, PublicKeyHashing, IssuerName, SerialNumber);


                    string SignatureValue = SharedUtilities.GetDigitalSignature(InvoiceHash, EcSecp256k1Privkeypem);

                    SignedUBL signedUBL = new(InvoiceHash,
                        SignedPropertiesHash,
                        SignatureValue,
                        X509CertificateContent,
                    SignatureTimestamp,
                    PublicKeyHashing,
                        IssuerName,
                        SerialNumber);

                    base64QrCode = QrCodeGenerator.GenerateQRCode(InvoiceObject, signedUBL);

                    string stringXMLQrCode = SharedUtilities.ReadResource("ZatcaDataQr.xml").Replace("TLV_QRCODE_STRING", base64QrCode);
                    string stringXMLSignature = SharedUtilities.ReadResource("ZatcaDataSignature.xml");
                    string stringUBLExtension = signedUBL.ToString();

                    int profileIDIndex = CleanInvoice.IndexOf("<cbc:ProfileID>");
                    CleanInvoice = CleanInvoice.Insert(profileIDIndex - 6, stringUBLExtension);

                    int AccountingSupplierPartyIndex = CleanInvoice.IndexOf("<cac:AccountingSupplierParty>");

                    CleanInvoice = CleanInvoice.Insert(AccountingSupplierPartyIndex - 6, stringXMLQrCode);

                    AccountingSupplierPartyIndex = CleanInvoice.IndexOf("<cac:AccountingSupplierParty>");

                    CleanInvoice = CleanInvoice.Insert(AccountingSupplierPartyIndex - 6, stringXMLSignature);

                }

                byte[] bytes = Encoding.UTF8.GetBytes(CleanInvoice);
                base64SignedInvoice = Convert.ToBase64String(bytes);

                requestApi = new ZatcaRequestApi()
                {
                    invoiceHash = InvoiceHash,
                    uuid = InvoiceObject.UUID,
                    invoice = base64SignedInvoice
                };

                string SellerIdentification = InvoiceObject.AccountingSupplierParty.Party.PartyTaxScheme.CompanyID.ToString();
                string IssueDate = InvoiceObject.IssueDate.Replace("-", "");
                string IssueTime = InvoiceObject.IssueTime.Replace(":", "");
                string InvoiceNumber = new string(InvoiceObject.ID.Value.ToString().Select(c => Path.GetInvalidFileNameChars().Contains(c) ? '_' : c).ToArray());

                XmlFileName = $"{SellerIdentification}_{IssueDate}{IssueTime}_{InvoiceNumber}.xml";

            }
            catch (Exception)
            {
                //Console.WriteLine($"Error Get SignedInvoice XML: {ex.Message}");
                throw;
            }
        }

    }
}
