using GeeSuthSoft.KSA.ZATCA.Models;

namespace GeeSuthSoft.KSA.ZATCA.XunitTest.HelperTest;
using System;
using System.Text;
using System.Xml.Linq;
    

public class DecodeInvoiceHelper
{

public class DecodedData
{
    public byte[] RawBytes { get; set; }
    public string TextData { get; set; }
    public bool IsText { get; set; }
    public CryptographicStamp Signature { get; set; }
    public QrCodeData QrCode { get; set; }
}

public class QrCodeData
{
    public string ArabicCompanyName { get; set; }
    public string EnglishCompanyName { get; set; }
    public string TaxNumber { get; set; }
    public DateTime Timestamp { get; set; }
    public decimal Amount1 { get; set; }
    public decimal Amount2 { get; set; }
    public string Hash1 { get; set; }
    public string Hash2 { get; set; }
    public string Signature { get; set; }
}

public class CryptographicStamp
{
    public string SignatureValue { get; set; }
    public string X509Certificate { get; set; }
}

public class Base64Parser
{
    public DecodedData ParseBase64(string base64String)
    {
        var result = new DecodedData();

        try
        {
            // Decode Base64 to bytes
            byte[] data = Convert.FromBase64String(base64String);
            result.RawBytes = data;

            // Attempt to decode as UTF-8 text
            string decodedText = Encoding.UTF8.GetString(data);
            result.IsText = IsValidUtf8(data);

            if (result.IsText)
            {
                result.TextData = decodedText;

                // Try parsing as QR code data (custom format)
                result.QrCode = ParseQrCodeData(decodedText);

                // Try parsing as XML signature (cryptographic stamp)
                if (result.QrCode == null)
                {
                    result.Signature = ParseXmlSignature(decodedText);
                }
            }

            return result;
        }
        catch (FormatException)
        {
            throw new ArgumentException("Invalid Base64 string");
        }
    }

    private QrCodeData ParseQrCodeData(string text)
    {
        // Example format: "SellerVAT=123456|Total=4800.00|Hash=abc123|Timestamp=2023-01-01T12:00:00"
        var qrData = new QrCodeData();
        
        // Split fields using non-printable ASCII control characters (e.g., 0x01, 0x02)
        string[] parts = text.Split(new[] { 
            (char)1, (char)2, (char)3, (char)4, (char)5, 
            (char)6, (char)7, (char)8, (char)16, (char)7 
        }, StringSplitOptions.RemoveEmptyEntries);

        // Map parts to properties (adjust indices based on observed structure)
        if (parts.Length >= 1)
        {
            // Split company name into Arabic and English
            string[] companyParts = parts[0].Split(new[] { " | " }, StringSplitOptions.None);
            if (companyParts.Length >= 2)
            {
                qrData.ArabicCompanyName = companyParts[0].TrimStart((char)0x6F); // Remove leading "o"
                qrData.EnglishCompanyName = companyParts[1];
            }
        }

        if (parts.Length >= 2) qrData.TaxNumber = parts[1];
        if (parts.Length >= 3 && DateTime.TryParse(parts[2], out DateTime ts)) qrData.Timestamp = ts;
        if (parts.Length >= 4 && decimal.TryParse(parts[3], out decimal amt1)) qrData.Amount1 = amt1;
        if (parts.Length >= 5 && decimal.TryParse(parts[4], out decimal amt2)) qrData.Amount2 = amt2;
        if (parts.Length >= 6) qrData.Hash1 = parts[5];
        if (parts.Length >= 7) qrData.Hash2 = parts[6];
        if (parts.Length >= 8) qrData.Signature = parts[7];

        return qrData;
    }

    private CryptographicStamp ParseXmlSignature(string xmlText)
    {
        try
        {
            var doc = XDocument.Parse(xmlText);
            var ns = XNamespace.Get("http://www.w3.org/2000/09/xmldsig#");

            return new CryptographicStamp
            {
                SignatureValue = doc.Descendants(ns + "SignatureValue").FirstOrDefault()?.Value,
                X509Certificate = doc.Descendants(ns + "X509Certificate").FirstOrDefault()?.Value
            };
        }
        catch
        {
            return null; // Not valid XML
        }
    }

    private bool IsValidUtf8(byte[] data)
    {
        try
        {
            Encoding.UTF8.GetString(data);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
}

public static class DecodeInvoiceHelperExt
{
    public static DecodeInvoiceHelper.DecodedData DecodeToObject(this ContentQR value)
    {
        var parser = new DecodeInvoiceHelper.Base64Parser();
        DecodeInvoiceHelper.DecodedData result = parser.ParseBase64(value.Base64QrCodeContent);
        return result;
    }
}