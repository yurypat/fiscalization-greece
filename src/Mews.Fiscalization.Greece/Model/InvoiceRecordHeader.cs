using System;
using System.Collections.Generic;
using System.Text;

namespace Mews.Fiscalization.Greece.Model
{
    public class InvoiceRecordHeader
    {
        public InvoiceRecordHeader(string invoiceSeries, string invoiceSerialNumber, DateTime invoiceIssueDate, bool vatPaymentSuspension, decimal? exchangeRate, 
            long? correlatedInvoices, bool selfPricing, DateTime? dispatchDate, DateTime? dispatchTime, string vehicleNumber)
        {
            InvoiceSeries = invoiceSeries;
            InvoiceSerialNumber = invoiceSerialNumber;
            InvoiceIssueDate = invoiceIssueDate;
            VatPaymentSuspension = vatPaymentSuspension;
            ExchangeRate = exchangeRate;
            CorrelatedInvoices = correlatedInvoices;
            SelfPricing = selfPricing;
            DispatchDate = dispatchDate;
            DispatchTime = dispatchTime;
            VehicleNumber = vehicleNumber;
        }

        public string InvoiceSeries { get; }

        public string InvoiceSerialNumber { get; }

        public DateTime InvoiceIssueDate { get; }

        public bool VatPaymentSuspension { get; }

        public decimal? ExchangeRate { get; }

        public long? CorrelatedInvoices { get; }

        public bool SelfPricing { get; }

        public DateTime? DispatchDate { get; }

        public DateTime? DispatchTime { get; }

        public string VehicleNumber { get; }
    }
}
