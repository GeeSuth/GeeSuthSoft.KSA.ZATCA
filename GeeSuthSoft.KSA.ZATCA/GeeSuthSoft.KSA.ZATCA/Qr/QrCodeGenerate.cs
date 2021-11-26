using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRCoder;
using GeeSuthSoft.KSA.ZATCA.Qr;

namespace GeeSuthSoft.KSA.ZATCA.Qr
{
    public class QrCodeGenerate
    {

        #region Based Method



        private static string Generate(string ContentTlvInBase64, QrCodeOption option = null, string AddsString = "")
        {

           

            

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(
                ContentTlvInBase64
                , QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            var qrCodeImage = new object();
            if (option.CenterImage == null)
            {
                qrCodeImage = qrCode.GetGraphic(20,
               option.PointsColor,
               option.BackgroundColor,
               option.DrawQuietZones);
            }
            else
            {
                qrCodeImage = qrCode.GetGraphic(20,
                ColorTranslator.FromHtml(option.PointsColor),
                ColorTranslator.FromHtml(option.BackgroundColor),
                option.CenterImage,
                option.IconSizePercent,
                option.IconBorderWidth,
                option.DrawQuietZones);
            }
            using (var ms = new MemoryStream())
            {
                using (var bitmap = new Bitmap((Bitmap)qrCodeImage))
                {
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    var SigBase64 = Convert.ToBase64String(ms.GetBuffer()); //Get Base64

                    return SigBase64;
                }
            }
        }





        #endregion


        #region General

        /// <summary>
        /// Get QrCode  in Base64 string to use
        /// </summary>
        /// <param name="sellerName"> Seller Name  </param>
        /// <param name="vatRegisterId"> Vat No </param>
        /// <param name="time"> recipt Date </param>
        /// <param name="vatTotal"> total vat amount  </param>
        /// <param name="TotalInvoice"> total amount  </param>
        /// <param name="option"> options to more Customizing Use QrCodeOption object to see what can be changed </param>
        /// <param name="Note"> if you like to add some note </param>
        /// <returns> QrCode In base64 </returns>
        public static string GetBase64(string sellerName, string vatRegisterId, DateTime time, decimal vatTotal, decimal TotalInvoice, QrCodeOption option = null, string Note = "")
        {

            return Generate(QrHelper.GenerateTLV(new Models.QrContent(
                sellerName, vatRegisterId, time, vatTotal, TotalInvoice
                )), option);
        }



        /// <summary>
        /// Get QrCode  in Base64 string can be opened in browser just past result in url 
        /// </summary>
        /// <param name="sellerName"> Seller Name  </param>
        /// <param name="vatRegisterId"> Vat No </param>
        /// <param name="time"> recipt Date </param>
        /// <param name="vatTotal"> total vat amount  </param>
        /// <param name="TotalInvoice"> total amount  </param>
        /// <param name="option"> options to more Customizing Use QrCodeOption object to see what can be changed </param>
        /// <param name="Note"> if you like to add some note </param>
        /// <returns> data:image/png;base64,string </returns>
        public static string GetBase64InUrl(string sellerName, string vatRegisterId, DateTime time, decimal vatTotal, decimal TotalInvoice, QrCodeOption option = null, string Note = "")
        {
            return "data:image/png;base64," + Generate(QrHelper.GenerateTLV(new Models.QrContent(
                sellerName, vatRegisterId, time, vatTotal, TotalInvoice
                )), option);
        }




        /// <summary>
        /// Get QrCode In Image 
        /// </summary>
        /// <param name="sellerName"> Seller Name  </param>
        /// <param name="vatRegisterId"> Vat No </param>
        /// <param name="time"> recipt Date </param>
        /// <param name="vatTotal"> total vat amount  </param>
        /// <param name="TotalInvoice"> total amount  </param>
        /// <param name="option"> options to more Customizing Use QrCodeOption object to see what can be changed </param>
        /// <param name="Note"> if you like to add some note </param>
        /// <returns> Image Object </returns>
        public static Image GetImage(string sellerName, string vatRegisterId, DateTime time, decimal vatTotal, decimal TotalInvoice, QrCodeOption option = null, string Note = "")
        {
            return (Bitmap)new ImageConverter().ConvertFrom(Convert.FromBase64String(
                Generate(QrHelper.GenerateTLV(new Models.QrContent(
                sellerName, vatRegisterId, time, vatTotal, TotalInvoice
                )), option)
                ));
        }




        /// <summary>
        /// Get The TLV data To check this diectly with ZATCA tools 
        /// </summary>
        /// <param name="sellerName"> Seller Name  </param>
        /// <param name="vatRegisterId"> Vat No </param>
        /// <param name="time"> recipt Date </param>
        /// <param name="vatTotal"> total vat amount  </param>
        /// <param name="TotalInvoice"> total amount  </param>
        /// <param name="option"> options to more Customizing Use QrCodeOption object to see what can be changed </param>
        /// <param name="Note"> if you like to add some note </param>
        /// <returns> QrCode In base64 </returns>
        public static string GetTLVString(string sellerName, string vatRegisterId, DateTime time, decimal vatTotal, decimal TotalInvoice, QrCodeOption option = null, string Note = "")
        {


            return QrHelper.GenerateTLV(new Models.QrContent(
                sellerName, vatRegisterId, time, vatTotal, TotalInvoice
                ));
        }



        #endregion




    }
}
