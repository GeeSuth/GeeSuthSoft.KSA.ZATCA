using GeeSuthSoft.KSA.ZATCA.Enums;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Pkcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeeSuthSoft.KSA.ZATCA.Models;

namespace GeeSuthSoft.KSA.ZATCA.Helper
{
    internal class X509CertificateGenerator
    {
        internal string CreateCertificate(CsrGeneration dto, AsymmetricCipherKeyPair ecKeyPair, bool isPemFormat, EnvironmentType environment)
        {
            X509Name subject = CreateCertificateSubjectName(dto);
            X509Name san = CreateCertificateOtherAttributes(dto);
            DerSet attributes = X509ExtensionsGenerator(san, environment);
            Pkcs10CertificationRequest request = new Pkcs10CertificationRequest("SHA256withECDSA", subject, ecKeyPair.Public, attributes, ecKeyPair.Private);

            StringBuilder builder = new StringBuilder();
            PemWriter writer = new PemWriter(new StringWriter(builder));
            writer.WriteObject(request);
            writer.Writer.Flush();

            string rawData = builder.ToString();
            string base64EncodedCsr = Convert.ToBase64String(Encoding.ASCII.GetBytes(rawData));

            return isPemFormat ? rawData : base64EncodedCsr;
        }


        private X509Name CreateCertificateSubjectName(CsrGeneration dto)
        {
            var list1 = new List<DerObjectIdentifier> { X509Name.C, X509Name.OU, X509Name.O, X509Name.CN };
            var list2 = new List<string> { dto.CountryName, dto.OrganizationUnitName, dto.OrganizationName, dto.CommonName };
            return new X509Name(list1.ToArray(), list2.ToArray());
        }

        private X509Name CreateCertificateOtherAttributes(CsrGeneration dto)
        {
            var list1 = new List<DerObjectIdentifier> { X509Name.Surname, X509Name.UID, X509Name.T, new DerObjectIdentifier("2.5.4.26"), X509Name.BusinessCategory };
            var list2 = new List<string> { dto.SerialNumber, dto.OrganizationIdentifier, dto.InvoiceType, dto.LocationAddress, dto.IndustryBusinessCategory };
            return new X509Name(list1.ToArray(), list2.ToArray());
        }

        private string GetCertificateTemplateName(EnvironmentType environment)
        {
            return environment switch
            {
                EnvironmentType.Production => "ZATCA-Code-Signing",
                EnvironmentType.Simulation => "PREZATCA-Code-Signing",
                EnvironmentType.NonProduction => "TSTZATCA-Code-Signing",
                _ => "ZATCA-Code-Signing",
            };
        }
        private DerSet X509ExtensionsGenerator(X509Name san, EnvironmentType environment)
        {
            string certificateTemplateName = GetCertificateTemplateName(environment);
            var dictionary1 = new Dictionary<DerObjectIdentifier, X509Extension>
        {
            { new DerObjectIdentifier("1.3.6.1.4.1.311.20.2"), new X509Extension(false, new DerOctetString(new DerPrintableString(certificateTemplateName))) },
            { X509Extensions.SubjectAlternativeName, new X509Extension(false, new DerOctetString(new DerSequence(new DerTaggedObject(4, san)))) }
        };
            var element = new X509Extensions(dictionary1);
            return new DerSet(new AttributePkcs(PkcsObjectIdentifiers.Pkcs9AtExtensionRequest, new DerSet(element)));
        }
    }
}
