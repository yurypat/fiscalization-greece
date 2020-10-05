namespace Mews.Fiscalization.Greece.Model.Types
{
    public class NonNegativeAmount : LimitedDecimal
    {
        public NonNegativeAmount(decimal value)
            : base(value, minValue: 0, maxDecimalPlaces: 2)
        {
        }
    }
}
