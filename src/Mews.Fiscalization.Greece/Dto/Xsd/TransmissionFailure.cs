using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Greece.Dto.Xsd
{
    [Serializable]
    public enum TransmissionFailure : byte
    {
        [XmlEnum("1")]
        ProviderCommunicationIssue = 1,
        [XmlEnum("2")]
        MyDataCommunicationIssue = 2
    }
}
