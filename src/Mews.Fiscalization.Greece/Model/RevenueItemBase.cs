using Mews.Fiscalization.Greece.Model.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Greece.Model
{
    public abstract class RevenueItemBase
    {
        public RevenueItemBase(LimitedDecimal netValue, TaxType taxType, LimitedDecimal vatValue, IEnumerable<ItemIncomeClassificationBase> incomeClassifications, PositiveInt lineNumber = null, VatExemptionType? vatExemption = null, CityTax cityTax = null)
        {
            NetValue = netValue ?? throw new ArgumentNullException(nameof(netValue));
            TaxType = taxType;
            VatValue = vatValue ?? throw new ArgumentNullException(nameof(vatValue));
            IncomeClassifications = incomeClassifications ?? throw new ArgumentNullException(nameof(incomeClassifications));
            LineNumber = lineNumber ?? new PositiveInt(1);
            VatExemption = vatExemption;
            CityTax = cityTax;

            if (taxType == TaxType.Vat0 && !vatExemption.HasValue)
            {
                throw new ArgumentException($"{nameof(VatExemption)} must be specified when TaxType is {taxType}");
            }

            if (incomeClassifications.Count() == 0)
            {
                throw new ArgumentException($"Minimal count of {nameof(incomeClassifications)} is 1.");
            }
        }

        public PositiveInt LineNumber { get; }

        public LimitedDecimal NetValue { get; }

        public TaxType TaxType { get; }

        public LimitedDecimal VatValue { get; }

        public VatExemptionType? VatExemption { get; }

        public CityTax CityTax { get; }

        public IEnumerable<ItemIncomeClassificationBase> IncomeClassifications { get; }
    }
}
