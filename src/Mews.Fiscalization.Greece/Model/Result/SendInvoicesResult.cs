using Mews.Fiscalization.Greece.Dto.Xsd;
using Mews.Fiscalization.Greece.Extensions;
using System.Linq;
using System.Collections.Generic;

namespace Mews.Fiscalization.Greece.Model.Result
{
    public class SendInvoicesResult
    {
        internal SendInvoicesResult(ResponseDoc responseDoc)
        {
            SendInvoiceResults = responseDoc.Responses.Select(response => new SendInvoiceResult(
                statusCode: response.StatusCode.ConvertToEnum<SendInvoiceStatusCode>(),
                invoiceIdentifier: response.InvoiceUid,
                invoiceRegistrationNumber: response.InvoiceMark,
                invoiceRegistrationNumberSpecified: response.InvoiceMarkSpecified,
                errors: response.Errors?.Select(error => new SendInvoiceError(error.Code, error.Message))));
        }

        public IEnumerable<SendInvoiceResult> SendInvoiceResults { get; }
    }
}
