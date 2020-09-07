using System;
using System.Collections.Generic;
using System.Text;

namespace Mews.Fiscalization.Greece.Model
{
    public class InvoiceDocument
    {
        public InvoiceDocument(IEnumerable<InvoiceRecord> invoiceRecords)
        {
            InvoiceRecords = invoiceRecords;
        }

        public IEnumerable<InvoiceRecord> InvoiceRecords { get; }
    }
}
