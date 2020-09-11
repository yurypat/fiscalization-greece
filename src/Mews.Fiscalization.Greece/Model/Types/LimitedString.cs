using System;

namespace Mews.Fiscalization.Greece.Model.Types
{
    public abstract class LimitedString : StringIdentifier
    {
        public LimitedString(string value, int minLength, int? maxLength = null)
            : base(value)
        {
            if (maxLength != null && value.Length > maxLength.Value)
            {
                throw new ArgumentException($"Max length of string is {maxLength.Value}.");
            }

            if (value.Length < minLength)
            {
                throw new ArgumentException($"Min length of string is {minLength}.");
            }
        }
    }
}
