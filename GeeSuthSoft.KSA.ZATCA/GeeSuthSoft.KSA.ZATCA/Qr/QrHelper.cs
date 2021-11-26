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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns> String TVL Data with base64 </returns>
        public static string GenerateTLV(Models.QrContent content)
        {
            string ContentTlv = string.Join("", 
                content.SellerName.ToString(), 
                content.VatNo.ToString(), 
                content.Timespan.ToString(),
                content.TotalInvoice.ToString(),
                content.TotalTaxAmount.ToString());
            
            var data = Encoding.UTF8.GetBytes(ContentTlv);
            return Convert.ToBase64String(data);
        }
    }
}
