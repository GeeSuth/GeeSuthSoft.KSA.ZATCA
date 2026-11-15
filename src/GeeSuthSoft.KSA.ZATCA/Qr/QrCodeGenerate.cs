using QRCoder;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;

namespace GeeSuthSoft.KSA.ZATCA.QR
{
    internal class QrCodeGenerate
    {
        private const int PixelsPerModule = 20;

        private static readonly byte[] DefaultDarkColorRgba = { 0x15, 0x46, 0x3F, 0xFF };
        private static readonly byte[] DefaultLightColorRgba = { 0xFF, 0xFF, 0xFF, 0xFF };

        #region Based Method


        public string Generate(string Base64, QrCodeOption? option = null, string AddsString = "")
        {
            if (string.IsNullOrWhiteSpace(Base64))
            {
                throw new ArgumentException("QR code content must not be null or empty.", nameof(Base64));
            }

            if (option?.CenterImage != null)
            {
                if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    throw new PlatformNotSupportedException(
                        "Center images are rendered with System.Drawing (GDI+), which is only supported on Windows. " +
                        "Set QrCodeOption.CenterImage to null to generate QR codes on Linux/Docker.");
                }

                return GenerateWithCenterImage(Base64, option);
            }

            using (var qrGenerator = new QRCodeGenerator())
            using (var qrData = qrGenerator.CreateQrCode(Base64, QRCodeGenerator.ECCLevel.Q))
            using (var pngQrCode = new PngByteQRCode(qrData))
            {
                // PngByteQRCode is fully managed (no System.Drawing/GDI+) and works on Linux/Docker.
                var pngBytes = pngQrCode.GetGraphic(
                    pixelsPerModule: PixelsPerModule,
                    darkColorRgba: ParseRgba(option?.PointsColor, DefaultDarkColorRgba),
                    lightColorRgba: ParseRgba(option?.BackgroundColor, DefaultLightColorRgba),
                    drawQuietZones: option?.DrawQuietZones ?? true);

                return Convert.ToBase64String(pngBytes);
            }
        }


        #endregion

        /// <summary>
        /// Windows-only fallback used when <see cref="QrCodeOption.CenterImage"/> is set.
        /// Never called on non-Windows platforms (see <see cref="Generate"/>).
        /// </summary>
#pragma warning disable CA1416 // Center-image rendering is guarded by the Windows runtime check in Generate.
        private static string GenerateWithCenterImage(string content, QrCodeOption option)
        {
            var darkColor = ParseRgba(option.PointsColor, DefaultDarkColorRgba);
            var lightColor = ParseRgba(option.BackgroundColor, DefaultLightColorRgba);

            using (var qrGenerator = new QRCodeGenerator())
            using (var qrData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q))
            using (var qrCode = new QRCode(qrData))
            using (var bitmap = qrCode.GetGraphic(
                PixelsPerModule,
                Color.FromArgb(darkColor[3], darkColor[0], darkColor[1], darkColor[2]),
                Color.FromArgb(lightColor[3], lightColor[0], lightColor[1], lightColor[2]),
                option.CenterImage,
                option.IconSizePercent,
                option.IconBorderWidth,
                option.DrawQuietZones))
            using (var ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                return Convert.ToBase64String(ms.ToArray());
            }
        }
#pragma warning restore CA1416

        /// <summary>
        /// Parses a hex HTML color ("#RGB", "#RRGGBB" or "#AARRGGBB") into RGBA bytes.
        /// Invalid input falls back to <paramref name="fallback"/>.
        /// </summary>
        internal static byte[] ParseRgba(string? htmlColor, byte[] fallback)
        {
            if (string.IsNullOrWhiteSpace(htmlColor))
            {
                return fallback;
            }

            var hex = htmlColor!.Trim();
            if (hex.StartsWith("#", StringComparison.Ordinal))
            {
                hex = hex.Substring(1);
            }

            if (hex.Length == 3)
            {
                hex = $"{hex[0]}{hex[0]}{hex[1]}{hex[1]}{hex[2]}{hex[2]}";
            }

            var alphaHex = "FF";
            if (hex.Length == 8)
            {
                // #AARRGGBB
                alphaHex = hex.Substring(0, 2);
                hex = hex.Substring(2);
            }

            if (hex.Length != 6)
            {
                return fallback;
            }

            var rgba = new byte[4];
            for (var i = 0; i < 3; i++)
            {
                if (!TryParseHexByte(hex.Substring(i * 2, 2), out rgba[i]))
                {
                    return fallback;
                }
            }

            if (!TryParseHexByte(alphaHex, out rgba[3]))
            {
                return fallback;
            }

            return rgba;
        }

        private static bool TryParseHexByte(string value, out byte result)
        {
            return byte.TryParse(
                value,
                NumberStyles.HexNumber,
                CultureInfo.InvariantCulture,
                out result);
        }
    }
}
