using GeeSuthSoft.KSA.ZATCA.Enums;
using GeeSuthSoft.KSA.ZATCA.Generators;
using GeeSuthSoft.KSA.ZATCA.Models;

namespace GeeSuthSoft.KSA.ZATCA.XunitTest.InternalService;

public class GenerateCrsInternalTest
{
     [Fact]
        public void Generate_Crs_Test()
        {

            var onboardingResult = new OnboardingResult();


            var csrGenerationDto = new CsrGeneration
            {
                CommonName = "TST-886431145-399999999900003",
                SerialNumber = "1-TST|2-TST|3-ed22f1d8-e6a2-1118-9b58-d9a8f11e445f",
                OrganizationIdentifier = "399999999900003",
                OrganizationUnitName = "Riyadh Branch",
                OrganizationName = "Maximum Speed Tech Supply LTD",
                CountryName = "SA",
                InvoiceType = "1100",
                LocationAddress = "RRRD2929",
                IndustryBusinessCategory = "Supply activities"
            };

            var csrGenerator = new GeneratorCsr();
            var (generatedCsr, privateKey, errorMessages)
                = csrGenerator.GenerateCsrAndPrivateKey(csrGenerationDto, EnvironmentType.NonProduction);


            onboardingResult.GeneratedCsr = generatedCsr;
            onboardingResult.PrivateKey = privateKey;


            Assert.NotNull(onboardingResult.GeneratedCsr);
            Assert.NotNull(onboardingResult.PrivateKey);

            Assert.NotEmpty(onboardingResult.GeneratedCsr);
            Assert.NotEmpty(onboardingResult.PrivateKey);
        }

}
