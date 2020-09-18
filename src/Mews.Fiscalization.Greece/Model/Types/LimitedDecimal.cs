using Mews.Fiscalization.Greece.Extensions;
using System;

namespace Mews.Fiscalization.Greece.Model.Types
{
    public abstract class LimitedDecimal : Identifier<decimal>
    {
        public LimitedDecimal(decimal value, decimal minValue, int maxDecimalPlaces)
            : base(value)
        {
            var isValidAmount = IsValid(value, minValue, maxDecimalPlaces);
            if (!isValidAmount)
            {
                throw new ArgumentException($"{nameof(value)} is not valid decimal.");
            }
        }

        public static bool IsValid(decimal value, decimal minValue, int maxDecimalPlaces)
        {
            return value >= minValue && value.GetDecimalPlaces() <= maxDecimalPlaces;
        }
    }
}
