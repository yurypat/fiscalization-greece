namespace Mews.Fiscalization.Greece.Model.Types
{
    public class PositiveInt : LimitedInt
    {
        public PositiveInt(int value)
            : base(value, minValue: 1)
        {
        }

        public static bool IsValid(int value)
        {
            return IsValid(value, minValue: 1);
        }
    }
}
