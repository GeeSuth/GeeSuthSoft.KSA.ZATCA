namespace GeeSuthSoft.KSA.ZATCA.Models
{
    internal class SignedInvoiceResult
    {
        public string InvoiceHash { get; set; } = null!;
        public string Base64SignedInvoice { get; set; }= null!;
        public ContentQR Base64QrCodeContent { get; set; }= new ContentQR();
        public string XmlFileName { get; set; }= null!;
        public ZatcaRequestApi RequestApi { get; set; } = null!;

    }

    internal class ContentQR
    {
        public string Base64QrCodeContent { get; set; }= null!;
    }
}
