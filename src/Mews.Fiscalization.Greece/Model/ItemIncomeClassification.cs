using Mews.Fiscalization.Greece.Model.Types;

namespace Mews.Fiscalization.Greece.Model
{
    public class ItemIncomeClassification : ItemIncomeClassificationBase
    {
        public ItemIncomeClassification(ClassificationType classificationType, ClassificationCategory classificationCategory, Amount amount)
            : base(classificationType, classificationCategory, amount)
        {
        }
    }
}
