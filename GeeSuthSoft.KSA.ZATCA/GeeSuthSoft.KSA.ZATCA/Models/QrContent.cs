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
            this.SellerName = new Tag(1, Encoding.UTF8.GetBytes(SellerName));
            this.VatNo = new Tag(2, Encoding.UTF8.GetBytes(VatNo));
            this.Timespan = new Tag(3, Encoding.UTF8.GetBytes(TimeRecipt.ToString()));
            this.TotalInvoice = new Tag(4, Encoding.UTF8.GetBytes(TotalAmount.ToString()));
            this.TotalTaxAmount = new Tag(5, Encoding.UTF8.GetBytes(TotalTaxAmount.ToString()));

        }
        public Tag SellerName { get; set; }
        public Tag VatNo { get; set; }
        public Tag Timespan { get; set; }
        public Tag TotalInvoice { get; set; }
        public Tag TotalTaxAmount { get; set; }

    }
}
