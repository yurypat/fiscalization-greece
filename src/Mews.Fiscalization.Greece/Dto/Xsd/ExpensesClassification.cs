using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Greece.Dto.Xsd
{
    [Serializable]
    [XmlType(Namespace = InvoicesDoc.Namespace)]
    public class ExpensesClassification
    {
        [XmlElement(ElementName = "classificationType")]
        public ExpenseClassificationType ClassificationType { get; set; }

        [XmlElement(ElementName = "classificationCategory")]
        public ExpenseClassificationCategory ClassificationCategory { get; set; }

        [XmlElement(ElementName = "amount")]
        public string Amount { get; set; }

        [XmlElement(ElementName = "id")]
        public byte Id { get; set; }

        [XmlIgnore]
        public bool IdSpecified { get; set; }
    }
}