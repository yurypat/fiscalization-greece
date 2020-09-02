using Mews.Fiscalization.Greece.Dto.Xsd;
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
            var isSuccess = responseDoc?.Responses?.All(x => x.StatusCode == StatusCode.Success);
            if (isSuccess.HasValue)
            {
                IsSuccess = isSuccess.Value;
            }
        }

        public bool IsSuccess { get; }

        public IEnumerable<SendInvoiceResult> SendInvoiceResults { get; }
    }
}
