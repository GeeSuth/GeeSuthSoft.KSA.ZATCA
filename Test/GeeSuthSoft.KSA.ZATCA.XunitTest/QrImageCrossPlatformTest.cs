using System.Text;
using GeeSuthSoft.KSA.ZATCA.Dto;
using GeeSuthSoft.KSA.ZATCA.Extensions;
using GeeSuthSoft.KSA.ZATCA.QR;

namespace GeeSuthSoft.KSA.ZATCA.XunitTest;

// Qr is obsolete, but its public API is exactly what these regression tests cover.
#pragma warning disable CS0618

/// <summary>
/// Regression tests for QR image generation on Linux/Docker (no System.Drawing/GDI+).
/// </summary>
public class QrImageCrossPlatformTest
{
    private const string PngDataUrlPrefix = "data:image/png;base64,";

    private static readonly byte[] PngSignature = { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
    private static readonly byte[] PngEndChunk =
        { 0x00, 0x00, 0x00, 0x00, 0x49, 0x45, 0x4E, 0x44, 0xAE, 0x42, 0x60, 0x82 };

    private const string SellerName = "مؤسسة سالم ناصر احمد";
    private const string VatNumber = "399999999900003";
    private const decimal VatAmount = 15m;
    private const decimal TotalWithVat = 115m;
    private static readonly DateTime ReceiptDate = new DateTime(2024, 5, 15, 10, 30, 0, DateTimeKind.Utc);

    [Fact]
    public void Generate_ReturnsDecodablePng()
    {
        var base64 = new QrCodeGenerate().Generate("test-content");

        Assert.False(string.IsNullOrWhiteSpace(base64));
        AssertValidPng(Convert.FromBase64String(base64));
    }

    [Fact]
    public void GetBase64InUrl_IsPngDataUrl()
    {
        var dataUrl = Qr.GetBase64InUrl(SellerName, VatNumber, ReceiptDate, VatAmount, TotalWithVat);

        Assert.StartsWith(PngDataUrlPrefix, dataUrl);
        AssertValidPng(Convert.FromBase64String(dataUrl.Substring(PngDataUrlPrefix.Length)));
    }

    [Fact]
    public void Qr_GetBase64_MatchesPngSignature()
    {
        var base64 = Qr.GetBase64(SellerName, VatNumber, ReceiptDate, VatAmount, TotalWithVat);

        AssertValidPng(Convert.FromBase64String(base64));
    }

    [Fact]
    public void Qr_GetPngBytes_MatchesPngSignature()
    {
        var png = Qr.GetPngBytes(SellerName, VatNumber, ReceiptDate, VatAmount, TotalWithVat);

        AssertValidPng(png);
    }

    [Fact]
    public void QrExtensions_GenerateImageDirectOpenInBrowser_IsPngDataUrl()
    {
        var contentQR = new ContentQR { Base64QrCodeContent = "test-content" };

        var dataUrl = contentQR.GenerateImageDirectOpenInBrowser();

        Assert.StartsWith(PngDataUrlPrefix, dataUrl);
        AssertValidPng(Convert.FromBase64String(dataUrl.Substring(PngDataUrlPrefix.Length)));
    }

    [Fact]
    public void Generate_WithOptions_ReturnsPngWithoutGdiPlus()
    {
        // Passing QrCodeOption must not initialize System.Drawing/GDI+ when CenterImage is null.
        var option = new QrCodeOption
        {
            PointsColor = "#000",
            BackgroundColor = "#FFFFFF",
            DrawQuietZones = false
        };

        var png = Convert.FromBase64String(new QrCodeGenerate().Generate("test-content", option));

        AssertValidPng(png);
    }

    [Fact]
    public void Generate_WithInvalidColors_FallsBackToDefaults()
    {
        var option = new QrCodeOption
        {
            PointsColor = "not-a-color",
            BackgroundColor = "#12"
        };

        var png = Convert.FromBase64String(new QrCodeGenerate().Generate("test-content", option));

        AssertValidPng(png);
    }

    [Fact]
    public void ParseRgba_SupportsRgbRrgbaaAndAarrggbb()
    {
        var fallback = new byte[] { 0x01, 0x02, 0x03, 0x04 };

        Assert.Equal(new byte[] { 0x11, 0x22, 0x33, 0xFF }, QrCodeGenerate.ParseRgba("#123", fallback));
        Assert.Equal(new byte[] { 0x11, 0x22, 0x33, 0xFF }, QrCodeGenerate.ParseRgba("#112233", fallback));
        Assert.Equal(new byte[] { 0x11, 0x22, 0x33, 0x44 }, QrCodeGenerate.ParseRgba("#44112233", fallback));
        Assert.Same(fallback, QrCodeGenerate.ParseRgba("invalid", fallback));
        Assert.Same(fallback, QrCodeGenerate.ParseRgba(null, fallback));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Generate_EmptyContent_ThrowsArgumentException(string content)
    {
        Assert.Throws<ArgumentException>(() => new QrCodeGenerate().Generate(content));
    }

    private static void AssertValidPng(byte[] png)
    {
        Assert.NotNull(png);
        Assert.True(png.Length > 8 + 25 + PngEndChunk.Length, "PNG is too short to be a valid image.");

        Assert.Equal(PngSignature, png.Take(PngSignature.Length).ToArray());

        // First chunk must be IHDR with a 13 byte payload.
        Assert.Equal(13, ReadBigEndianInt32(png, 8));
        Assert.Equal("IHDR", Encoding.ASCII.GetString(png, 12, 4));

        var width = ReadBigEndianInt32(png, 16);
        var height = ReadBigEndianInt32(png, 20);
        Assert.True(width > 0, "PNG width must be greater than zero.");
        Assert.True(height > 0, "PNG height must be greater than zero.");

        // A complete PNG stream ends with the IEND chunk.
        Assert.Equal(PngEndChunk, png.Skip(png.Length - PngEndChunk.Length).ToArray());
    }

    private static int ReadBigEndianInt32(byte[] bytes, int offset)
    {
        return (bytes[offset] << 24)
             | (bytes[offset + 1] << 16)
             | (bytes[offset + 2] << 8)
             | bytes[offset + 3];
    }
}

#pragma warning restore CS0618
