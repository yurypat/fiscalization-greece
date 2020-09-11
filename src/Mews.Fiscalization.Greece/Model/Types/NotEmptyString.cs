namespace Mews.Fiscalization.Greece.Model.Types
{
    public class NotEmptyString : LimitedString
    {
        public NotEmptyString(string value)
            : base(value, minLength: 1)
        {
        }
    }
}
