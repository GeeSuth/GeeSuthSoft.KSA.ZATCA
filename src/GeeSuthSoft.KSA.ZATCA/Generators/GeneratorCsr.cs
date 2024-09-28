using GeeSuthSoft.KSA.ZATCA.Dto;
using GeeSuthSoft.KSA.ZATCA.Enums;
using GeeSuthSoft.KSA.ZATCA.Helper;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("GeeSuthSoft.KSA.ZATCA.XunitTest")]
namespace GeeSuthSoft.KSA.ZATCA.Generators
{
    internal class GeneratorCsr
    {

        private readonly X509CertificateGenerator x509CertificateGenerator = new X509CertificateGenerator();


        public (string csr, string privateKey, List<string> errorMessages) GenerateCsrAndPrivateKey(
            CsrGenerationDto csrGenerationDto, 
            EnvironmentType environment, 
            bool pemFormat = false)
        {

            List<string> errorMessages = new List<string>();

            if (!csrGenerationDto.IsValid(out errorMessages))
            {
                throw new Exception("CSR configuration is not valid. Errors: " + string.Join(", ", errorMessages));
            }

            AsymmetricCipherKeyPair keyPair = GenerateKeyPair();
            string csr = GenerateCertificate(csrGenerationDto, keyPair, environment, pemFormat);

            string privateKey = GeneratePrivateKey(keyPair, pemFormat);

            return (csr, privateKey, errorMessages);
        }


        private string GenerateCertificate(CsrGenerationDto dto, AsymmetricCipherKeyPair keyPair, EnvironmentType environment, bool pemFormat = false)
        {
            try
            {
                return x509CertificateGenerator.CreateCertificate(dto, keyPair, pemFormat, environment);
            }
            catch (Exception ex)
            {
                throw new Exception("Error generating CSR String", ex);
            }
        }

        private AsymmetricCipherKeyPair GenerateKeyPair()
        {
            ECKeyGenerationParameters parameters = new ECKeyGenerationParameters(SecObjectIdentifiers.SecP256k1, new SecureRandom());
            ECKeyPairGenerator generator = new ECKeyPairGenerator();
            generator.Init(parameters);
            return generator.GenerateKeyPair();
        }

        private string GeneratePrivateKey(AsymmetricCipherKeyPair keyPair, bool pemFormat)
        {
            try
            {
                return GetPrivateKey(keyPair, pemFormat);
            }
            catch (Exception ex)
            {
                throw new Exception("Error generating EC Private Key String", ex);
            }
        }

        private string GetPrivateKey(AsymmetricCipherKeyPair keys, bool pemFormat)
        {
            StringWriter stringWriter = new StringWriter();
            PemWriter pemWriter = new PemWriter(stringWriter);
            pemWriter.WriteObject(keys.Private);
            pemWriter.Writer.Flush();

            string privateKeyString = stringWriter.ToString();

            if (!pemFormat)
            {
                privateKeyString = privateKeyString
                    .Replace("-----BEGIN EC PRIVATE KEY-----", "")
                    .Replace("\n", "")
                    .Replace("\r", "")
                    .Replace("-----END EC PRIVATE KEY-----", "");
            }

            return privateKeyString;
        }

    }
}
