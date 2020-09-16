namespace Mews.Fiscalization.Greece.Model.Types
{
    public class NonNegativeInt : LimitedInt
    {
        public NonNegativeInt(int value)
            : base(value, minValue: 0)
        {
        }

        public static bool IsValid(int value)
        {
            return IsValid(value, minValue: 0);
        }
    }
}
