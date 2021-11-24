using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeeSuthSoft.KSA.ZATCA.Qr
{
    public static class QrHelper
    {
        //Use TLV
        public static string GenerateTLV(Models.QrContent content)
        {
            var data = content.SellerName.ToString() +
                   content.VatNo.ToString() +
                   content.Timespan.ToString() +
                   content.TotalInvoice.ToString() +
                   content.TotalTaxAmount.ToString();
            return data;
        }
    }
}
