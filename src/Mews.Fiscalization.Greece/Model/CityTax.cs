using Mews.Fiscalization.Greece.Model.Types;
using System;

namespace Mews.Fiscalization.Greece.Model
{
    public class CityTax
    {
        public CityTax(CityTaxType type, NonNegativeAmount amount)
        {
            Type = type;
            Amount = amount ?? throw new ArgumentNullException(nameof(amount));
        }

        public CityTaxType Type { get; }

        public NonNegativeAmount Amount { get; }
    }
}
