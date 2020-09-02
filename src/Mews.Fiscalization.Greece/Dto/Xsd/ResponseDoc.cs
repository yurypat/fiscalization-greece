using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Greece.Dto.Xsd
{
    [Serializable]
    [XmlType(Namespace = InvoicesDoc.Namespace)]
    [XmlRoot(ElementName = "ResponseDoc", Namespace = InvoicesDoc.Namespace)]
    public class ResponseDoc
    {
        [XmlElement(ElementName = "response")]
        public Response[] Responses { get; set; }
    }
}
