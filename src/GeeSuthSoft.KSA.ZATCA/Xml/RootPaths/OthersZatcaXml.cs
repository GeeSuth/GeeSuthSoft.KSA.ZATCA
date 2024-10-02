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
    public class ID
    {
        [XmlAttribute(AttributeName = "schemeID")]
        public string SchemeID { get; set; }

        [XmlAttribute(AttributeName = "schemeAgencyID")]
        public string SchemeAgencyID { get; set; }

        [XmlText]
        public string Value { get; set; }

        public ID(string value)
        {
            Value = value;
        }

        public ID(string schemeID, string schemeAgencyID, string value)
        {
            SchemeID = schemeID;
            SchemeAgencyID = schemeAgencyID;
            Value = value;
        }

        public ID() { }
    }

    public class Amount
    {
        [XmlAttribute(AttributeName = "currencyID")]
        public string CurrencyID { get; set; }

        [XmlText]
        public string Value { get; set; } = "0";

        [XmlIgnore]
        [JsonIgnore]
        public decimal NumericValue
        {
            get => decimal.TryParse(Value, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal result) ? result : 0;
            set => Value = value.ToString(CultureInfo.InvariantCulture);
        }

        public Amount(string currencyID, double value)
        {
            CurrencyID = currencyID;
            Value = value.ToString("F2", CultureInfo.InvariantCulture);
            //Value = value.ToString("G17", CultureInfo.InvariantCulture); // G17 memastikan bahwa semua digit desimal disimpan
        }

        public Amount() { }
    }

    public class TaxCategory
    {
        [XmlElement(ElementName = "ID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public ID ID { get; set; }

        [XmlElement(ElementName = "Percent", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public double Percent { get; set; }

        [XmlElement(ElementName = "TaxExemptionReasonCode", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string TaxExemptionReasonCode { get; set; }

        [XmlElement(ElementName = "TaxExemptionReason", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string TaxExemptionReason { get; set; }

        [XmlElement(ElementName = "TaxScheme", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public TaxScheme TaxScheme { get; set; }

        public TaxCategory() { }
    }

    public class TaxScheme
    {
        [XmlElement(ElementName = "ID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public ID ID { get; set; }
    }
}
