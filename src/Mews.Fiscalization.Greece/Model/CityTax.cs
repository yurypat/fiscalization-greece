using Mews.Fiscalization.Greece.Model.Types;
using System;

namespace Mews.Fiscalization.Greece.Model
{
    public class CityTax
    {
        public CityTax(CityTaxType type, Amount amount)
        {
            Type = type;
            Amount = amount ?? throw new ArgumentNullException(nameof(amount));
        }

        public CityTaxType Type { get; }

        public Amount Amount { get; }
    }
}
