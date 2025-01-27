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
}
