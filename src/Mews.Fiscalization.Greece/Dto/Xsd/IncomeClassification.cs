using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Greece.Dto.Xsd
{
    [Serializable]
    [XmlType(Namespace = InvoicesDoc.Namespace)]
    public class IncomeClassification
    {
        [XmlElement(ElementName = "id")]
        public byte SerialNumber { get; set; }

        [XmlIgnore]
        public bool SerialNumberSpecified { get; set; }

        [XmlElement(ElementName = "classificationType")]
        public IncomeClassificationType ClassificationType { get; set; }

        [XmlElement(ElementName = "classificationCategory", IsNullable = false)]
        public IncomeClassificationCategory ClassificationCategory { get; set; }

        [XmlElement(ElementName = "amount")]
        public decimal Amount { get; set; }
    }
}