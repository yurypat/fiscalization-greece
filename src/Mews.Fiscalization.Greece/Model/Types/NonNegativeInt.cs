namespace Mews.Fiscalization.Greece.Model.Types
{
    public class NonNegativeInt : LimitedInt
    {
        public NonNegativeInt(int value)
            : base(value, minValue: 0)
        {
        }
    }
}
