using Mews.Fiscalization.Greece.Model.Types;
using System.Collections.Generic;

namespace Mews.Fiscalization.Greece.Model
{
    public class PositiveRevenue : Revenue
    {
        public PositiveRevenue(PositiveAmount netValue, TaxType taxType, PositiveAmount vatValue, ClassificationType classificationType, ClassificationCategory classificationCategory, PositiveInt lineNumber = null, VatExemptionType? vatExemption = null, CityTax cityTax = null)
            : base(netValue, taxType, vatValue, new[] { new ItemIncomeClassification(classificationType, classificationCategory, netValue) }, lineNumber, vatExemption, cityTax)
        {
        }

        public PositiveRevenue(PositiveAmount netValue, TaxType taxType, PositiveAmount vatValue, IEnumerable<ItemIncomeClassification> incomeClassifications, PositiveInt lineNumber = null, VatExemptionType? vatExemption = null, CityTax cityTax = null)
            : base(netValue, taxType, vatValue, incomeClassifications, lineNumber, vatExemption, cityTax)
        {
        }
    }
}
