using System;

namespace Mews.Fiscalization.Greece.Model.Types
{
    public abstract class LimitedInt : Identifier<int>
    {
        public LimitedInt(int value, int minValue, int? maxValue = null)
            : base(value)
        {
            if (maxValue != null && value > maxValue.Value)
            {
                throw new ArgumentException($"Max value of int is {maxValue.Value}.");
            }

            if (value < minValue)
            {
                throw new ArgumentException($"Min value of int is {minValue}.");
            }
        }

        public static bool IsValid(int value, int minValue, int? maxValue = null)
        {
            return value >= minValue && (maxValue == null || value <= maxValue.Value);
        }
    }
}
