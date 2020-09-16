using Mews.Fiscalization.Greece.Model.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Greece.Model
{
    public class InvoiceRecord
    {
        public InvoiceRecord(
            InvoiceRecordParty issuer,
            InvoiceRecordHeader invoiceHeader,
            IEnumerable<InvoiceRecordDetail> invoiceDetails,
            InvoiceRecordSummary invoiceSummary,
            StringIdentifier invoiceIdentifier = null,
            InvoiceRegistrationNumber invoiceRegistrationNumber = null,
            InvoiceRegistrationNumber cancelledByInvoiceRegistrationNumber = null,
            InvoiceRecordParty counterpart = null,
            IEnumerable<InvoiceRecordPaymentMethodDetails> paymentMethods = null)
        {
            Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
            InvoiceHeader = invoiceHeader ?? throw new ArgumentNullException(nameof(invoiceHeader));
            InvoiceDetails = invoiceDetails ?? throw new ArgumentNullException(nameof(invoiceDetails));
            InvoiceSummary = invoiceSummary ?? throw new ArgumentNullException(nameof(invoiceSummary));
            InvoiceIdentifier = invoiceIdentifier;
            InvoiceRegistrationNumber = invoiceRegistrationNumber;
            CanceledByInvoiceRegistrationNumber = cancelledByInvoiceRegistrationNumber;
            Counterpart = counterpart;
            PaymentMethods = paymentMethods;

            if (invoiceDetails.Count() == 0)
            {
                throw new ArgumentException($"Minimal count of {nameof(invoiceDetails)} is 1.");
            }
        }

        public StringIdentifier InvoiceIdentifier { get; }

        public InvoiceRegistrationNumber InvoiceRegistrationNumber { get; }

        public InvoiceRegistrationNumber CanceledByInvoiceRegistrationNumber { get; }

        public InvoiceRecordParty Issuer { get; }

        public InvoiceRecordParty Counterpart { get; }

        public InvoiceRecordHeader InvoiceHeader { get; set; }

        public IEnumerable<InvoiceRecordPaymentMethodDetails> PaymentMethods { get; set; }

        public IEnumerable<InvoiceRecordDetail> InvoiceDetails { get; }

        public InvoiceRecordSummary InvoiceSummary { get; }
    }
}
