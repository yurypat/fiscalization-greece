using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Greece.Dto.Xsd
{
    [Serializable]
    [XmlType(Namespace = InvoicesDoc.Namespace)]
    public class PaymentMethodDetails
    {
        [XmlElement(ElementName = "type", IsNullable = false)]
        public PaymentMethodType Type { get; set; }

        [XmlElement(ElementName = "amount", IsNullable = false)]
        public decimal Amount { get; set; }

        [XmlElement(ElementName = "paymentMethodInfo")]
        public string PaymentMethodInfo { get; set; }
    }
}