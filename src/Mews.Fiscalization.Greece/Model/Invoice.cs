using Mews.Fiscalization.Greece.Model.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Greece.Model
{
    public abstract class Invoice
    {
        public Invoice(
            InvoiceHeader header,
            BillType billType,
            LocalCounterpart issuer,
            IEnumerable<Revenue> revenueItems,
            Counterpart counterpart = null,
            IEnumerable<Payment> payments = null,
            InvoiceRegistrationNumber invoiceRegistrationNumber = null,
            InvoiceRegistrationNumber cancelledByInvoiceRegistrationNumber = null)
        {
            Header = header ?? throw new ArgumentNullException(nameof(header));
            BillType = billType;
            Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
            RevenueItems = revenueItems.ToList();
            Counterpart = counterpart;
            Payments = payments;
            InvoiceRegistrationNumber = invoiceRegistrationNumber;
            CanceledByInvoiceRegistrationNumber = cancelledByInvoiceRegistrationNumber;

            if (!RevenueItems.Any())
            {
                throw new ArgumentException($"Minimal count of {nameof(revenueItems)} is 1.");
            }
        }

        public InvoiceHeader Header { get; }

        public BillType BillType { get; }

        public LocalCounterpart Issuer { get; }

        public IReadOnlyList<Revenue> RevenueItems { get; }

        public Counterpart Counterpart { get; }

        public IEnumerable<Payment> Payments { get; }

        public InvoiceRegistrationNumber InvoiceRegistrationNumber { get; }

        public InvoiceRegistrationNumber CanceledByInvoiceRegistrationNumber { get; }
    }
}
