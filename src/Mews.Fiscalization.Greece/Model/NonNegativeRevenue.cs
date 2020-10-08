using Mews.Fiscalization.Greece.Model.Types;

namespace Mews.Fiscalization.Greece.Model
{
    public class NonNegativeRevenue : Revenue
    {
        public NonNegativeRevenue(
            NonNegativeAmount netValue,
            NonNegativeAmount vatValue,
            TaxType taxType,
            ClassificationType classificationType,
            ClassificationCategory classificationCategory,
            PositiveInt lineNumber = null,
            VatExemptionType? vatExemption = null)
            : base(netValue, vatValue, taxType, classificationType, classificationCategory, lineNumber, vatExemption)
        {
        }
    }
}
