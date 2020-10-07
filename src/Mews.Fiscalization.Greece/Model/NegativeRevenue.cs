using Mews.Fiscalization.Greece.Model.Types;

namespace Mews.Fiscalization.Greece.Model
{
    public class NegativeRevenue : Revenue
    {
        public NegativeRevenue(NegativeAmount netValue, TaxType taxType, NegativeAmount vatValue, ClassificationType classificationType, ClassificationCategory classificationCategory, PositiveInt lineNumber = null, VatExemptionType? vatExemption = null, CityTax cityTax = null)
            : base(netValue, taxType, vatValue, new[] { new ItemIncomeClassification(classificationType, classificationCategory, netValue) }, lineNumber, vatExemption, cityTax)
        {
        }
    }
}
