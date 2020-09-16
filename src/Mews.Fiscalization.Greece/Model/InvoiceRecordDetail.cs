using Mews.Fiscalization.Greece.Model.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Greece.Model
{
    public class InvoiceRecordDetail
    {
        public InvoiceRecordDetail(Amount netValue, TaxType taxType, Amount vatValue, IEnumerable<InvoiceRecordIncomeClassification> invoiceRecordIncomeClassification, PositiveInt lineNumber = null)
        {
            NetValue = netValue ?? throw new ArgumentNullException(nameof(netValue));
            TaxType = taxType;
            VatValue = vatValue ?? throw new ArgumentNullException(nameof(vatValue));
            InvoiceRecordIncomeClassification = invoiceRecordIncomeClassification ?? throw new ArgumentNullException(nameof(invoiceRecordIncomeClassification));
            LineNumber = lineNumber ?? new PositiveInt(1);

            if (invoiceRecordIncomeClassification.Count() == 0)
            {
                throw new ArgumentException($"Minimal count of {nameof(invoiceRecordIncomeClassification)} is 1.");
            }
        }
        public InvoiceRecordDetail(Amount netValue, TaxType taxType, Amount vatValue, ClassificationType classificationType, ClassificationCategory classificationCategory, PositiveInt lineNumber = null)
            :this(netValue, taxType, vatValue, new[] { new InvoiceRecordIncomeClassification(classificationType, classificationCategory, netValue) }, lineNumber)
        {
        }

        public PositiveInt LineNumber { get; }

        public Amount NetValue { get; }

        public TaxType TaxType { get; }

        public Amount VatValue { get; }

        public IEnumerable<InvoiceRecordIncomeClassification> InvoiceRecordIncomeClassification { get; }
    }
}
