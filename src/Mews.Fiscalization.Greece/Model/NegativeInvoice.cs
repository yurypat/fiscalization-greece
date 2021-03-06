﻿using Mews.Fiscalization.Greece.Model.Types;
using System.Collections.Generic;

namespace Mews.Fiscalization.Greece.Model
{
    public class NegativeInvoice : Invoice
    {
        public NegativeInvoice(
            LocalCompany issuer,
            NegativeInvoiceHeader header,
            IEnumerable<NegativeRevenue> revenueItems,
            StringIdentifier invoiceIdentifier = null,
            InvoiceRegistrationNumber invoiceRegistrationNumber = null,
            InvoiceRegistrationNumber cancelledByInvoiceRegistrationNumber = null,
            ForeignCompany counterpart = null,
            IEnumerable<NegativePayment> payments = null)
            : base(issuer, header, revenueItems, invoiceIdentifier, invoiceRegistrationNumber, cancelledByInvoiceRegistrationNumber, counterpart, payments)
        {
        }
    }
}
