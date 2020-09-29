using System;

namespace Mews.Fiscalization.Greece.Model.Types
{
    public class NegativeAmount : LimitedDecimal
    {
        public NegativeAmount(decimal value)
            : base(value, maxValue: 0, maxDecimalPlaces: 2)
        {
        }

        public override decimal Value { get => Math.Abs(base.Value); }
    }
}

