using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeeSuthSoft.KSA.ZATCA.Qr
{
    public static class Qr
    {

        /// <summary>
        /// Get QrCode In Image 
        /// </summary>
        /// <param name="SellerName"> Seller Name  </param>
        /// <param name="VatNo"> Vat No </param>
        /// <param name="ReciptDate"> recipt Date </param>
        /// <param name="VatAmountOnly"> total vat amount  </param>
        /// <param name="TotalWithVat"> total amount  </param>
        /// <param name="option"> options to more Customizing Use QrCodeOption object to see what can be changed </param>
        /// <param name="Note"> if you like to add some note </param>
        /// <returns> Image Object </returns>
        public static Image GetImage(string SellerName, string VatNo, DateTime ReciptDate, decimal VatAmountOnly, decimal TotalWithVat, QrCodeOption option = null, string Note = "")
        {
            QrCodeGenerate Generate = new QrCodeGenerate();
            string base64 = TLV.Tlv.GetBase64(SellerName, VatNo, ReciptDate, VatAmountOnly, TotalWithVat);
            return (Bitmap)new ImageConverter()
                .ConvertFrom(
                Convert.FromBase64String(Generate.Generate(base64))
                );
        }



        /// <summary>
        /// Get QrCode  in Base64 string can be opened in browser just past result in url 
        /// </summary>
        /// <param name="SellerName"> Seller Name  </param>
        /// <param name="VatNo"> Vat No </param>
        /// <param name="ReciptDate"> recipt Date </param>
        /// <param name="VatAmountOnly"> total vat amount  </param>
        /// <param name="TotalWithVat"> total amount  </param>
        /// <param name="option"> options to more Customizing Use QrCodeOption object to see what can be changed </param>
        /// <param name="Note"> if you like to add some note </param>
        /// <returns> data:image/png;base64,string </returns>
        public static string GetBase64InUrl(string SellerName, string VatNo, DateTime ReciptDate, decimal VatAmountOnly, decimal TotalWithVat, QrCodeOption option = null, string Note = "")
        {
            QrCodeGenerate Generate = new QrCodeGenerate();
            return ("data:image/png;base64," +
                 Generate.Generate(
                     TLV.Tlv.GetBase64(SellerName, VatNo, ReciptDate, VatAmountOnly, TotalWithVat),option, Note)
                );
        }




        /// <summary>
        /// Get QrCode  in Base64 string to use
        /// </summary>
        /// <param name="SellerName"> Seller Name  </param>
        /// <param name="VatNo"> Vat No </param>
        /// <param name="ReciptDate"> recipt Date </param>
        /// <param name="VatAmountOnly"> total vat amount  </param>
        /// <param name="TotalWithVat"> total amount  </param>
        /// <param name="option"> options to more Customizing Use QrCodeOption object to see what can be changed </param>
        /// <param name="Note"> if you like to add some note </param>
        /// <returns> QrCode In base64 </returns>
        public static string GetBase64(string SellerName, string VatNo, DateTime ReciptDate, decimal VatAmountOnly, decimal TotalWithVat, QrCodeOption option = null, string Note = "")
        {
            QrCodeGenerate Generate = new QrCodeGenerate();

            return Generate.Generate(
                TLV.Tlv.GetBase64(SellerName, VatNo, ReciptDate, VatAmountOnly, TotalWithVat)
                 ,option,Note);
        }


    }
}
