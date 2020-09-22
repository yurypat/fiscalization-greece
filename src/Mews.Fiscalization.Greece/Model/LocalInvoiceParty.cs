using Mews.Fiscalization.Greece.Model.Types;

namespace Mews.Fiscalization.Greece.Model
{
    public class LocalInvoiceParty : InvoiceParty
    {
        private const string GreeceCountryCode = "GR";

        public LocalInvoiceParty(TaxIdentifier taxNumber, NonNegativeInt branch = null, StringIdentifier name = null, Address address = null)
            : base(taxNumber, new CountryCode(GreeceCountryCode), branch, name ,address)
        {
        }
    }
}
