namespace GeeSuthSoft.KSA.ZATCA;

public class GsCsrGenerationDto
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
}