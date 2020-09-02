using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Greece.Dto.Xsd
{
    [Serializable]
    [XmlType(Namespace = InvoicesDoc.Namespace)]
    public class ActivityUndertakingDeclaration
    {
        [XmlElement(ElementName = "applicationId", IsNullable = false)]
        public string ApplicationId { get; set; }

        [XmlElement(ElementName = "applicationDate", IsNullable = false)]
        public DateTime ApplicationDate { get; set; }

        [XmlElement(ElementName = "doy")]
        public string TaxOffice { get; set; }

        [XmlElement(ElementName = "shipID", IsNullable = false)]
        public string ShipId { get; set; }
    }
}
