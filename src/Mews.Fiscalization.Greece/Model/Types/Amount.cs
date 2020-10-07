namespace Mews.Fiscalization.Greece.Model.Types
{
    public abstract class Amount : LimitedDecimal
    {
        public Amount(decimal value, decimal? minValue = null, decimal? maxValue = null)
            : base(value, maxDecimalPlaces: 2, minValue: minValue, maxValue: maxValue)
        {
        }
    }
}
