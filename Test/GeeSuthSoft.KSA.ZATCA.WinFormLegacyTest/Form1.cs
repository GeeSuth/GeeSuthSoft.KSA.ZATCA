using System;
using System.Windows.Forms;
using GeeSuthSoft.KSA.ZATCA.Dto;
using GeeSuthSoft.KSA.ZATCA.Services;
using GeeSuthSoft.KSA.ZATCA.Helper;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace GeeSuthSoft.KSA.ZATCA.WinFormLegacyTest
{
    public partial class Form1 : Form
    {
        private readonly IZatcaOnboardingService _zatcaOnboardingService;

        public Form1()
        {
            InitializeComponent();

            // Manual instantiation for testing purposes in WinForms
            var options = new ZatcaOptions
            {
                LogRequestAndResponse = true
            };

            var config = new ZatcaApiConfig(Options.Create(options));

            // Using NullLogger and a simple HttpClientFactory mock for manual setup
            _zatcaOnboardingService = new ZatcaOnboardingService(
                config,
                new SimpleHttpClientFactory(),
                NullLogger<ZatcaOnboardingService>.Instance
            );
        }

        private void btnGenerateCsr_Click(object sender, EventArgs e)
        {
            try
            {
                var csrDto = new CsrGenerationDto
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

                var result = _zatcaOnboardingService.GenerateCsr(csrDto, true);
                txtOutput.Text = $"CSR Generated Successfully:\r\n\r\n{result.Csr}\r\n\r\nPrivate Key:\r\n{result.PrivateKey}";
            }
            catch (Exception ex)
            {
                txtOutput.Text = $"Error: {ex.Message}\r\n{ex.StackTrace}";
            }
        }
    }

    // Simple mock for IHttpClientFactory
    public class SimpleHttpClientFactory : IHttpClientFactory
    {
        public HttpClient CreateClient(string name)
        {
            return new HttpClient();
        }
    }
}
