using GeeSuthSoft.KSA.ZATCA.Dto;
using GeeSuthSoft.KSA.ZATCA.QR;

namespace GeeSuthSoft.KSA.ZATCA.Extensions;

public static class QrExtensions
{
    /// <summary>
    /// return the QR of invoice as a base64 encoded PNG image.
    /// </summary>
    /// <param name="contentQR"></param>
    /// <returns></returns>
    public static string GenerateURLImage(this ContentQR contentQR)
    {
        if (contentQR == null)
        {
            throw new ArgumentNullException(nameof(contentQR));
        }

        return new QrCodeGenerate().Generate(contentQR.Base64QrCodeContent);
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
