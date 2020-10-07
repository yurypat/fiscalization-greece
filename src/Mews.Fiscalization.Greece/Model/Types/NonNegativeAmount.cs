namespace Mews.Fiscalization.Greece.Model.Types
{
    public class NonNegativeAmount : Amount
    {
        public NonNegativeAmount(decimal value)
            : base(value, minValue: 0)
        {
        }
    }
}
