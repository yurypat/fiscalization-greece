using System;
using System.Collections.Generic;
using System.Text;

namespace Mews.Fiscalization.Greece.Model.Result
{
    public class SendInvoiceResult
    {
        public SendInvoiceResult(SendInvoiceStatusCode statusCode, string invoiceIdentifier, long? invoiceRegistrationNumber, IEnumerable<SendInvoiceError> errors)
        {
            StatusCode = statusCode;
            InvoiceIdentifier = invoiceIdentifier;
            InvoiceRegistrationNumber = invoiceRegistrationNumber;
            Errors = errors;
        }

        public SendInvoiceStatusCode StatusCode { get; }

        public string InvoiceIdentifier { get; }

        public long? InvoiceRegistrationNumber { get; }

        public IEnumerable<SendInvoiceError> Errors { get; }
    }
}
