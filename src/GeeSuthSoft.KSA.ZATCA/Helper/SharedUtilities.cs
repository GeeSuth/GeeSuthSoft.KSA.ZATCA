using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml.Xsl;
using System.Xml;
using System.Security.Cryptography;
using Org.BouncyCastle.OpenSsl;
using System.Numerics;
using System.Security.Cryptography.Xml;
using Org.BouncyCastle.Crypto.Parameters;

namespace GeeSuthSoft.KSA.ZATCA.Helper
{
    internal static class SharedUtilities
    {
        internal static string ObjectToXml(this object obj, XmlSerializerNamespaces namespaces, string filePath = null)
        {
            XmlSerializer serializer = new(obj.GetType());

            using MemoryStream memoryStream = new();
            XmlWriterSettings settings = new()
            {
                Indent = true,
                IndentChars = "    ",
                OmitXmlDeclaration = false,
                Encoding = Encoding.UTF8,
            };

            using (XmlWriter xmlWriter = XmlWriter.Create(memoryStream, settings))
            {
                serializer.Serialize(xmlWriter, obj, namespaces);
                xmlWriter.Flush();
            }

            memoryStream.Position = 0;
            string xmlString = new StreamReader(memoryStream).ReadToEnd().Trim();

            if (!string.IsNullOrEmpty(filePath))
            {
                File.WriteAllText(filePath, xmlString);
            }

            return xmlString;
        }

        internal static string MoveLastAttributeToFirst(this string xmlString)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            var root = xmlDoc.DocumentElement;
            var lastAttribute = root.Attributes[root.Attributes.Count - 1];

            root.Attributes.Remove(lastAttribute);
            root.Attributes.InsertBefore(lastAttribute, root.Attributes[0]);

            var sb = new StringBuilder();
            var settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "    ",
                OmitXmlDeclaration = false,
                Encoding = Encoding.UTF8
            };

            using (var xmlWriter = XmlWriter.Create(sb, settings))
            {
                xmlDoc.WriteTo(xmlWriter);
            }

            return sb.ToString();
        }

        internal static string ApplyXSLT(this string xml, string xsltFileContent, bool indent)
        {
            StringBuilder output = new();
            XmlWriterSettings settings = new()
            {
                OmitXmlDeclaration = true,
                Encoding = Encoding.UTF8,
                ConformanceLevel = ConformanceLevel.Auto,
                Indent = indent
            };
            using (XmlWriter writer = XmlWriter.Create(output, settings))
            {
                XmlReader stylesheet = XmlReader.Create(new StringReader(xsltFileContent));
                XmlReader input = XmlReader.Create(new StringReader(xml));
                input.Read();
                XslCompiledTransform transform1 = new();
                transform1.Load(stylesheet);
                transform1.Transform(input, writer);
            }
            return output.ToString();
        }

        internal static string ReadResource(string fileName)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string resourceName = assembly.GetManifestResourceNames()
                .SingleOrDefault(str => str.Contains(fileName));

            if (string.IsNullOrEmpty(resourceName))
            {
                return null;
            }

            using Stream stream = assembly.GetManifestResourceStream(resourceName);
            if (stream != null)
            {
                using StreamReader reader = new(stream);
                return reader.ReadToEnd();
            }
            else
            {
                return null;
            }
        }

        internal static string GetBase64InvoiceHash(string eInvoiceXml)
        {
            using MemoryStream stream = new(Encoding.UTF8.GetBytes(eInvoiceXml));
            XmlDsigC14NTransform transform1 = new(false);
            transform1.LoadInput(stream);
            MemoryStream output = transform1.GetOutput() as MemoryStream;
            byte[] hashBytes = HashSha256(Encoding.UTF8.GetString(output.ToArray()));
            return Convert.ToBase64String(hashBytes);
        }

        internal static byte[] HashSha256(string rawData)
        {
            return SHA256.HashData(Encoding.UTF8.GetBytes(rawData));
        }

        //https://zatca1.discourse.group/t/signed-and-reported-b2c-xmls-not-validated-with-web-based-validator/1376/3?u=ecloud
        internal static string GetDigitalSignature1(string xmlHashing, string privateKeyContent)
        {
            byte[] buffer;
            sbyte[] numArray = (from x in Convert.FromBase64String(xmlHashing) select (sbyte)x).ToArray();
            privateKeyContent = privateKeyContent.Replace("\n", "").Replace("\t", "");
            if (!privateKeyContent.Contains("-----BEGIN EC PRIVATE KEY-----") && !privateKeyContent.Contains("-----END EC PRIVATE KEY-----"))
            {
                privateKeyContent = "-----BEGIN EC PRIVATE KEY-----\n" + privateKeyContent + "\n-----END EC PRIVATE KEY-----\n";
            }
            using (TextReader reader = new StringReader(privateKeyContent))
            {
                AsymmetricKeyParameter @private = ((AsymmetricCipherKeyPair)new PemReader(reader).ReadObject()).Private;
                ISigner signer = SignerUtilities.GetSigner("SHA-256withECDSA");
                signer.Init(true, @private);
                signer.BlockUpdate((byte[])(Array)numArray, 0, numArray.Length);
                buffer = signer.GenerateSignature();
            }
            return Convert.ToBase64String(buffer);
        }


        internal static byte[] DecodeFromBase64(string base64EncodedText) => !string.IsNullOrEmpty(base64EncodedText) ? Convert.FromBase64String(base64EncodedText) : (byte[])null;


        internal static string GetDigitalSignature(string xmlHashing, string privateKeyContent)
        {
            // Decode XML hash from Base64
            byte[] hashBytes = SharedUtilities.DecodeFromBase64(xmlHashing);

            // Ensure private key is in PEM format
            const string ecPrivateKeyHeader = "-----BEGIN EC PRIVATE KEY-----";
            const string ecPrivateKeyFooter = "-----END EC PRIVATE KEY-----";

            // If key is missing the header and footer, add them
            if (!privateKeyContent.Contains(ecPrivateKeyHeader) && !privateKeyContent.Contains(ecPrivateKeyFooter))
            {
                privateKeyContent = $"{ecPrivateKeyHeader}\n{privateKeyContent.Trim()}\n{ecPrivateKeyFooter}";
            }

            byte[] signature;

            try
            {
                // Read private key and generate signature
                using (var reader = new StringReader(privateKeyContent))
                {
                    var pemReader = new PemReader(reader);
                    var keyPair = (AsymmetricCipherKeyPair)pemReader.ReadObject();

                    // Ensure the key is an EC private key
                    if (keyPair.Private is not ECPrivateKeyParameters)
                        throw new ArgumentException("The provided key is not an EC private key.");

                    var signer = SignerUtilities.GetSigner("SHA-256withECDSA");
                    signer.Init(true, keyPair.Private);  // Initialize signer with the private key
                    signer.BlockUpdate(hashBytes, 0, hashBytes.Length);

                    signature = signer.GenerateSignature();
                }
            }
            catch (PemException pemEx)
            {
                throw new Exception("Failed to parse private key: " + pemEx.Message, pemEx);
            }

            // Return the generated signature as a Base64 string
            return Convert.ToBase64String(signature);
        }



        internal static string GetSerialNumberForCertificateObject(X509Certificate2 x509Certificate2)
        {
            sbyte[] numArray = (from x in x509Certificate2.GetSerialNumber() select (sbyte)x).ToArray();
            BigInteger integer = new((byte[])(Array)numArray);
            return integer.ToString();
        }

        internal static string HashSha256AsString(string rawData)
        {
            StringBuilder builder = new();
            foreach (byte num2 in HashSha256(rawData))
            {
                builder.Append(num2.ToString("x2"));
            }
            return builder.ToString();
        }

        internal static string ToFormattedXml(this string xml, bool omitXmlDeclaration = false)
        {
            XmlDocument doc = new();
            doc.LoadXml(xml);

            using MemoryStream memoryStream = new();
            XmlWriterSettings settings = new()
            {
                Indent = true,
                IndentChars = "    ",
                OmitXmlDeclaration = omitXmlDeclaration,
                Encoding = new UTF8Encoding(false)
            };

            using (XmlWriter writer = XmlWriter.Create(memoryStream, settings))
            {
                doc.WriteTo(writer);
            }

            return Encoding.UTF8.GetString(memoryStream.ToArray());
        }

        internal static string GetSignedPropertiesHash(string signingTime, string digestValue, string x509IssuerName, string x509SerialNumber)
        {
            string xmlString = $@"<xades:SignedProperties xmlns:xades=""http://uri.etsi.org/01903/v1.3.2#"" Id=""xadesSignedProperties"">
                                    <xades:SignedSignatureProperties>
                                        <xades:SigningTime>{signingTime}</xades:SigningTime>
                                        <xades:SigningCertificate>
                                            <xades:Cert>
                                                <xades:CertDigest>
                                                    <ds:DigestMethod xmlns:ds=""http://www.w3.org/2000/09/xmldsig#"" Algorithm=""http://www.w3.org/2001/04/xmlenc#sha256""/>
                                                    <ds:DigestValue xmlns:ds=""http://www.w3.org/2000/09/xmldsig#"">{digestValue}</ds:DigestValue>
                                                </xades:CertDigest>
                                                <xades:IssuerSerial>
                                                    <ds:X509IssuerName xmlns:ds=""http://www.w3.org/2000/09/xmldsig#"">{x509IssuerName}</ds:X509IssuerName>
                                                    <ds:X509SerialNumber xmlns:ds=""http://www.w3.org/2000/09/xmldsig#"">{x509SerialNumber}</ds:X509SerialNumber>
                                                </xades:IssuerSerial>
                                            </xades:Cert>
                                        </xades:SigningCertificate>
                                    </xades:SignedSignatureProperties>
                                </xades:SignedProperties>".Replace("\r\n", "\n");  // Normalize line endings to LF only

            byte[] hashBytes = SHA256.HashData(Encoding.UTF8.GetBytes(xmlString.Trim()));
            string hashHex = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(hashHex));
        }

    }

}
