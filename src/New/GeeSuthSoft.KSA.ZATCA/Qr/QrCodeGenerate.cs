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


        public string Generate(string Base64, QrCodeOption? option = null, string AddsString = "")
        {

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(Base64, QRCodeGenerator.ECCLevel.Q);
            
            QRCode qrCode = new QRCode(qrCodeData);
            var qrCodeImage = new object();
            if (option != null)
            {
                if (option.CenterImage == null)
                {
                    qrCodeImage = qrCode.GetGraphic(20, option.PointsColor, option.BackgroundColor,option.DrawQuietZones);
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
            }
            else
            {
                qrCodeImage = qrCode.GetGraphic(20);
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





    }
}
