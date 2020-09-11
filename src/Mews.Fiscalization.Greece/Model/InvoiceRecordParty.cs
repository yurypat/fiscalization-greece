using Mews.Fiscalization.Greece.Model.Types;
using System;

namespace Mews.Fiscalization.Greece.Model
{
    public class InvoiceRecordParty
    {
        public InvoiceRecordParty(VatIdentifier vatNumber, NonNegativeInt branch, StringIdentifier name, CountryCode countryCode, InvoiceRecordPartyAddress invoiceRecordPartyAddress)
        {
            VatNumber = vatNumber ?? throw new ArgumentNullException(nameof(vatNumber));
            Branch = branch ?? throw new ArgumentNullException(nameof(branch));
            Name = name;
            CountryCode = countryCode ?? throw new ArgumentNullException(nameof(countryCode));
            Address = invoiceRecordPartyAddress;
        }

        public VatIdentifier VatNumber { get; }

        public NonNegativeInt Branch { get; }

        public StringIdentifier Name { get; }

        public CountryCode CountryCode { get; }

        public InvoiceRecordPartyAddress Address { get; }
    }
}
