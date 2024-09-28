using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GeeSuthSoft.KSA.ZATCA.Xml.RootPaths
{

    public class InvoiceLine
    {
        [XmlElement(ElementName = "ID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public ID ID { get; set; }

        [XmlElement(ElementName = "InvoicedQuantity", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public InvoicedQuantity InvoicedQuantity { get; set; }

        [XmlElement(ElementName = "LineExtensionAmount", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public Amount LineExtensionAmount { get; set; }

        [XmlElement(ElementName = "DocumentReference", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public DocumentReference[] DocumentReference { get; set; }

        [XmlElement(ElementName = "TaxTotal", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public TaxTotal TaxTotal { get; set; }

        [XmlElement(ElementName = "Item", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public Item Item { get; set; }

        [XmlElement(ElementName = "Price", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public Price Price { get; set; }

        [XmlElement(ElementName = "AllowanceCharge", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public Amount AllowanceCharge { get; set; }
    }

    public class InvoicedQuantity
    {
        [XmlAttribute(AttributeName = "unitCode")]
        public string UnitCode { get; set; }

        [XmlText]
        public string Quantity { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public decimal NumericValue
        {
            get => decimal.TryParse(Quantity, out decimal result) ? result : 0;
            set => Quantity = value.ToString();
        }

        public InvoicedQuantity(string unitCode, double quantity)
        {
            UnitCode = unitCode;
            Quantity = quantity.ToString();
        }
        public InvoicedQuantity() { }
    }

    public class Item
    {
        [XmlElement(ElementName = "Name", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string Name { get; set; }

        [XmlElement(ElementName = "ClassifiedTaxCategory", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public ClassifiedTaxCategory ClassifiedTaxCategory { get; set; }
    }

    public class DocumentReference
    {
        [XmlElement(ElementName = "ID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public ID ID { get; set; }

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

        [XmlElement(ElementName = "DocumentTypeCode", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string DocumentTypeCode { get; set; }


    }

    public class ClassifiedTaxCategory
    {
        [XmlElement(ElementName = "ID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public ID ID { get; set; }

        [XmlElement(ElementName = "Percent", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public double Percent { get; set; }

        [XmlElement(ElementName = "TaxExemptionReason", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string TaxExemptionReason { get; set; }

        [XmlElement(ElementName = "TaxExemptionReasonCode", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string TaxExemptionReasonCode { get; set; }

        //[XmlIgnore]
        //[JsonIgnore]
        //public decimal NumericValue
        //{
        //    get => decimal.TryParse(Percent, out decimal result) ? result : 0;
        //    set => Percent = value.ToString();
        //}

        [XmlElement(ElementName = "TaxScheme", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public TaxScheme TaxScheme { get; set; }
    }

    public class Price
    {
        [XmlElement(ElementName = "PriceAmount", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public Amount PriceAmount { get; set; }

        [XmlElement(ElementName = "AllowanceCharge", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public AllowanceCharge AllowanceCharge { get; set; }
    }
}
