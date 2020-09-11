namespace Mews.Fiscalization.Greece.Model.Types
{
    public class Amount : LimitedDecimal
    {
        public Amount(decimal value)
            : base(value, minValue: 0, maxDecimalPlaces: 2)
        {
        }
    }
}
