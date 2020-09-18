using System;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Greece.Model
{
    public class InvoiceDocument
    {
        public InvoiceDocument(IEnumerable<Invoice> invoices)
        {
            Invoices = invoices ?? throw new ArgumentNullException(nameof(invoices));

            if (invoices.Count() == 0)
            {
                throw new ArgumentException($"Minimal count of {nameof(invoices)} is 1.");
            }
        }

        public IEnumerable<Invoice> Invoices { get; }
    }
}
