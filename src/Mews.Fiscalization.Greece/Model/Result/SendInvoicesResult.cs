using Mews.Fiscalization.Greece.Dto.Xsd;
using Mews.Fiscalization.Greece.Extensions;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Mews.Fiscalization.Greece.Model.Result
{
    public class SendInvoicesResult
    {
        internal SendInvoicesResult(ResponseDoc responseDoc)
        {
            SendInvoiceResults = responseDoc.Responses.Select(response => new SendInvoiceResult(
                lineNumber: response.Index,
                invoiceIdentifier: response.InvoiceUid,
                invoiceRegistrationNumber: response.InvoiceMark,
                invoiceRegistrationNumberSpecified: response.InvoiceMarkSpecified,
                errors: response.Errors?.Select(error => new Error(MapErrorCode(error.Code, response.StatusCode), error.Message))));
        }

        public IEnumerable<SendInvoiceResult> SendInvoiceResults { get; }

        private string MapErrorCode(string errorCode, StatusCode statusCode)
        {
            // Error codes which are returned from API have some integer value that describes particular error. But we need only category of the error
            // so we return value of the status code.
            if (int.TryParse(errorCode, out _))
            {
                return statusCode.ToString();
            }

            return errorCode;
        }
    }
}
