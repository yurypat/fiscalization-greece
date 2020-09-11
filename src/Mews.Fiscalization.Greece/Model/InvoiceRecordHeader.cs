using Mews.Fiscalization.Greece.Model.Types;
using System;

namespace Mews.Fiscalization.Greece.Model
{
    public class InvoiceRecordHeader
    {
        public InvoiceRecordHeader(LimitedString1to50 invoiceSeries, LimitedString1to50 invoiceSerialNumber, DateTime invoiceIssueDate, BillType billType, CurrencyCode currencyCode, ExchangeRate exchangeRate)
        {
            InvoiceSeries = invoiceSeries ?? throw new ArgumentNullException(nameof(invoiceSeries));
            InvoiceSerialNumber = invoiceSerialNumber ?? throw new ArgumentNullException(nameof(invoiceSerialNumber));
            InvoiceIssueDate = invoiceIssueDate;
            BillType = billType;
            CurrencyCode = currencyCode;
            ExchangeRate = exchangeRate;
        }

        public LimitedString1to50 InvoiceSeries { get; }

        public LimitedString1to50 InvoiceSerialNumber { get; }

        public DateTime InvoiceIssueDate { get; }

        public BillType BillType { get; }

        public CurrencyCode CurrencyCode { get; }

        public ExchangeRate ExchangeRate { get; }
    }
}
