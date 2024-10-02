using Microsoft.Extensions.DependencyInjection;
using Xunit;
using GeeSuthSoft.KSA.ZATCA.Services;
using GeeSuthSoft.KSA.ZATCA.Helper;
using GeeSuthSoft.KSA.ZATCA.Dto;
using GeeSuthSoft.KSA.ZATCA.Enums;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using GeeSuthSoft.KSA.ZATCA.XunitTest.Shared;

namespace GeeSuthSoft.KSA.ZATCA.XunitTest;
public class OnboardingDeviceDiTest : IClassFixture<ServiceProviderFixture>
{
    private readonly IServiceProvider _serviceProvider;

    public OnboardingDeviceDiTest()
    {
        var services = new ServiceCollection();

        // Configure services
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

        _serviceProvider = services.BuildServiceProvider();
    }


}


