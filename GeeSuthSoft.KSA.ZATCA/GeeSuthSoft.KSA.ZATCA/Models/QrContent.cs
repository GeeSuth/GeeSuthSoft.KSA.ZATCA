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
                         decimal TotalAmount,
                         decimal TotalTaxAmount)
        {
            this.SellerName = new Tag(1, SellerName);
            this.VatNo = new Tag(2, VatNo);
            this.Timespan = new Tag(3, TimeRecipt.ToString("yyyy-MM-dd hh:mm:ss"));
            this.TotalInvoice = new Tag(4, TotalAmount.ToString());
            this.TotalTaxAmount = new Tag(5, TotalTaxAmount.ToString());

        }
        public Tag SellerName { get; set; }
        public Tag VatNo { get; set; }
        public Tag Timespan { get; set; }
        public Tag TotalInvoice { get; set; }
        public Tag TotalTaxAmount { get; set; }

    }
}
