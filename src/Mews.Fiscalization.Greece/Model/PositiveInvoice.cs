using Mews.Fiscalization.Greece.Model.Types;
using System.Collections.Generic;

namespace Mews.Fiscalization.Greece.Model
{
    public class PositiveInvoice : Invoice
    {
        public PositiveInvoice(
            InvoiceHeader header,
            BillType billType,
            LocalCompany issuer,
            IEnumerable<PositiveRevenue> revenueItems,
            Company counterpart = null,
            IEnumerable<PositivePayment> payments = null,
            InvoiceRegistrationNumber invoiceRegistrationNumber = null,
            InvoiceRegistrationNumber cancelledByInvoiceRegistrationNumber = null)
            : base(header, billType, issuer, revenueItems, counterpart, payments, invoiceRegistrationNumber, cancelledByInvoiceRegistrationNumber)
        {
        }
    }
}
