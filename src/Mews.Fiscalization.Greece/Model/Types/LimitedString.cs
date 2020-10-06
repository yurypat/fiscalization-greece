using System;

namespace Mews.Fiscalization.Greece.Model.Types
{
    public abstract class LimitedString : Identifier<string>
    {
        public LimitedString(string value, int minLength, int? maxLength = null)
            : base(value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (maxLength != null && value.Length > maxLength.Value)
            {
                throw new ArgumentException($"Max length of string is {maxLength.Value}.");
            }

            if (value.Length < minLength)
            {
                throw new ArgumentException($"Min length of string is {minLength}.");
            }
        }

        public static bool IsValid(string value, int minLength, int? maxLength = null)
        {
            return value != null && value.Length >= minLength && (maxLength == null || value.Length <= maxLength.Value);
        }
    }
}
