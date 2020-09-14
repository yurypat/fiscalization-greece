using Mews.Fiscalization.Greece.Model.Types;
using System;

namespace Mews.Fiscalization.Greece.Model
{
    public class InvoiceRecordParty
    {
        public InvoiceRecordParty(TaxIdentifier taxNumber, NonNegativeInt branch, StringIdentifier name, CountryCode countryCode, InvoiceRecordPartyAddress invoiceRecordPartyAddress)
        {
            TaxNumber = taxNumber ?? throw new ArgumentNullException(nameof(taxNumber));
            Branch = branch ?? throw new ArgumentNullException(nameof(branch));
            Name = name;
            CountryCode = countryCode ?? throw new ArgumentNullException(nameof(countryCode));
            Address = invoiceRecordPartyAddress;
        }

        public TaxIdentifier TaxNumber { get; }

        public NonNegativeInt Branch { get; }

        public StringIdentifier Name { get; }

        public CountryCode CountryCode { get; }

        public InvoiceRecordPartyAddress Address { get; }
    }
}
