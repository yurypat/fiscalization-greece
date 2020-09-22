using Mews.Fiscalization.Greece.Model.Types;
using System;

namespace Mews.Fiscalization.Greece.Model
{
    public class InvoiceParty
    {
        public InvoiceParty(StringIdentifier taxNumber, CountryCode countryCode, NonNegativeInt branch = null, StringIdentifier name = null, Address address = null)
        {
            TaxNumber = taxNumber ?? throw new ArgumentNullException(nameof(taxNumber));
            CountryCode = countryCode ?? throw new ArgumentNullException(nameof(countryCode));
            Branch = branch ?? new NonNegativeInt(0);
            Name = name;
            Address = address;
        }

        public StringIdentifier TaxNumber { get; }

        public NonNegativeInt Branch { get; }

        public StringIdentifier Name { get; }

        public CountryCode CountryCode { get; }

        public Address Address { get; }
    }
}
