using System;
using Mews.Fiscalization.Greece.Model.Types;

namespace Mews.Fiscalization.Greece.Model
{
    public class Counterpart
    {
        public Counterpart(CountryCode countryCode, NonEmptyString taxIdentifier = null, NonNegativeInt branch = null, string name = null, Address address = null)
        {
            TaxIdentifier = taxIdentifier ?? new NonEmptyString("0");
            CountryCode = countryCode ?? throw new ArgumentNullException(nameof(countryCode));
            Branch = branch ?? new NonNegativeInt(0);
            Name = name;
            Address = address;
        }

        public NonEmptyString TaxIdentifier { get; }

        public NonNegativeInt Branch { get; }

        public string Name { get; }

        public CountryCode CountryCode { get; }

        public Address Address { get; }
    }
}
