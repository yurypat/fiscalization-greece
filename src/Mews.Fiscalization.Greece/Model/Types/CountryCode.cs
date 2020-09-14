using Mews.Fiscalization.Greece.Dto.Xsd;
using System;

namespace Mews.Fiscalization.Greece.Model.Types
{
    public class CountryCode : LimitedString
    {
        public CountryCode(string value)
            : base(value, minLength: 2, maxLength: 2)
        {
            if (!Enum.TryParse<Country>(value, out _))
            {
                throw new ArgumentException("Country code is not valid ISO 3166 code.", nameof(value));
            }
        }
    }
}
