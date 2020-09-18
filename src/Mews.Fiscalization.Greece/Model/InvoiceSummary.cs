using Mews.Fiscalization.Greece.Model.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Greece.Model
{
    public class InvoiceSummary
    {
        public InvoiceSummary(Amount net, Amount vat, Amount gross, IEnumerable<ItemIncomeClassification> invoiceIncomeClassifications, Amount otherTaxes = null)
        {
            TotalNetValue = net ?? throw new ArgumentNullException(nameof(net));
            TotalVatValue = vat ?? throw new ArgumentNullException(nameof(vat));
            TotalGrossValue = gross ?? throw new ArgumentNullException(nameof(gross));
            TotalOtherTaxesAmount = otherTaxes;
            InvoiceIncomeClassifications = invoiceIncomeClassifications ?? throw new ArgumentNullException(nameof(invoiceIncomeClassifications));

            if (invoiceIncomeClassifications.Count() == 0)
            {
                throw new ArgumentException($"Minimal count of {nameof(invoiceIncomeClassifications)} is 1.");
            }
        }

        public Amount TotalNetValue { get; }

        public Amount TotalVatValue { get; }

        public Amount TotalGrossValue { get; }

        public Amount TotalOtherTaxesAmount { get; }

        public IEnumerable<ItemIncomeClassification> InvoiceIncomeClassifications { get; }
    }
}
