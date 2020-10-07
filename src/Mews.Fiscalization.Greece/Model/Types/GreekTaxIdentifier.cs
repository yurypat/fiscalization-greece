using System;
using System.Text.RegularExpressions;

namespace Mews.Fiscalization.Greece.Model.Types
{
    public class GreekTaxIdentifier : NonEmptyString
    {
        private static readonly Regex Pattern = new Regex("[0-9]{9}$");

        public GreekTaxIdentifier(string value)
            : base(value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (!Pattern.IsMatch(value))
            {
                throw new ArgumentException($"The value '{value}' does not match the pattern '{Pattern}'");
            }
        }

        public new static bool IsValid(string value)
        {
            return value != null && Pattern.IsMatch(value);
        }
    }
}
