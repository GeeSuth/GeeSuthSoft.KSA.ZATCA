using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GeeSuthSoft.KSA.ZATCA.Dto
{
    public class CsrGenerationDto
    {
        public string CommonName { get; set; } //= "TST-886431145-399999999900003";
        public string SerialNumber { get; set; } //= "1-TST|2-TST|3-ed22f1d8-e6a2-1118-9b58-d9a8f11e445f";
        public string OrganizationIdentifier { get; set; } //= "399999999900003";
        public string OrganizationUnitName { get; set; } //= "Riyadh Branch";
        public string OrganizationName { get; set; } //= "Maximum Speed Tech Supply LTD";
        public string CountryName { get; set; } = "SA";
        public string InvoiceType { get; set; } = "1100";
        public string LocationAddress { get; set; } // = "RRRD2929";
        public string IndustryBusinessCategory { get; set; } //= "Supply activities";

        public bool IsValid(out List<string> errors)
        {
            errors = new List<string>();

            if (string.IsNullOrEmpty(CommonName))
            {
                errors.Add("Common name is mandatory field");
            }
            if (string.IsNullOrEmpty(CountryName))
            {
                errors.Add("Country name is a mandatory field.");
            }
            if (CountryName.Length > 3 || CountryName.Length < 2)
            {
                errors.Add("Invalid country code name, please provide a valid country code name");
            }
            if (string.IsNullOrEmpty(IndustryBusinessCategory))
            {
                errors.Add("Industry is a mandatory field");
            }
            if (string.IsNullOrEmpty(OrganizationUnitName))
            {
                errors.Add("Organization unit name is a mandatory field");
            }
            if (string.IsNullOrEmpty(OrganizationIdentifier))
            {
                errors.Add("Organization identifier is a mandatory field");
            }
            else
            {
                if (OrganizationIdentifier.Substring(10, 1) == "1" && OrganizationUnitName.Length != 10)
                {
                    errors.Add("Organization Unit Name must be the 10-digit TIN number of the individual group member whose device is being onboarded");
                }
                if (OrganizationIdentifier.Length != 15)
                {
                    errors.Add("Invalid organization identifier, please provide a valid 15 digit of your vat number");
                }
                if (OrganizationIdentifier[0] != '3')
                {
                    errors.Add("Invalid organization identifier, organization identifier should start with digit 3");
                }
                if (OrganizationIdentifier[^1] != '3')
                {
                    errors.Add("Invalid organization identifier, organization identifier should end with digit 3");
                }
            }
            if (string.IsNullOrEmpty(InvoiceType))
            {
                errors.Add("Invoice type is a mandatory field");
            }
            if (InvoiceType.Length != 4 || !Regex.IsMatch(InvoiceType, "^[0-1]{4}$"))
            {
                errors.Add("Invalid invoice type, please provide a valid invoice type");
            }
            if (string.IsNullOrEmpty(LocationAddress))
            {
                errors.Add("Location is a mandatory field");
            }
            if (string.IsNullOrEmpty(OrganizationName))
            {
                errors.Add("Organization name is a mandatory field");
            }
            if (string.IsNullOrEmpty(SerialNumber))
            {
                errors.Add("Serial number is a mandatory field.");
            }
            if (!Regex.IsMatch(SerialNumber, @"1-(.+)\|2-(.+)\|3-(.+)"))
            {
                errors.Add("Invalid serial number. Serial number should be in the format: 1-...|2-...|3-....");
            }

            return errors.Count == 0;
        }
    }
}
