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
            LocalCompany issuer,
            IEnumerable<Revenue> revenueItems,
            Company counterpart = null,
            IEnumerable<Payment> payments = null,
            InvoiceRegistrationNumber invoiceRegistrationNumber = null,
            InvoiceRegistrationNumber cancelledByInvoiceRegistrationNumber = null)
        {
            Header = header ?? throw new ArgumentNullException(nameof(header));
            BillType = billType;
            Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
            var revenue = revenueItems.ToList();
            RevenueItems = revenue;
            Counterpart = counterpart;
            Payments = payments;
            InvoiceRegistrationNumber = invoiceRegistrationNumber;
            CanceledByInvoiceRegistrationNumber = cancelledByInvoiceRegistrationNumber;

            if (revenue.Count == 0)
            {
                throw new ArgumentException($"Minimal count of {nameof(revenueItems)} is 1.");
            }
        }

        public InvoiceHeader Header { get; }

        public BillType BillType { get; }

        public LocalCompany Issuer { get; }

        public IEnumerable<Revenue> RevenueItems { get; }

        public Company Counterpart { get; }

        public IEnumerable<Payment> Payments { get; }

        public InvoiceRegistrationNumber InvoiceRegistrationNumber { get; }

        public InvoiceRegistrationNumber CanceledByInvoiceRegistrationNumber { get; }
    }
}
