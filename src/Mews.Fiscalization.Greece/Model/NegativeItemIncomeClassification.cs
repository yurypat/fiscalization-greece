using Mews.Fiscalization.Greece.Model.Types;

namespace Mews.Fiscalization.Greece.Model
{
    public class NegativeItemIncomeClassification : ItemIncomeClassificationBase
    {
        public NegativeItemIncomeClassification(ClassificationType classificationType, ClassificationCategory classificationCategory, NegativeAmount amount)
            : base(classificationType, classificationCategory, amount)
        {
        }
    }
}
