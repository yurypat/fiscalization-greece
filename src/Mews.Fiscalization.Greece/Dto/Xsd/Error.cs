using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Greece.Dto.Xsd
{
    [Serializable]
    [XmlType(Namespace = InvoicesDoc.Namespace)]
    public class Error
    {
        [XmlElement(ElementName = "message")]
        public string Message { get; set; }

        [XmlElement(ElementName = "code")]
        public string Code { get; set; }
    }
}