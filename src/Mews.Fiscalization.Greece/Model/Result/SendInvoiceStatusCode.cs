using System;
using System.Collections.Generic;
using System.Text;

namespace Mews.Fiscalization.Greece.Model.Result
{
    public enum SendInvoiceStatusCode
    {
        Success,
        ValidationError,
        TechnicalError,
        XmlSyntaxError
    }
}
