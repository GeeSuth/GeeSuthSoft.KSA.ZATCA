using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GeeSuthSoft.KSA.ZATCA.Xml.RootPaths
{
    public class AccountingSupplierParty
    {
        [XmlElement(ElementName = "Party", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public Party Party { get; set; }
    }
    public class AccountingCustomerParty
    {
        [XmlElement(ElementName = "Party", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public Party Party { get; set; }
    }

    public class Party
    {
        [XmlElement(ElementName = "PartyIdentification", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public PartyIdentification PartyIdentification { get; set; }

        [XmlElement(ElementName = "PostalAddress", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public PostalAddress PostalAddress { get; set; }

        [XmlElement(ElementName = "PartyTaxScheme", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public PartyTaxScheme PartyTaxScheme { get; set; }

        [XmlElement(ElementName = "PartyLegalEntity", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public PartyLegalEntity PartyLegalEntity { get; set; }
    }

    public class PartyIdentification
    {
        [XmlElement(ElementName = "ID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public ID ID { get; set; }
    }

    public class PostalAddress
    {
        [XmlElement(ElementName = "StreetName", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string StreetName { get; set; }

        [XmlElement(ElementName = "BuildingNumber", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string BuildingNumber { get; set; }

        [XmlElement(ElementName = "CitySubdivisionName", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string CitySubdivisionName { get; set; }

        [XmlElement(ElementName = "CityName", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string CityName { get; set; }

        [XmlElement(ElementName = "PostalZone", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string PostalZone { get; set; }

        [XmlElement(ElementName = "Country", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public Country Country { get; set; }
    }

    public class PartyTaxScheme
    {
        [XmlElement(ElementName = "CompanyID", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string CompanyID { get; set; }
        [XmlElement(ElementName = "TaxScheme", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2")]
        public TaxScheme TaxScheme { get; set; }
    }

    public class PartyLegalEntity
    {
        [XmlElement(ElementName = "RegistrationName", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string RegistrationName { get; set; }

        public PartyLegalEntity(string registrationName)
        {
            RegistrationName = registrationName;
        }

        public PartyLegalEntity() { }
    }

    public class Country
    {
        [XmlElement(ElementName = "IdentificationCode", Namespace = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2")]
        public string IdentificationCode { get; set; }

        public Country(string identificationCode)
        {
            IdentificationCode = identificationCode;
        }
        public Country() { }

    }
}
