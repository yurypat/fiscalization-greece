using System;
using Mews.Fiscalization.Greece.Model.Types;
using System.Collections.Generic;

namespace Mews.Fiscalization.Greece.Model
{
    public class CreditInvoice : Invoice
    {
        public CreditInvoice(
            InvoiceHeader header,
            LocalCounterpart issuer,
            IEnumerable<NegativeRevenue> revenueItems,
            Counterpart counterpart,
            IEnumerable<NegativePayment> payments = null,
            InvoiceRegistrationNumber invoiceRegistrationNumber = null,
            InvoiceRegistrationNumber cancelledByInvoiceRegistrationNumber = null,
            InvoiceRegistrationNumber correlatedInvoice = null)
            : base(header, issuer, revenueItems, counterpart, payments, invoiceRegistrationNumber, cancelledByInvoiceRegistrationNumber, correlatedInvoice)
        {
            if (counterpart == null)
            {
                throw new ArgumentNullException(nameof(counterpart));
            }
        }
    }
}
