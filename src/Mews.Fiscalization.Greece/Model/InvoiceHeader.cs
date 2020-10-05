using Mews.Fiscalization.Greece.Model.Types;
using System;

namespace Mews.Fiscalization.Greece.Model
{
    public class InvoiceHeader
    {
        public InvoiceHeader(
            LimitedString1to50 invoiceSeries,
            LimitedString1to50 invoiceSerialNumber,
            DateTime invoiceIssueDate,
            StringIdentifier invoiceIdentifier = null,
            CurrencyCode currencyCode = null,
            ExchangeRate exchangeRate = null)
        {
            InvoiceSeries = invoiceSeries ?? throw new ArgumentNullException(nameof(invoiceSeries));
            InvoiceSerialNumber = invoiceSerialNumber ?? throw new ArgumentNullException(nameof(invoiceSerialNumber));
            InvoiceIssueDate = invoiceIssueDate;
            InvoiceIdentifier = invoiceIdentifier;
            CurrencyCode = currencyCode;
            ExchangeRate = exchangeRate;
        }

        public LimitedString1to50 InvoiceSeries { get; }

        public LimitedString1to50 InvoiceSerialNumber { get; }

        public DateTime InvoiceIssueDate { get; }

        public StringIdentifier InvoiceIdentifier { get; }

        public CurrencyCode CurrencyCode { get; }

        public ExchangeRate ExchangeRate { get; }
    }
}
