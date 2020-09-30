using Mews.Fiscalization.Greece.Model.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Greece.Model
{
    public class PositiveInvoice : Invoice
    {
        public PositiveInvoice(
            LocalCompany issuer,
            PositiveInvoiceHeader header,
            IEnumerable<PositiveRevenue> revenueItems,
            StringIdentifier invoiceIdentifier = null,
            InvoiceRegistrationNumber invoiceRegistrationNumber = null,
            InvoiceRegistrationNumber cancelledByInvoiceRegistrationNumber = null,
            ForeignCompany counterpart = null,
            IEnumerable<PositivePayment> payments = null)
            : base(issuer, header, revenueItems, invoiceIdentifier, invoiceRegistrationNumber, cancelledByInvoiceRegistrationNumber, counterpart, payments)
        {
        }
    }
}
