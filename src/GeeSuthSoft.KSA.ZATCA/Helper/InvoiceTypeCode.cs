using GeeSuthSoft.KSA.ZATCA.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GeeSuthSoft.KSA.ZATCA.Helper
{
    public class InvoiceTypeCode
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlText]
        public string Value { get; set; }

        public InvoiceTypeCode(InvoiceType type, string subType)
        {
            string typeCode = ((int)type).ToString();
            Name = (subType.Trim() + "0000000")[..7];
            Value = typeCode;
        }

        public InvoiceTypeCode() { }
    }
}
