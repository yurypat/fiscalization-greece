using System;
using System.Collections.Generic;
using System.Text;

namespace Mews.Fiscalization.Greece.Dto
{
    public class InvoiceRecordDetails
    {
        public InvoiceRecordDetails(int lineNumber, decimal netValue, decimal vatAmount)
        {
            LineNumber = lineNumber;
            NetValue = netValue;
            VatAmount = vatAmount;
        }

        public int LineNumber { get; }

        public decimal NetValue { get; }

        public decimal VatAmount { get; }
    }
}
