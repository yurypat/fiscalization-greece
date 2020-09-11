using Mews.Fiscalization.Greece.Model.Types;
using System;

namespace Mews.Fiscalization.Greece.Model
{
    public class InvoiceRecordPartyAddress
    {
        public InvoiceRecordPartyAddress(StringIdentifier street, StringIdentifier number, NotEmptyString postalCode, NotEmptyString city)
        {
            Street = street;
            Number = number;
            PostalCode = postalCode ?? throw new ArgumentNullException(nameof(postalCode));
            City = city ?? throw new ArgumentNullException(nameof(city));
        }

        public StringIdentifier Street { get; }

        public StringIdentifier Number { get; }

        public NotEmptyString PostalCode { get; }

        public NotEmptyString City { get; }
    }
}
