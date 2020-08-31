using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Greece.Dto.Xsd
{
    [Serializable]
    [XmlRoot(Namespace = InvoicesDoc.Namespace)]
    public class Taxes
    {
        [XmlElement(ElementName = "id")]
        public byte Id { get; set; }

        [XmlIgnore]
        public bool IdSpecified { get; set; }

        [XmlElement(ElementName = "taxType", IsNullable = false)]
        public TaxType TaxType { get; set; }

        [XmlElement(ElementName = "taxCategory")]
        public TaxCategory TaxCategory { get; set; }

        [XmlElement(ElementName = "underlyingValue")]
        public decimal UnderlyingValue { get; set; }

        [XmlIgnore]
        public bool UnderlyingValueSpecified { get; set; }

        [XmlElement(ElementName = "taxAmount", IsNullable = false)]
        public decimal TaxAmount { get; set; }
    }
}