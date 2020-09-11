namespace Mews.Fiscalization.Greece.Model.Types
{
    public class CurrencyCode : LimitedString
    {
        public CurrencyCode(string value)
            : base(value, minLength: 3, maxLength: 3)
        {
        }
    }
}

