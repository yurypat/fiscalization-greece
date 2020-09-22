using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Greece.Model.Result
{
    public class SendInvoiceResult
    {
        public SendInvoiceResult(int lineNumber, string invoiceIdentifier, long invoiceRegistrationNumber, bool invoiceRegistrationNumberSpecified, IEnumerable<Error> errors)
        {
            LineNumber = lineNumber;

            if (errors == null || errors.Count() == 0)
            {
                Success = new SendInvoiceSuccess(invoiceIdentifier, invoiceRegistrationNumber, invoiceRegistrationNumberSpecified);
            }
            else
            {
                Error = new SendInvoiceError(errors.First());
            }
        }

        public int LineNumber { get; set; }

        public bool IsSuccess
        {
            get { return Success != null; }
        }

        public SendInvoiceSuccess Success { get; }

        public SendInvoiceError Error { get; }
    }
}
