using Mews.Fiscalization.Greece.Model.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Greece.Model
{
    public class InvoiceRecordSummary
    {
        public InvoiceRecordSummary(Amount totalNetValue, Amount totalVatAmount, Amount totalGrossValue, IEnumerable<InvoiceRecordIncomeClassification> invoiceRecordIncomeClassification)
        {
            TotalNetValue = totalNetValue ?? throw new ArgumentNullException(nameof(totalNetValue));
            TotalVatAmount = totalVatAmount ?? throw new ArgumentNullException(nameof(totalVatAmount));
            TotalGrossValue = totalGrossValue ?? throw new ArgumentNullException(nameof(totalGrossValue));
            InvoiceRecordIncomeClassification = invoiceRecordIncomeClassification ?? throw new ArgumentNullException(nameof(invoiceRecordIncomeClassification));

            if (invoiceRecordIncomeClassification.Count() == 0)
            {
                throw new ArgumentException($"Minimal count of {nameof(invoiceRecordIncomeClassification)} is 1.");
            }
        }

        public Amount TotalNetValue { get; }

        public Amount TotalVatAmount { get; }

        public Amount TotalGrossValue { get; }

        public IEnumerable<InvoiceRecordIncomeClassification> InvoiceRecordIncomeClassification { get; }
    }
}
