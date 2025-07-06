using GeeSuthSoft.KSA.ZATCA.Enums;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeeSuthSoft.KSA.ZATCA.Models;

namespace GeeSuthSoft.KSA.ZATCA.Helper
{
    public interface IZatcaApiConfig{
        string GetUrl(string endpoint);

        public string ReportingUrl {get;}
        public string ClearanceUrl {get;}
        public string ComplianceCSIDUrl {get;}
        public string ComplianceCheckUrl {get;}
        public string ProductionCSIDUrl {get;}
        
        public EnvironmentType Environment {get;}
        
        public bool LogsEnabled { get; }
    }
    public class ZatcaApiConfig : IZatcaApiConfig
    {
        private readonly IOptions<ZatcaOptions> _zatcaOptions;

        public ZatcaApiConfig(IOptions<ZatcaOptions> zatcaOptions)
        {
            _zatcaOptions = zatcaOptions;
        }
        public string ReportingUrl => GetUrl("invoices/reporting/single");
        public string ClearanceUrl => GetUrl("invoices/clearance/single");
        public string ComplianceCSIDUrl => GetUrl("compliance");
        public string ComplianceCheckUrl => GetUrl("compliance/invoices");
        public string ProductionCSIDUrl => GetUrl("production/csids");
        public EnvironmentType Environment => _zatcaOptions.Value.Environment;
 
        public string GetUrl(string endpoint)
        {
            string environment = _zatcaOptions.Value.Environment switch
            {
                EnvironmentType.NonProduction => "developer-portal",
                EnvironmentType.Simulation => "simulation",
                EnvironmentType.Production => "core",
                _ => "developer-portal"
            };

            //return $"https://gw-fatoora.zatca.gov.sa/e-invoicing/{environment}/{endpoint}";
            return $"{_zatcaOptions.Value.ZatcaBaseUrl ?? "https://gw-fatoora.zatca.gov.sa"}/e-invoicing/{environment}/{endpoint}";
        }
        
        public bool LogsEnabled => _zatcaOptions.Value.LogRequestAndResponse;
    }
}
