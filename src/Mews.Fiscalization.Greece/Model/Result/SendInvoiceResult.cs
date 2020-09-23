using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Greece.Model.Result
{
    public class SendInvoiceResult
    {
        public SendInvoiceResult(int lineNumber, string invoiceIdentifier, long invoiceRegistrationNumber, bool invoiceRegistrationNumberSpecified, IEnumerable<SendInvoiceError> errors)
        {
            LineNumber = lineNumber;
            Errors = errors;

            if (errors == null || errors.Count() == 0)
            {
                Success = new SendInvoiceSuccess(invoiceIdentifier, invoiceRegistrationNumber, invoiceRegistrationNumberSpecified);
            }
        }

        public int LineNumber { get; set; }

        public bool IsSuccess
        {
            get { return Success != null; }
        }

        public SendInvoiceSuccess Success { get; }

        public IEnumerable<SendInvoiceError> Errors { get; }
    }
}
