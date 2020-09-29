using Mews.Fiscalization.Greece.Model.Types;
using System.Collections.Generic;

namespace Mews.Fiscalization.Greece.Model
{
    public class RevenueItem : RevenueItemBase
    {
        public RevenueItem(Amount netValue, TaxType taxType, Amount vatValue, ClassificationType classificationType, ClassificationCategory classificationCategory, PositiveInt lineNumber = null, VatExemptionType? vatExemption = null, CityTax cityTax = null)
            : base(netValue, taxType, vatValue, new[] { new ItemIncomeClassification(classificationType, classificationCategory, netValue) }, lineNumber, vatExemption, cityTax)
        {
        }

        public RevenueItem(Amount netValue, TaxType taxType, Amount vatValue, IEnumerable<ItemIncomeClassification> incomeClassifications, PositiveInt lineNumber = null, VatExemptionType? vatExemption = null, CityTax cityTax = null)
            : base(netValue, taxType, vatValue, incomeClassifications, lineNumber, vatExemption, cityTax)
        {
        }
    }
}
