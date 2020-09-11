using Mews.Fiscalization.Greece.Model.Types;
using System;

namespace Mews.Fiscalization.Greece.Model
{
    public class InvoiceRecordIncomeClassification
    {
        public InvoiceRecordIncomeClassification(ClassificationType classificationType, ClassificationCategory classificationCategory, Amount amount)
        {
            ClassificationType = classificationType;
            ClassificationCategory = classificationCategory;
            Amount = amount ?? throw new ArgumentNullException(nameof(Amount));
        }

        public ClassificationType ClassificationType { get; }

        public ClassificationCategory ClassificationCategory { get; }

        public Amount Amount { get; }
    }
}
