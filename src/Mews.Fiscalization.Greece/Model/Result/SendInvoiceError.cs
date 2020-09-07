using System;
using System.Collections.Generic;
using System.Text;

namespace Mews.Fiscalization.Greece.Model.Result
{
    public class SendInvoiceError
    {
        public SendInvoiceError(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public string Code { get; }

        public string Message { get; }
    }
}
