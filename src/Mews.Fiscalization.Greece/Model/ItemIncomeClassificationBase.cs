using Mews.Fiscalization.Greece.Model.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mews.Fiscalization.Greece.Model
{
    public abstract class ItemIncomeClassificationBase
    {
        public ItemIncomeClassificationBase(ClassificationType classificationType, ClassificationCategory classificationCategory, LimitedDecimal amount)
        {
            ClassificationType = classificationType;
            ClassificationCategory = classificationCategory;
            Amount = amount ?? throw new ArgumentNullException(nameof(Amount));
        }

        public ClassificationType ClassificationType { get; }

        public ClassificationCategory ClassificationCategory { get; }

        public LimitedDecimal Amount { get; }
    }
}
