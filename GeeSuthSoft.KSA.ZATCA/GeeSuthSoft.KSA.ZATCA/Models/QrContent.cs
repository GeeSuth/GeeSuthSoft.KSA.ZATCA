using GeeSuthSoft.KSA.ZATCA.TLV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeeSuthSoft.KSA.ZATCA.Models
{

    public class QrContent
    {
        public QrContent(string SellerName,
                         string VatNo,
                         DateTime TimeRecipt,
                         decimal TotalInvoiceWithVat,
                         decimal TotalVatAmount)
        {
            this.SellerName = new Tag(1, Encoding.UTF8.GetBytes(SellerName));
            this.VatNo = new Tag(2, Encoding.Default.GetBytes(VatNo));
            this.Timespan = new Tag(3, Encoding.Default.GetBytes(TimeRecipt.ToString()));
            this.TotalInvoiceWithVat = new Tag(4, Encoding.Default.GetBytes(TotalInvoiceWithVat.ToString()));
            this.TotalVatAmount = new Tag(5, Encoding.Default.GetBytes(TotalVatAmount.ToString()));

        }
        public Tag SellerName { get; set; }
        public Tag VatNo { get; set; }
        public Tag Timespan { get; set; }
        public Tag TotalInvoiceWithVat { get; set; }
        public Tag TotalVatAmount { get; set; }

    }
}
