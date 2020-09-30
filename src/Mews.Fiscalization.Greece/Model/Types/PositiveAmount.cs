namespace Mews.Fiscalization.Greece.Model.Types
{
    public class PositiveAmount : LimitedDecimal
    {
        public PositiveAmount(decimal value)
            : base(value, minValue: 0, maxDecimalPlaces: 2)
        {
        }
    }
}
