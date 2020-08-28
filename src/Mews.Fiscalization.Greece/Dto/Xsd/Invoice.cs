using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Greece.Dto.Xsd
{
	[Serializable]
	[XmlType(Namespace = InvoicesDoc.Namespace)]
	public class Invoice
	{
		[XmlElement(ElementName = "uid")]
		public string Uid { get; set; }

		[XmlElement(ElementName = "mark")]
		public long Mark { get; set; }

		[XmlIgnore]
		public bool MarkSpecified { get; set; }

		[XmlElement(ElementName = "cancelledByMark")]
		public long CancelledByMark { get; set; }

		[XmlIgnore]
		public bool CancelledByMarkSpecified { get; set; }

		[XmlElement(ElementName = "authenticationCode")]
		public string AuthenticationCode { get; set; }

		[XmlElement(ElementName = "transmissionFailure")]
		public TransmissionFailure TransmissionFailure { get; set; }

		[XmlIgnore]
		public bool TransmissionFailureSpecified { get; set; }

		[XmlElement(ElementName = "issuer")]
		public Party Issuer { get; set; }

		[XmlElement(ElementName = "counterpart")]
		public Party Counterpart { get; set; }

		[XmlElement(ElementName = "invoiceHeader", IsNullable = false)]
		public InvoiceHeader InvoiceHeader { get; set; }

		[XmlElement(ElementName = "paymentMethods")]
		public PaymentMethodDetails[] PaymentMethods { get; set; }

		[XmlElement(ElementName = "invoiceDetails", IsNullable = false)]
		public InvoiceDetails InvoiceDetails { get; set; }

		[XmlElement(ElementName = "taxesTotals")]
		public Taxes[] TaxesTotals { get; set; }

		[XmlElement(ElementName = "invoiceSummary", IsNullable = false)]
		public InvoiceSummary InvoiceSummary { get; set; }
	}
}