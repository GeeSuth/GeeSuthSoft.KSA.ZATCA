using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeeSuthSoft.KSA.ZATCA.Tags;
using GeeSuthSoft.KSA.ZATCA.Ext;

namespace GeeSuthSoft.KSA.ZATCA.TLV
{
    public static class Tlv
    {

        /// <summary>
        /// 
        /// </summary>
        /// <returns> return HEX Ex: 010453414C4D020D3030... </returns>
        private static string ToTLV(string SellerName,string VatNo,DateTime ReciptDate,decimal VatAmountOnly, decimal TotalWithVat)
        {
            UseTag Tag1 = new UseTag(1, SellerName);
            UseTag Tag2 = new UseTag(2, VatNo);
            UseTag Tag3 = new UseTag(3, ReciptDate.ToString("yyyy-MM-ddTHH:mm:ssZ"));
            UseTag Tag4 = new UseTag(4, TotalWithVat.ToString());
            UseTag Tag5 = new UseTag(5, VatAmountOnly.ToString());
            

            return $"{Tag1.ToString()}{Tag2.ToString()}{Tag3.ToString()}{Tag4.ToString()}{Tag5.ToString()}";

            
        }


        /// <returns> Ex: AQRTQUxNAg0wMDAwMDAwMDAwMDA5AxQyMDIyLTAxLTAxVDAwO..... </returns>
        public static string GetBase64(string SellerName, string VatNo, DateTime ReciptDate, decimal VatAmountOnly, decimal TotalWithVat)
        {
            var tlv = ToTLV(SellerName, VatNo, ReciptDate, VatAmountOnly, TotalWithVat);
            var hex = tlv.HexStringToHex();
            return Convert.ToBase64String(hex);
            
        }

       
    }
}
