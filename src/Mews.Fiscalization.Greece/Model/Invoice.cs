using Mews.Fiscalization.Greece.Model.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Greece.Model
{
    public abstract class Invoice
    {
        public Invoice(
            LocalCompany issuer,
            InvoiceHeader header,
            IEnumerable<Revenue> revenueItems,
            StringIdentifier invoiceIdentifier = null,
            InvoiceRegistrationNumber invoiceRegistrationNumber = null,
            InvoiceRegistrationNumber cancelledByInvoiceRegistrationNumber = null,
            ForeignCompany counterpart = null,
            IEnumerable<Payment> payments = null)
        {
            Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
            Header = header ?? throw new ArgumentNullException(nameof(header));
            InvoiceIdentifier = invoiceIdentifier;
            InvoiceRegistrationNumber = invoiceRegistrationNumber;
            CanceledByInvoiceRegistrationNumber = cancelledByInvoiceRegistrationNumber;
            Counterpart = counterpart;
            RevenueItems = revenueItems;
            Payments = payments;

            if (revenueItems.Count() == 0)
            {
                throw new ArgumentException($"Minimal count of {nameof(revenueItems)} is 1.");
            }
        }
        public InvoiceHeader Header { get; set; }

        public StringIdentifier InvoiceIdentifier { get; }

        public InvoiceRegistrationNumber InvoiceRegistrationNumber { get; }

        public InvoiceRegistrationNumber CanceledByInvoiceRegistrationNumber { get; }

        public LocalCompany Issuer { get; }

        public ForeignCompany Counterpart { get; }

        public IEnumerable<Revenue> RevenueItems { get; }

        public IEnumerable<Payment> Payments { get; set; }
    }
}
