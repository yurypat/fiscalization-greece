﻿using Mews.Fiscalization.Greece.Model.Types;
using System;

namespace Mews.Fiscalization.Greece.Model
{
    public class PositiveInvoiceHeader : InvoiceHeader
    {
        public PositiveInvoiceHeader(LimitedString1to50 invoiceSeries, LimitedString1to50 invoiceSerialNumber, DateTime invoiceIssueDate, BillType billType, CurrencyCode currencyCode = null, ExchangeRate exchangeRate = null)
            : base(invoiceSeries, invoiceSerialNumber, invoiceIssueDate, billType, currencyCode, exchangeRate)
        {
        }
    }
}
