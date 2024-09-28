using GeeSuthSoft.KSA.ZATCA.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeeSuthSoft.KSA.ZATCA.XunitTest.ConstValue
{
    public static class CompanyTemplateTest
    {
        public static CsrGenerationDto CrsCompanyInfo()
        {
            return new CsrGenerationDto
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
        }
    }
}
