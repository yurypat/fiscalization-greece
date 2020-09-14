using System;

namespace Mews.Fiscalization.Greece.Model.Types
{
    public class TaxIdentifier : StringIdentifier
    {
        public TaxIdentifier(string value)
            : base(value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (!Patterns.TaxIdentifier.IsMatch(value))
            {
                throw new ArgumentException($"The value '{value}' does not match the pattern '{Patterns.TaxIdentifier}'");
            }
        }
    }
}
