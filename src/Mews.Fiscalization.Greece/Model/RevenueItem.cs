using Mews.Fiscalization.Greece.Model.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Greece.Model
{
    public class RevenueItem
    {
        public RevenueItem(Amount netValue, TaxType taxType, Amount vatValue, IEnumerable<ItemIncomeClassification> invoiceIncomeClassifications, PositiveInt lineNumber = null, VatExemption? vatExemption = null, CityTax cityTax = null)
        {
            NetValue = netValue ?? throw new ArgumentNullException(nameof(netValue));
            TaxType = taxType;
            VatValue = vatValue ?? throw new ArgumentNullException(nameof(vatValue));
            InvoiceRecordIncomeClassification = invoiceIncomeClassifications ?? throw new ArgumentNullException(nameof(invoiceIncomeClassifications));
            LineNumber = lineNumber ?? new PositiveInt(1);
            VatExemption = vatExemption;
            CityTax = cityTax;

            if (taxType == TaxType.Vat0 && !vatExemption.HasValue)
            {
                throw new ArgumentException($"{nameof(VatExemption)} must be specified when TaxType is {taxType}");
            }

            if (invoiceIncomeClassifications.Count() == 0)
            {
                throw new ArgumentException($"Minimal count of {nameof(invoiceIncomeClassifications)} is 1.");
            }
        }
        public RevenueItem(Amount netValue, TaxType taxType, Amount vatValue, ClassificationType classificationType, ClassificationCategory classificationCategory, PositiveInt lineNumber = null, VatExemption? vatExemption = null, CityTax cityTax = null)
            :this(netValue, taxType, vatValue, new[] { new ItemIncomeClassification(classificationType, classificationCategory, netValue) }, lineNumber, vatExemption, cityTax)
        {
        }

        public PositiveInt LineNumber { get; }

        public Amount NetValue { get; }

        public TaxType TaxType { get; }

        public Amount VatValue { get; }

        public VatExemption? VatExemption { get; }

        public CityTax CityTax { get; }

        public IEnumerable<ItemIncomeClassification> InvoiceRecordIncomeClassification { get; }
    }
}
