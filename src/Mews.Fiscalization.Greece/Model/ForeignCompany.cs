using Mews.Fiscalization.Greece.Model.Types;

namespace Mews.Fiscalization.Greece.Model
{
    public class ForeignCompany: InvoiceParty
    {
        public ForeignCompany(NotEmptyString taxNumber, CountryCode countryCode, NonNegativeInt branch = null, StringIdentifier name = null, Address address = null)
            : base(taxNumber, countryCode, branch, name, address)
        {
        }
    }
}
