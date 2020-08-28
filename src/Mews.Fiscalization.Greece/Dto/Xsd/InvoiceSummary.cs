using System.Xml.Serialization;

namespace Mews.Fiscalization.Greece.Dto.Xsd
{
    [XmlRoot(ElementName = "invoiceSummary", Namespace = "http://www.aade.gr/myDATA/invoice/v1.0")]
    public class InvoiceSummary
    {
        [XmlElement(ElementName = "totalNetValue", Namespace = "http://www.aade.gr/myDATA/invoice/v1.0")]
        public string TotalNetValue { get; set; }

        [XmlElement(ElementName = "totalVatAmount", Namespace = "http://www.aade.gr/myDATA/invoice/v1.0")]
        public string TotalVatAmount { get; set; }

        [XmlElement(ElementName = "totalWithheldAmount", Namespace = "http://www.aade.gr/myDATA/invoice/v1.0")]
        public string TotalWithheldAmount { get; set; }

        [XmlElement(ElementName = "totalFeesAmount", Namespace = "http://www.aade.gr/myDATA/invoice/v1.0")]
        public string TotalFeesAmount { get; set; }

        [XmlElement(ElementName = "totalStampDutyAmount", Namespace = "http://www.aade.gr/myDATA/invoice/v1.0")]
        public string TotalStampDutyAmount { get; set; }

        [XmlElement(ElementName = "totalOtherTaxesAmount", Namespace = "http://www.aade.gr/myDATA/invoice/v1.0")]
        public string TotalOtherTaxesAmount { get; set; }

        [XmlElement(ElementName = "totalDeductionsAmount", Namespace = "http://www.aade.gr/myDATA/invoice/v1.0")]
        public string TotalDeductionsAmount { get; set; }

        [XmlElement(ElementName = "totalGrossValue", Namespace = "http://www.aade.gr/myDATA/invoice/v1.0")]
        public string TotalGrossValue { get; set; }

        [XmlElement(ElementName = "incomeClassification", Namespace = "http://www.aade.gr/myDATA/invoice/v1.0")]
        public IncomeClassification IncomeClassification { get; set; }

        [XmlElement(ElementName = "expensesClassification", Namespace = "http://www.aade.gr/myDATA/invoice/v1.0")]
        public ExpensesClassification ExpensesClassification { get; set; }
    }
}