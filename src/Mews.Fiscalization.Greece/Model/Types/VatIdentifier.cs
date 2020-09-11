using System;

namespace Mews.Fiscalization.Greece.Model.Types
{
    public class VatIdentifier : StringIdentifier
    {
        public VatIdentifier(string value)
            : base(value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if (!Patterns.VatIdentifier.IsMatch(value))
            {
                throw new ArgumentException($"The value '{value}' does not match the pattern '{Patterns.VatIdentifier}'");
            }
        }
    }
}
