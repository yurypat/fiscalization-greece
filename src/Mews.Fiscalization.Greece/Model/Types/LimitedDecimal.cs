using Mews.Fiscalization.Greece.Extensions;
using System;

namespace Mews.Fiscalization.Greece.Model.Types
{
    public abstract class LimitedDecimal : Identifier<decimal>
    {
        public LimitedDecimal(decimal value, int maxDecimalPlaces, decimal? minValue = null, decimal? maxValue = null)
            : base(value)
        {
            var isValidAmount = IsValid(value, maxDecimalPlaces, minValue, maxValue);
            if (!isValidAmount)
            {
                throw new ArgumentException($"{nameof(value)} is not valid decimal.");
            }
        }

        public static bool IsValid(decimal value, int maxDecimalPlaces, decimal? minValue = null, decimal? maxValue = null)
        {
            return ((minValue != null && value >= minValue) || (maxValue != null && value <= maxValue)) && value.GetDecimalPlaces() <= maxDecimalPlaces;
        }
    }
}
