using Mews.Fiscalization.Greece.Dto.Xsd;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Mews.Fiscalization.Greece.Dto
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

        public bool IsSuccess
        {
            get;
        }
    }
}
