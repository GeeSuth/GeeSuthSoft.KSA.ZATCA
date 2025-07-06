using GeeSuthSoft.KSA.ZATCA.Enums;
using GeeSuthSoft.KSA.ZATCA.Helper;
using GeeSuthSoft.KSA.ZATCA.Models;
using GeeSuthSoft.KSA.ZATCA.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GeeSuthSoft.KSA.ZATCA.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddZatca(this IServiceCollection services,
    Action<ZatcaOptions> configureOptions)
    {
        if(services.All(w => w.ServiceType != typeof(IHttpClientFactory)))
        {
            services.AddHttpClient();
        }

        if (services.All(x => x.ServiceType != typeof(ILoggerFactory)))
        {
            services.AddLogging();
        }


        services.Configure<ZatcaOptions>(configureOptions);
        services.AddSingleton<IZatcaApiConfig, ZatcaApiConfig>();
        


        services.AddTransient<IZatcaInvoiceService, ZatcaInvoiceService>();
        services.AddTransient<IZatcaOnboardingService, ZatcaOnboardingService>();
        services.AddTransient<IZatcaShareService, ZatcaShareService>();
        return services;

    }
}

