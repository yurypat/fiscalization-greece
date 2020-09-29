using Mews.Fiscalization.Greece.Model.Types;
using System.Collections.Generic;

namespace Mews.Fiscalization.Greece.Model
{
    public class NegativeRevenueItem : RevenueItemBase
    {
        public NegativeRevenueItem(NegativeAmount netValue, TaxType taxType, NegativeAmount vatValue, ClassificationType classificationType, ClassificationCategory classificationCategory, PositiveInt lineNumber = null, VatExemptionType? vatExemption = null, CityTax cityTax = null)
            : base(netValue, taxType, vatValue, new[] { new NegativeItemIncomeClassification(classificationType, classificationCategory, netValue) }, lineNumber, vatExemption, cityTax)
        {
        }

        public NegativeRevenueItem(NegativeAmount netValue, TaxType taxType, NegativeAmount vatValue, IEnumerable<NegativeItemIncomeClassification> incomeClassifications, PositiveInt lineNumber = null, VatExemptionType? vatExemption = null, CityTax cityTax = null)
            : base(netValue, taxType, vatValue, incomeClassifications, lineNumber, vatExemption, cityTax)
        {
        }
    }
}
