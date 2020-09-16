using Mews.Fiscalization.Greece.Model.Types;
using System;

namespace Mews.Fiscalization.Greece.Model
{
    public class Address
    {
        public Address(NotEmptyString postalCode, NotEmptyString city, StringIdentifier street = null, StringIdentifier number = null)
        {
            PostalCode = postalCode ?? throw new ArgumentNullException(nameof(postalCode));
            City = city ?? throw new ArgumentNullException(nameof(city));
            Street = street;
            Number = number;
        }

        public StringIdentifier Street { get; }

        public StringIdentifier Number { get; }

        public NotEmptyString PostalCode { get; }

        public NotEmptyString City { get; }
    }
}
