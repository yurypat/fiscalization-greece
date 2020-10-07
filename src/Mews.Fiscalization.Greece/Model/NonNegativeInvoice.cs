using Mews.Fiscalization.Greece.Model.Types;
using System.Collections.Generic;

namespace Mews.Fiscalization.Greece.Model
{
    public class NonNegativeInvoice : Invoice
    {
        public NonNegativeInvoice(
            InvoiceHeader header,
            BillType billType,
            LocalCounterpart issuer,
            IEnumerable<NonNegativeRevenue> revenueItems,
            Counterpart counterpart = null,
            IEnumerable<NonNegativePayment> payments = null,
            InvoiceRegistrationNumber invoiceRegistrationNumber = null,
            InvoiceRegistrationNumber cancelledByInvoiceRegistrationNumber = null)
            : base(header, billType, issuer, revenueItems, counterpart, payments, invoiceRegistrationNumber, cancelledByInvoiceRegistrationNumber)
        {
        }
    }
}
