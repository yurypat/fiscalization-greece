namespace Mews.Fiscalization.Greece.Model.Types
{
    public class PositiveInt : LimitedInt
    {
        public PositiveInt(int value)
            : base(value, minValue: 1)
        {
        }
    }
}
