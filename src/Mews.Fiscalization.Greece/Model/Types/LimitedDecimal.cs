using Mews.Fiscalization.Greece.Extensions;
using System;

namespace Mews.Fiscalization.Greece.Model.Types
{
    public class LimitedDecimal : Identifier<decimal>
    {
        public LimitedDecimal(decimal value, decimal minValue, int maxDecimalPlaces)
            : base(value)
        {
            var isValidAmount = value >= minValue && value.GetDecimalPlaces() <= maxDecimalPlaces;
            if (!isValidAmount)
            {
                throw new ArgumentException($"{nameof(value)} is not valid decimal.");
            }
        }
    }
}
