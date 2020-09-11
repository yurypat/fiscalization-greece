namespace Mews.Fiscalization.Greece.Model.Types
{
    public class ExchangeRate : LimitedDecimal
    {
        public ExchangeRate(decimal value)
            : base(value, minValue: 0, maxDecimalPlaces: 5)
        {
        }
    }
}
