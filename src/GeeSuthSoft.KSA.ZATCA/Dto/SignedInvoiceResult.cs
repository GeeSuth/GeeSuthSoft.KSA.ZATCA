using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeeSuthSoft.KSA.ZATCA.Dto
{
    public class SignedInvoiceResult
    {
        public string InvoiceHash { get; set; }
        public string Base64SignedInvoice { get; set; }
        public string Base64QrCode { get; set; }
        public string XmlFileName { get; set; }
        public ZatcaRequestApi RequestApi { get; set; }

        public string? QrImageUrl { get; set; }
    }
}
