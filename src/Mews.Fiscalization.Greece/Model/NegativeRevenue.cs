using Mews.Fiscalization.Greece.Model.Types;
using System.Collections.Generic;

namespace Mews.Fiscalization.Greece.Model
{
    public class NegativeRevenue : Revenue
    {
        public NegativeRevenue(NegativeAmount netValue, TaxType taxType, NegativeAmount vatValue, ClassificationType classificationType, ClassificationCategory classificationCategory, PositiveInt lineNumber = null, VatExemptionType? vatExemption = null, CityTax cityTax = null)
            : base(netValue, taxType, vatValue, new[] { new ItemIncomeClassification(classificationType, classificationCategory, netValue) }, lineNumber, vatExemption, cityTax)
        {
        }

        public NegativeRevenue(NegativeAmount netValue, TaxType taxType, NegativeAmount vatValue, IEnumerable<ItemIncomeClassification> incomeClassifications, PositiveInt lineNumber = null, VatExemptionType? vatExemption = null, CityTax cityTax = null)
            : base(netValue, taxType, vatValue, incomeClassifications, lineNumber, vatExemption, cityTax)
        {
        }
    }
}
