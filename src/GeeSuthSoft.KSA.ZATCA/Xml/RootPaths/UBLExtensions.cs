using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GeeSuthSoft.KSA.ZATCA.Xml.RootPaths
{
    public class UBLExtensions
    {
        [XmlElement(ElementName = "UBLExtension", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")]
        public UBLExtension UBLExtension { get; set; }

        public UBLExtensions() { }
    }
    public class UBLExtension
    {
        [XmlElement(ElementName = "ExtensionURI", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")]
        public string ExtensionURI { get; set; } = "urn:oasis:names:specification:ubl:dsig:enveloped:xades";

        [XmlElement(ElementName = "ExtensionContent", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2")]
        public ExtensionContent ExtensionContent { get; set; }
    }

    public class ExtensionContent
    {
        [XmlElement(ElementName = "UBLDocumentSignatures", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonSignatureComponents-2")]
        public UBLDocumentSignatures UBLDocumentSignatures { get; set; }
    }

    public class UBLDocumentSignatures
    {
        [XmlElement("SignatureInformation", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:SignatureAggregateComponents-2")]
        public SignatureInformation SignatureInformation { get; set; }

        [XmlAttribute(AttributeName = "sig", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Sig { get; set; }

        [XmlAttribute(AttributeName = "sac", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Sac { get; set; }

        [XmlAttribute(AttributeName = "sbc", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Sbc { get; set; }

    }

    public class SignatureInformation
    {
        [XmlElement("ID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public ID ID { get; set; } = new ID { Value = "urn:oasis:names:specification:ubl:signature:1" };

        [XmlElement("ReferencedSignatureID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:SignatureBasicComponents-2")]
        public string ReferencedSignatureID { get; set; } = "urn:oasis:names:specification:ubl:signature:Invoice";

        [XmlElement(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public Signature Signature { get; set; }
    }

    public class Signature
    {
        [XmlElement(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public SignedInfo SignedInfo { get; set; }

        [XmlAttribute(AttributeName = "ds", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Ds { get; set; }

        [XmlAttribute(AttributeName = "Id")]
        public string Id { get; set; }

        [XmlElement(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public string SignatureValue { get; set; }

        [XmlElement(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public KeyInfo KeyInfo { get; set; }

        [XmlElement("Object", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public SignatureObject SignatureObject { get; set; }

        [XmlElement(ElementName = "ID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public ID ID { get; set; }

        [XmlElement(ElementName = "SignatureMethod", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string SignatureMethod { get; set; }
    }

    public class SignatureObject
    {
        [XmlElement("QualifyingProperties", Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
        public QualifyingProperties QualifyingProperties { get; set; }
    }

    public class QualifyingProperties
    {
        [XmlAttribute(AttributeName = "xades", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xades { get; set; }

        [XmlAttribute(AttributeName = "Target")]
        public string Target { get; set; } = "signature";

        [XmlElement(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
        public SignedProperties SignedProperties { get; set; }

    }

    public class SignedProperties
    {

        [XmlAttribute]
        public string Id { get; set; } = "xadesSignedProperties";

        [XmlElement(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
        public SignedSignatureProperties SignedSignatureProperties { get; set; }

    }
    public class SignedInfo
    {
        [XmlElement(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public CanonicalizationMethod CanonicalizationMethod { get; set; }

        [XmlElement(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public SignatureMethod SignatureMethod { get; set; }

        [XmlElement("Reference", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public Reference[] Reference { get; set; }
    }

    public class CanonicalizationMethod
    {
        [XmlAttribute]
        public string Algorithm { get; set; } = "http://www.w3.org/2006/12/xml-c14n11";
    }

    public class SignatureMethod
    {
        [XmlAttribute]
        public string Algorithm { get; set; } = "http://www.w3.org/2001/04/xmldsig-more#ecdsa-sha256";
    }

    public class Reference
    {
        [XmlAttribute]
        public string Id { get; set; }

        [XmlAttribute]
        public string Type { get; set; }

        [XmlAttribute]
        public string URI { get; set; }

        [XmlArray(ElementName = "Transforms", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public Transform[] Transforms { get; set; }

        [XmlElement("DigestMethod", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public DigestMethod DigestMethod { get; set; }

        public string DigestValue { get; set; }

    }

    public class Transform
    {
        [XmlAttribute]
        public string Algorithm { get; set; }

        [XmlElement(ElementName = "XPath", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public string XPath { get; set; }
    }

    public class KeyInfo
    {
        public X509Data X509Data { get; set; }

        public KeyInfo(string x509Certificate)
        {
            X509Data = new X509Data { X509Certificate = x509Certificate };
        }

        public KeyInfo() { }
    }

    public class X509Data
    {
        public string X509Certificate { get; set; }
    }

    public class SignedSignatureProperties
    {
        [XmlElement(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
        public string SigningTime { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public DateTime SigningDateTime
        {
            get => DateTime.TryParseExact(SigningTime, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result) ? result : DateTime.MinValue;
            set => SigningTime = value.ToString("yyyy-MM-ddTHH:mm:ss");
        }

        [XmlElement(Namespace = "http://uri.etsi.org/01903/v1.3.2#")]
        public SigningCertificate SigningCertificate { get; set; }
    }

    public class SigningCertificate
    {
        public Cert Cert { get; set; }
    }

    public class Cert
    {
        public CertDigest CertDigest { get; set; }

        public IssuerSerial IssuerSerial { get; set; }
    }

    public class CertDigest
    {
        [XmlElement(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public DigestMethod DigestMethod { get; set; }

        [XmlElement(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public string DigestValue { get; set; }
    }

    public class DigestMethod
    {
        [XmlAttribute]
        public string Algorithm { get; set; }
    }

    public class IssuerSerial
    {
        [XmlElement(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public string X509IssuerName { get; set; }

        [XmlElement(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public string X509SerialNumber { get; set; }
    }
}
