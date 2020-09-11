namespace Mews.Fiscalization.Greece.Model.Types
{
    public class CountryCode : LimitedString
    {
        public CountryCode(string value)
            : base(value, minLength: 2, maxLength: 2)
        {
        }
    }
}
