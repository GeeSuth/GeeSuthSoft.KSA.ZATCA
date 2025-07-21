using System.Xml.Serialization;
using GeeSuthSoft.KSA.ZATCA.Helper;
using GeeSuthSoft.KSA.ZATCA.Xml.RootPaths;

namespace GeeSuthSoft.KSA.ZATCA.Extensions;

internal static class InvoiceXmlExtension
{
    internal static string GetCleanInvoiceXML(this Invoice InvoiceObject, bool applayXsl = true)
    {
        try
        {
            XmlSerializerNamespaces namespaces = new();
            namespaces.Add("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            namespaces.Add("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            namespaces.Add("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");

            var invoiceData = InvoiceObject.ObjectToXml(namespaces);


            if (applayXsl) { invoiceData = invoiceData.ApplyXSLT(SharedUtilities.ReadResource("ZatcaDataInvoice.xsl"), true); }

            return invoiceData.ToFormattedXml();

        }
        catch (Exception ex)
        {
            throw;
        }
    }
}