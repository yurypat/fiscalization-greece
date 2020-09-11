using System;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Greece.Model
{
    public class InvoiceDocument
    {
        public InvoiceDocument(IEnumerable<InvoiceRecord> invoiceRecords)
        {
            InvoiceRecords = invoiceRecords ?? throw new ArgumentNullException(nameof(invoiceRecords));

            if (invoiceRecords.Count() == 0)
            {
                throw new ArgumentException($"Minimal count of {nameof(invoiceRecords)} is 1.");
            }
        }

        public IEnumerable<InvoiceRecord> InvoiceRecords { get; }
    }
}
