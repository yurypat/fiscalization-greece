using Mews.Fiscalization.Greece.Model.Types;
using System;

namespace Mews.Fiscalization.Greece.Model
{
    public class NegativeInvoiceHeader : InvoiceHeader
    {
        public NegativeInvoiceHeader(LimitedString1to50 invoiceSeries, LimitedString1to50 invoiceSerialNumber, DateTime invoiceIssueDate, BillType billType = BillType.CreditInvoice, InvoiceRegistrationNumber correlatedInvoice = null, CurrencyCode currencyCode = null, ExchangeRate exchangeRate = null)
            : base(invoiceSeries, invoiceSerialNumber, invoiceIssueDate, billType, correlatedInvoice, currencyCode, exchangeRate)
        {
        }
    }
}
