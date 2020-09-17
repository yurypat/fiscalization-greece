using Mews.Fiscalization.Greece.Model.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Greece.Model
{
    public class InvoiceSummary
    {
        public InvoiceSummary(Amount totalNetValue, Amount totalVatAmount, Amount totalGrossValue, IEnumerable<ItemIncomeClassification> invoiceRecordIncomeClassification, Amount totalOtherTaxesAmount = null)
        {
            TotalNetValue = totalNetValue ?? throw new ArgumentNullException(nameof(totalNetValue));
            TotalVatValue = totalVatAmount ?? throw new ArgumentNullException(nameof(totalVatAmount));
            TotalGrossValue = totalGrossValue ?? throw new ArgumentNullException(nameof(totalGrossValue));
            TotalOtherTaxesAmount = totalOtherTaxesAmount;
            InvoiceRecordIncomeClassification = invoiceRecordIncomeClassification ?? throw new ArgumentNullException(nameof(invoiceRecordIncomeClassification));

            if (invoiceRecordIncomeClassification.Count() == 0)
            {
                throw new ArgumentException($"Minimal count of {nameof(invoiceRecordIncomeClassification)} is 1.");
            }
        }

        public Amount TotalNetValue { get; }

        public Amount TotalVatValue { get; }

        public Amount TotalGrossValue { get; }

        public Amount TotalOtherTaxesAmount { get; }

        public IEnumerable<ItemIncomeClassification> InvoiceRecordIncomeClassification { get; }
    }
}
