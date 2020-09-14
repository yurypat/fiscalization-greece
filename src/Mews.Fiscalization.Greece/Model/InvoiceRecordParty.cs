using Mews.Fiscalization.Greece.Model.Types;
using System;

namespace Mews.Fiscalization.Greece.Model
{
    public class InvoiceRecordParty
    {
        private const string GreeceCountryCode = "GR";

        public InvoiceRecordParty(NotEmptyString taxNumber, NonNegativeInt branch, StringIdentifier name, CountryCode countryCode, InvoiceRecordPartyAddress invoiceRecordPartyAddress)
        {
            TaxNumber = taxNumber ?? throw new ArgumentNullException(nameof(taxNumber));
            Branch = branch ?? throw new ArgumentNullException(nameof(branch));
            Name = name;
            CountryCode = countryCode ?? throw new ArgumentNullException(nameof(countryCode));
            Address = invoiceRecordPartyAddress;

            if (countryCode.Value == GreeceCountryCode && !Patterns.TaxIdentifier.IsMatch(taxNumber.Value))
            {
                throw new ArgumentException($"The value '{taxNumber.Value}' does not match the pattern '{Patterns.TaxIdentifier}'");
            }
        }

        public NotEmptyString TaxNumber { get; }

        public NonNegativeInt Branch { get; }

        public StringIdentifier Name { get; }

        public CountryCode CountryCode { get; }

        public InvoiceRecordPartyAddress Address { get; }
    }
}
