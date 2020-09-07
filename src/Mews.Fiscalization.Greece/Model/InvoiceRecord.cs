using System;
using System.Collections.Generic;
using System.Text;

namespace Mews.Fiscalization.Greece.Model
{
    public class InvoiceRecord
    {
		public InvoiceRecord(string invoiceIdentifier, long? invoiceRegistrationNumber, long? cancelledByInvoiceRegistrationNumber, string authenticationCode, InvoiceRecordParty issuer, InvoiceRecordParty counterpart, 
			InvoiceRecordHeader invoiceHeader, IEnumerable<InvoiceRecordPaymentMethodDetails> paymentMethods, InvoiceRecordDetails invoiceDetails, 
			IEnumerable<InvoiceRecordTaxes> taxesTotals, InvoiceRecordSummary invoiceSummary)
		{
			InvoiceIdentifier = invoiceIdentifier;
			InvoiceRegistrationNumber = invoiceRegistrationNumber;
			CancelledByInvoiceRegistrationNumber = cancelledByInvoiceRegistrationNumber;
			AuthenticationCode = authenticationCode;
			Issuer = issuer;
			Counterpart = counterpart;
			InvoiceHeader = invoiceHeader;
			PaymentMethods = paymentMethods;
			InvoiceDetails = invoiceDetails;
			TaxesTotals = taxesTotals;
			InvoiceSummary = invoiceSummary;
		}

		public string InvoiceIdentifier { get; }

		public long? InvoiceRegistrationNumber { get; }

		public long? CancelledByInvoiceRegistrationNumber { get; }

		public string AuthenticationCode { get; }

		public InvoiceRecordParty Issuer { get; }

		public InvoiceRecordParty Counterpart { get; }

		public InvoiceRecordHeader InvoiceHeader { get; set; }

		public IEnumerable<InvoiceRecordPaymentMethodDetails> PaymentMethods { get; set; }

		public InvoiceRecordDetails InvoiceDetails { get; }

		public IEnumerable<InvoiceRecordTaxes> TaxesTotals { get; }

		public InvoiceRecordSummary InvoiceSummary { get; }
	}
}
