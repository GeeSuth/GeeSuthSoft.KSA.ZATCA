using GeeSuthSoft.KSA.ZATCA.Helper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace GeeSuthSoft.KSA.ZATCA.Xml.RootPaths
{
    [XmlRoot(ElementName = "Invoice", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2")]
    internal class Invoice
    {

        [XmlElement(ElementName = "ProfileID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string ProfileID { get; set; }

        [XmlElement(ElementName = "ID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public ID ID { get; set; }

        [XmlElement(ElementName = "UUID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string UUID { get; set; }

        [XmlElement(ElementName = "IssueDate", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string IssueDate { get; set; }
        [XmlIgnore]
        [JsonIgnore]
        public DateTime IssueDateAsDateTime
        {
            get => DateTime.TryParseExact(IssueDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result) ? result : DateTime.MinValue;
            set => IssueDate = value.ToString("yyyy-MM-dd");
        }

        [XmlElement(ElementName = "IssueTime", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string IssueTime { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public DateTime IssueTimeAsDateTime
        {
            get => DateTime.TryParseExact(IssueTime, "HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result) ? result : DateTime.MinValue;
            set => IssueTime = value.ToString("HH:mm:ss");
        }

        [XmlElement(ElementName = "DueDate", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string DueDate { get; set; }
        [XmlIgnore]
        [JsonIgnore]
        public DateTime DueDateAsDateTime
        {
            get => DateTime.TryParseExact(DueDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result) ? result : DateTime.MinValue;
            set => DueDate = value.ToString("yyyy-MM-dd");
        }

        [XmlElement(ElementName = "InvoiceTypeCode", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public InvoiceTypeCode InvoiceTypeCode { get; set; }

        [XmlElement(ElementName = "Note", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public Note Note { get; set; }

        [XmlElement(ElementName = "DocumentCurrencyCode", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string DocumentCurrencyCode { get; set; }

        [XmlElement(ElementName = "TaxCurrencyCode", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string TaxCurrencyCode { get; set; }

        [XmlElement(ElementName = "BillingReference", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public BillingReference BillingReference { get; set; }

        [XmlElement(ElementName = "AdditionalDocumentReference", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public AdditionalDocumentReference[] AdditionalDocumentReference { get; set; }

        [XmlElement(ElementName = "Signature", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public Signature Signature { get; set; }

        [XmlElement(ElementName = "AccountingSupplierParty", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public AccountingSupplierParty AccountingSupplierParty { get; set; }

        [XmlElement(ElementName = "AccountingCustomerParty", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public AccountingCustomerParty AccountingCustomerParty { get; set; }

        [XmlElement(ElementName = "Delivery", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public Delivery Delivery { get; set; }

        [XmlElement(ElementName = "PaymentMeans", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public PaymentMeans PaymentMeans { get; set; }

        [XmlElement(ElementName = "AllowanceCharge", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public AllowanceCharge AllowanceCharge { get; set; }

        [XmlElement(ElementName = "TaxTotal", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public TaxTotal[] TaxTotal { get; set; }

        [XmlElement(ElementName = "LegalMonetaryTotal", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public LegalMonetaryTotal LegalMonetaryTotal { get; set; }

        [XmlElement(ElementName = "InvoiceLine", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public InvoiceLine[] InvoiceLine { get; set; }


        public Invoice() { }
    }


    internal class Attachment
    {
        [XmlElement(ElementName = "EmbeddedDocumentBinaryObject", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public EmbeddedDocumentBinaryObject EmbeddedDocumentBinaryObject { get; set; }

        public Attachment() { }
    }

    internal class EmbeddedDocumentBinaryObject
    {
        [XmlAttribute(AttributeName = "mimeCode")]
        public string MimeCode { get; set; } = "text/plain";

        [XmlText]
        public string Value { get; set; }

        public EmbeddedDocumentBinaryObject() { }

        public EmbeddedDocumentBinaryObject(string value)
        {
            Value = value;
        }
    }

    internal class Delivery
    {
        [XmlElement(ElementName = "ActualDeliveryDate", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string ActualDeliveryDate { get; set; }

        [XmlElement(ElementName = "LatestDeliveryDate", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string LatestDeliveryDate { get; set; }

    }

    internal class Note
    {

        [XmlAttribute(AttributeName = "languageID")]
        public string LanguageID { get; set; }

        [XmlText]
        public string Value { get; set; }

        public Note() { }

        public Note(string value)
        {
            Value = value;
            LanguageID = ContainsArabicCharacters(value) ? "ar" : "en";
        }
        private bool ContainsArabicCharacters(string input)
        {
            // Arabic Unicode range is \u0600 - \u06FF, and Arabic Supplement \u0750 - \u077F
            return Regex.IsMatch(input, @"\p{IsArabic}");
        }

    }

    internal class PaymentMeans
    {
        [XmlElement(ElementName = "PaymentMeansCode", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string PaymentMeansCode { get; set; }

        [XmlElement(ElementName = "InstructionNote", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string InstructionNote { get; set; }

        public PaymentMeans(string paymentMeansCode)
        {
            PaymentMeansCode = paymentMeansCode;
        }
        public PaymentMeans() { }
    }
}
