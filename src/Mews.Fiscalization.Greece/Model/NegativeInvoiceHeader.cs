using Mews.Fiscalization.Greece.Model.Types;
using System;

namespace Mews.Fiscalization.Greece.Model
{
    public class NegativeInvoiceHeader : InvoiceHeaderBase
    {
        public NegativeInvoiceHeader(LimitedString1to50 invoiceSeries, LimitedString1to50 invoiceSerialNumber, DateTime invoiceIssueDate, CurrencyCode currencyCode = null, ExchangeRate exchangeRate = null)
            : base(invoiceSeries, invoiceSerialNumber, invoiceIssueDate, BillType.CreditInvoice, currencyCode, exchangeRate)
        {
        }
    }
}
