
using System.Drawing;
using System.Runtime.CompilerServices;
using GeeSuthSoft.KSA.ZATCA.Dto;
using GeeSuthSoft.KSA.ZATCA.Helper;
using GeeSuthSoft.KSA.ZATCA.QR;

namespace GeeSuthSoft.KSA.ZATCA.Extensions;

public static class QrExtensions
{
    public static string GenerateURLImage(this ContentQR contentQR)
    {
        return new QR.QrCodeGenerate().Generate(contentQR.Base64QrCodeContent);
    }

    /// <summary>
    /// return the QR of invoice can directly open in browser "data:image/png;base64,....." 
    /// </summary>
    /// <param name="contentQR"></param>
    /// <returns></returns>
    public static string GenerateImageDirectOpenInBrowser(this ContentQR contentQR)
    {
        return  "data:image/png;base64," + contentQR.GenerateURLImage();
    }

}

