using System;

namespace Mews.Fiscalization.Greece.Model.Types
{
    public class NegativeAmount : Amount
    {
        public NegativeAmount(decimal value)
            : base(value, maxValue: 0)
        {
        }

        public override decimal Value { get => Math.Abs(base.Value); }
    }
}

