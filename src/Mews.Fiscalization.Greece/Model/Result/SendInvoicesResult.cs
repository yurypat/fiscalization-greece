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
                statusCode: MapSendInvoiceStatusCode(response.StatusCode),
                invoiceIdentifier: response.InvoiceUid,
                invoiceRegistrationNumber: response.InvoiceMark,
                invoiceRegistrationNumberSpecified: response.InvoiceMarkSpecified,
                errors: response.Errors?.Select(error => new SendInvoiceError(error.Code, error.Message))));
        }

        public IEnumerable<SendInvoiceResult> SendInvoiceResults { get; }

        private SendInvoiceStatusCode MapSendInvoiceStatusCode(StatusCode statusCode)
        {
            switch(statusCode)
            {
                case StatusCode.Success:
                    return SendInvoiceStatusCode.Success;
                case StatusCode.TechnicalError:
                    return SendInvoiceStatusCode.TechnicalError;
                case StatusCode.ValidationError:
                    return SendInvoiceStatusCode.ValidationError;
                case StatusCode.XmlSyntaxError:
                    return SendInvoiceStatusCode.XmlSyntaxError;
                default:
                    throw new ArgumentException($"Cannot map StatusCode {statusCode} to SendInvoiceStatusCode.");
            }
        }
    }
}
