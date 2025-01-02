using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GeeSuthSoft.KSA.ZATCA.Enums
{
    public enum InvoiceType
    {
        TaxInvoice = 388,
        TaxInvoiceDebitNote = 383,
        TaxInvoiceCreditNote = 381,
        PrepaymentTaxInvoice = 386
    }

    public enum BooleanEnum
    {
        [EnumMember(Value = "true")]
        True,
        [EnumMember(Value = "false")]
        False
    }

    public enum PartyIdentificationEnum
    {
        CRN, MOM, MLS, Number700, SAG, OTH
    }

    public enum EnvironmentType
    {
        NonProduction,
        Simulation,
        Production
    }

    public abstract class ZatcaReportingStatus
    {
        public const string REPORTED = "REPORTED";
    }

    public abstract class ZatcaValidationResults
    {
        public const string PASS = "PASS";
    }
}
