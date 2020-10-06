using Mews.Fiscalization.Greece.Model.Types;
using System;

namespace Mews.Fiscalization.Greece.Model
{
    public abstract class InvoiceParty
    {
        public InvoiceParty(string taxNumber, CountryCode countryCode, NonNegativeInt branch = null, string name = null, Address address = null)
        {
            TaxNumber = taxNumber ?? throw new ArgumentNullException(nameof(taxNumber));
            CountryCode = countryCode ?? throw new ArgumentNullException(nameof(countryCode));
            Branch = branch ?? new NonNegativeInt(0);
            Name = name;
            Address = address;
        }

        public string TaxNumber { get; }

        public NonNegativeInt Branch { get; }

        public string Name { get; }

        public CountryCode CountryCode { get; }

        public Address Address { get; }
    }
}
