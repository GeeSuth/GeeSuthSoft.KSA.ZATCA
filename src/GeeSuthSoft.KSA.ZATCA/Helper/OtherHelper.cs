using GeeSuthSoft.KSA.ZATCA.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeeSuthSoft.KSA.ZATCA.Helper
{
    public class OtherHelper
    {
        public string ReportingUrl => GetUrl(EnvironmentType.NonProduction,"invoices/reporting/single");
        public string ClearanceUrl => GetUrl(EnvironmentType.NonProduction, "invoices/clearance/single");
        public string ComplianceCSIDUrl => GetUrl(EnvironmentType.NonProduction, "compliance");
        public string ComplianceCheckUrl => GetUrl(EnvironmentType.NonProduction, "compliance/invoices");
        public string ProductionCSIDUrl => GetUrl(EnvironmentType.NonProduction, "production/csids");

        public string GetUrl(EnvironmentType EnvironmentType, string endpoint)
        {
            string environment = EnvironmentType switch
            {
                EnvironmentType.NonProduction => "developer-portal",
                EnvironmentType.Simulation => "simulation",
                EnvironmentType.Production => "core",
                _ => "developer-portal"
            };

            return $"https://gw-fatoora.zatca.gov.sa/e-invoicing/{environment}/{endpoint}";
        }
    }
}
