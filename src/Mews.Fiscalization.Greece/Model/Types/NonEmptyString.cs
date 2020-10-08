namespace Mews.Fiscalization.Greece.Model.Types
{
    public class NonEmptyString : LimitedString
    {
        public NonEmptyString(string value)
            : base(value, minLength: 1)
        {
        }

        public static bool IsValid(string value)
        {
            return IsValid(value, minLength: 1);
        }
    }
}
