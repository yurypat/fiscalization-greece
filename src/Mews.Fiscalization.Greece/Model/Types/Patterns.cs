using System.Text.RegularExpressions;

namespace Mews.Fiscalization.Greece.Model.Types
{
    public static class Patterns
    {
        public static readonly Regex VatIdentifier = new Regex("[0-9]{9}$");
    }
}
