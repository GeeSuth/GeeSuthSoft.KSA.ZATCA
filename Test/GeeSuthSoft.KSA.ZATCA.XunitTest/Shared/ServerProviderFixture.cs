
using Microsoft.Extensions.DependencyInjection;
using GeeSuthSoft.KSA.ZATCA.Services;
using GeeSuthSoft.KSA.ZATCA.Helper;
using GeeSuthSoft.KSA.ZATCA.Enums;
using Microsoft.Extensions.Options;
using System;
using GeeSuthSoft.KSA.ZATCA.Dto;
using GeeSuthSoft.KSA.ZATCA.Extensions;

namespace GeeSuthSoft.KSA.ZATCA.XunitTest.Shared
{
    public class ServiceProviderFixture : IDisposable
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public ServiceProviderFixture()
        {
            var services = new ServiceCollection();

            services.AddZatca(options =>
            {
                options.ZatcaBaseUrl = "https://gw-fatoora.zatca.gov.sa";
                options.Environment = EnvironmentType.NonProduction;
            });

            /* // Configure services
            services.AddHttpClient();
            services.AddLogging();
            services.AddSingleton<IZatcaApiConfig, ZatcaApiConfig>();

            // Configure options
            var zatcaOptions = new ZatcaOptions
            {
                ZatcaBaseUrl = "https://gw-fatoora.zatca.gov.sa",
                Environment = EnvironmentType.NonProduction
            };
            services.AddSingleton(Microsoft.Extensions.Options.Options.Create(zatcaOptions));

            // Register the service to be tested
            services.AddScoped<IZatcaInvoiceService, ZatcaInvoiceService>();
            services.AddScoped<IZatcaOnboardingService, ZatcaOnboardingService>();
 */
            ServiceProvider = services.BuildServiceProvider();
        }

        public void Dispose()
        {
            if (ServiceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
