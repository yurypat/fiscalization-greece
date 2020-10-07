using Mews.Fiscalization.Greece.Model.Types;
using System.Collections.Generic;

namespace Mews.Fiscalization.Greece.Model
{
    public class NegativeInvoice : Invoice
    {
        public NegativeInvoice(
            InvoiceHeader header,
            LocalCompany issuer,
            IEnumerable<NegativeRevenue> revenueItems,
            Company counterpart = null,
            IEnumerable<NegativePayment> payments = null,
            InvoiceRegistrationNumber invoiceRegistrationNumber = null,
            InvoiceRegistrationNumber cancelledByInvoiceRegistrationNumber = null,
            InvoiceRegistrationNumber correlatedInvoice = null)
            : base(header, BillType.CreditInvoice, issuer, revenueItems, counterpart, payments, invoiceRegistrationNumber, cancelledByInvoiceRegistrationNumber, correlatedInvoice)
        {
        }
    }
}
