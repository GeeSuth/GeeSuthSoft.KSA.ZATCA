
using GeeSuthSoft.KSA.ZATCA.Helper;

namespace GeeSuthSoft.KSA.ZATCA.Extensions;

public static class QrExtensions
{
    public static string GenerateQrCode(this string base64SignedInvoice)
    {
        return new QR.QrCodeGenerate().Generate(base64SignedInvoice);
    }
}

