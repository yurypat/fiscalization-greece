using Mews.Fiscalization.Greece.Dto.Xsd;
using Mews.Fiscalization.Greece.Extensions;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Mews.Fiscalization.Greece.Model.Result
{
    public class SendInvoicesResult
    {
        internal SendInvoicesResult(ResponseDoc responseDoc)
        {
            var sendInvoiceResults = new List<SendInvoiceResult>();

            foreach(var response in responseDoc.Responses)
            {
                sendInvoiceResults.Add(new SendInvoiceResult(response.StatusCode.ConvertToEnum<SendInvoiceStatusCode>(), response.InvoiceUid, response.InvoiceMark, 
                    response.InvoiceMarkSpecified, GetErrors(response)));
            }

            SendInvoiceResults = sendInvoiceResults;
        }

        public IEnumerable<SendInvoiceResult> SendInvoiceResults { get; }

        private IEnumerable<SendInvoiceError> GetErrors(Response response)
        {
            if (response.Errors != null)
            {
                var result = new List<SendInvoiceError>();

                foreach (var responseError in response.Errors)
                {
                    result.Add(new SendInvoiceError(responseError.Code, responseError.Message));
                }

                return result;
            }

            return null;
        }
    }
}
