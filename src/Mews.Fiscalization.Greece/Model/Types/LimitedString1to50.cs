namespace Mews.Fiscalization.Greece.Model.Types
{
    public class LimitedString1to50 : LimitedString
    {
        public LimitedString1to50(string value)
            : base(value, 1, 50)
        {
        }
    }
}
