using Mews.Fiscalization.Greece.Model.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Greece.Model
{
    public class Invoice
    {
        public Invoice(
            InvoiceParty issuer,
            InvoiceHeader header,
            IEnumerable<RevenueItem> revenueItems,
            InvoiceSummary summary,
            StringIdentifier invoiceIdentifier = null,
            InvoiceRegistrationNumber invoiceRegistrationNumber = null,
            InvoiceRegistrationNumber cancelledByInvoiceRegistrationNumber = null,
            InvoiceParty counterpart = null,
            IEnumerable<Payment> payments = null)
        {
            Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
            Header = header ?? throw new ArgumentNullException(nameof(header));
            RevenueItems = revenueItems ?? throw new ArgumentNullException(nameof(revenueItems));
            Summary = summary ?? throw new ArgumentNullException(nameof(summary));
            InvoiceIdentifier = invoiceIdentifier;
            InvoiceRegistrationNumber = invoiceRegistrationNumber;
            CanceledByInvoiceRegistrationNumber = cancelledByInvoiceRegistrationNumber;
            Counterpart = counterpart;
            Payments = payments;

            if (revenueItems.Count() == 0)
            {
                throw new ArgumentException($"Minimal count of {nameof(revenueItems)} is 1.");
            }
        }

        public StringIdentifier InvoiceIdentifier { get; }

        public InvoiceRegistrationNumber InvoiceRegistrationNumber { get; }

        public InvoiceRegistrationNumber CanceledByInvoiceRegistrationNumber { get; }

        public InvoiceParty Issuer { get; }

        public InvoiceParty Counterpart { get; }

        public InvoiceHeader Header { get; set; }

        public IEnumerable<Payment> Payments { get; set; }

        public IEnumerable<RevenueItem> RevenueItems { get; }

        public InvoiceSummary Summary { get; }
    }
}
