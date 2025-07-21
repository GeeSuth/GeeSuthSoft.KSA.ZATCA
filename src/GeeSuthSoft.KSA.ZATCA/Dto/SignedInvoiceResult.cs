using GeeSuthSoft.KSA.ZATCA.Xml.RootPaths;

namespace GeeSuthSoft.KSA.ZATCA.Dto
{
    public class SignedInvoiceResult
    {
        public string InvoiceHash { get; set; } = null!;
        public string Base64SignedInvoice { get; set; }= null!;
        public ContentQR Base64QrCodeContent { get; set; }= new ContentQR();
        public string XmlFileName { get; set; }= null!;
        public ZatcaRequestApi RequestApi { get; set; } = null!;

    }

    public class ContentQR
    {
        public string Base64QrCodeContent { get; set; }= null!;
    }

    public class SignedInvoiceRequestDto
    {
        public required Invoice Invoice { get; set; }
        public required string BinaryToken { get; set; }
        public required string Secret { get; set; }
        
    }
}
