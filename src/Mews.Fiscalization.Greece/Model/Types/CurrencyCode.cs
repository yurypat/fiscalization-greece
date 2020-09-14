using Mews.Fiscalization.Greece.Dto.Xsd;
using System;

namespace Mews.Fiscalization.Greece.Model.Types
{
    public class CurrencyCode : LimitedString
    {
        public CurrencyCode(string value)
            : base(value, minLength: 3, maxLength: 3)
        {
            if (!Enum.TryParse<Currency>(value, out _))
            {
                throw new ArgumentException("Currency code is not valid ISO-4217 code.", nameof(value));
            }
        }
    }
}

