using GeeSuthSoft.KSA.ZATCA.QR;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GeeSuthSoft.KSA.ZATCA
{
    [Obsolete("This class is obsolete and will be removed in a future version.")]
    public static class Qr
    {

        /// <summary>
        /// Get QrCode In Image (Windows only).
        /// </summary>
        /// <param name="SellerName"> Seller Name  </param>
        /// <param name="VatNo"> Vat No </param>
        /// <param name="ReciptDate"> recipt Date </param>
        /// <param name="VatAmountOnly"> total vat amount  </param>
        /// <param name="TotalWithVat"> total amount  </param>
        /// <param name="option"> options to more Customizing Use QrCodeOption object to see what can be changed </param>
        /// <param name="Note"> if you like to add some note </param>
        /// <returns> Image Object </returns>
        /// <remarks>
        /// This returns a System.Drawing.Bitmap (GDI+) and is only supported on Windows.
        /// Use <see cref="GetPngBytes"/> or <see cref="GetPngStream"/> for cross-platform code.
        /// </remarks>
        [Obsolete("GetImage returns a System.Drawing.Bitmap and is only supported on Windows. Use GetPngBytes or GetPngStream instead.")]
        public static Bitmap GetImage(string SellerName, string VatNo, DateTime ReciptDate, decimal VatAmountOnly, decimal TotalWithVat, QrCodeOption option = null, string Note = "")
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                throw new PlatformNotSupportedException(
                    "Qr.GetImage returns a System.Drawing.Bitmap (GDI+), which is only supported on Windows. " +
                    "Use Qr.GetPngBytes or Qr.GetPngStream for cross-platform PNG output.");
            }

            QrCodeGenerate Generate = new QrCodeGenerate();
            string base64 = TLV.Tlv.GetBase64(SellerName, VatNo, ReciptDate, VatAmountOnly, TotalWithVat);
            byte[] imageBytes = Convert.FromBase64String(Generate.Generate(base64));

            using (var ms = new MemoryStream(imageBytes))
            {
#pragma warning disable CA1416 // Guarded by the Windows runtime check above.
                return new Bitmap(ms) ?? new Bitmap(1, 1); // Return a 1x1 bitmap if decoding fails
#pragma warning restore CA1416
            }
        }

        /// <summary>
        /// Get QrCode as PNG bytes. Cross-platform (no System.Drawing/GDI+).
        /// </summary>
        /// <param name="SellerName"> Seller Name  </param>
        /// <param name="VatNo"> Vat No </param>
        /// <param name="ReciptDate"> recipt Date </param>
        /// <param name="VatAmountOnly"> total vat amount  </param>
        /// <param name="TotalWithVat"> total amount  </param>
        /// <param name="option"> options to more Customizing Use QrCodeOption object to see what can be changed </param>
        /// <param name="Note"> if you like to add some note </param>
        /// <returns> PNG image bytes </returns>
        public static byte[] GetPngBytes(string SellerName, string VatNo, DateTime ReciptDate, decimal VatAmountOnly, decimal TotalWithVat, QrCodeOption? option = null, string Note = "")
        {
            return Convert.FromBase64String(
                GetBase64(SellerName, VatNo, ReciptDate, VatAmountOnly, TotalWithVat, option, Note));
        }

        /// <summary>
        /// Get QrCode as a read-only PNG stream. Cross-platform (no System.Drawing/GDI+).
        /// </summary>
        /// <param name="SellerName"> Seller Name  </param>
        /// <param name="VatNo"> Vat No </param>
        /// <param name="ReciptDate"> recipt Date </param>
        /// <param name="VatAmountOnly"> total vat amount  </param>
        /// <param name="TotalWithVat"> total amount  </param>
        /// <param name="option"> options to more Customizing Use QrCodeOption object to see what can be changed </param>
        /// <param name="Note"> if you like to add some note </param>
        /// <returns> A read-only stream containing the PNG image. The caller owns and disposes it. </returns>
        public static Stream GetPngStream(string SellerName, string VatNo, DateTime ReciptDate, decimal VatAmountOnly, decimal TotalWithVat, QrCodeOption? option = null, string Note = "")
        {
            return new MemoryStream(
                GetPngBytes(SellerName, VatNo, ReciptDate, VatAmountOnly, TotalWithVat, option, Note),
                writable: false);
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
        public static string GetBase64InUrl(string SellerName, string VatNo, DateTime ReciptDate, decimal VatAmountOnly, decimal TotalWithVat, QrCodeOption? option = null, string Note = "")
        {
            return "data:image/png;base64," +
                 GetBase64(SellerName, VatNo, ReciptDate, VatAmountOnly, TotalWithVat, option, Note);
        }




        /// <summary>
        /// Get QrCode  in Base64 string to use (PNG image)
        /// </summary>
        /// <param name="SellerName"> Seller Name  </param>
        /// <param name="VatNo"> Vat No </param>
        /// <param name="ReciptDate"> recipt Date </param>
        /// <param name="VatAmountOnly"> total vat amount  </param>
        /// <param name="TotalWithVat"> total amount  </param>
        /// <param name="option"> options to more Customizing Use QrCodeOption object to see what can be changed </param>
        /// <param name="Note"> if you like to add some note </param>
        /// <returns> QrCode In base64 (PNG) </returns>
        public static string GetBase64(string SellerName, string VatNo, DateTime ReciptDate, decimal VatAmountOnly, decimal TotalWithVat, QrCodeOption? option = null, string Note = "")
        {
            QrCodeGenerate Generate = new QrCodeGenerate();

            return Generate.Generate(
                TLV.Tlv.GetBase64(SellerName, VatNo, ReciptDate, VatAmountOnly, TotalWithVat)
                 , option, Note);
        }


    }
}
