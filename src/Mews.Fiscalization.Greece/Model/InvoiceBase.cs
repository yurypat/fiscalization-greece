using Mews.Fiscalization.Greece.Model.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Greece.Model
{
    public abstract class InvoiceBase
    {
        public InvoiceBase(
            LocalCompany issuer,
            InvoiceHeaderBase header,
            IEnumerable<RevenueItemBase> revenueItems,
            StringIdentifier invoiceIdentifier = null,
            InvoiceRegistrationNumber invoiceRegistrationNumber = null,
            InvoiceRegistrationNumber cancelledByInvoiceRegistrationNumber = null,
            ForeignCompany counterpart = null,
            IEnumerable<PaymentBase> payments = null)
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
        public InvoiceHeaderBase Header { get; set; }

        public StringIdentifier InvoiceIdentifier { get; }

        public InvoiceRegistrationNumber InvoiceRegistrationNumber { get; }

        public InvoiceRegistrationNumber CanceledByInvoiceRegistrationNumber { get; }

        public LocalCompany Issuer { get; }

        public ForeignCompany Counterpart { get; }

        public IEnumerable<RevenueItemBase> RevenueItems { get; }

        public IEnumerable<PaymentBase> Payments { get; set; }
    }
}
