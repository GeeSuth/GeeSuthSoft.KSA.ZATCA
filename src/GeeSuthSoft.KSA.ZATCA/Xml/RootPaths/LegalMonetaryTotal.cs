using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GeeSuthSoft.KSA.ZATCA.Xml.RootPaths
{
    public class LegalMonetaryTotal
    {
        [XmlElement(ElementName = "LineExtensionAmount", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public Amount LineExtensionAmount { get; set; }

        [XmlElement(ElementName = "TaxExclusiveAmount", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public Amount TaxExclusiveAmount { get; set; }

        [XmlElement(ElementName = "TaxInclusiveAmount", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public Amount TaxInclusiveAmount { get; set; }

        [XmlElement(ElementName = "AllowanceTotalAmount", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public Amount AllowanceTotalAmount { get; set; }

        [XmlElement(ElementName = "ChargeTotalAmount", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public Amount ChargeTotalAmount { get; set; }

        [XmlElement(ElementName = "PrepaidAmount", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public Amount PrepaidAmount { get; set; }

        [XmlElement(ElementName = "PayableRoundingAmount", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public Amount PayableRoundingAmount { get; set; }

        [XmlElement(ElementName = "PayableAmount", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public Amount PayableAmount { get; set; }
    }
}
