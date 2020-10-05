using Mews.Fiscalization.Greece.Model.Types;

namespace Mews.Fiscalization.Greece.Model
{
    public class Company: InvoiceParty
    {
        public Company(NotEmptyString taxNumber, CountryCode countryCode, NonNegativeInt branch = null, StringIdentifier name = null, Address address = null)
            : base(taxNumber, countryCode, branch, name, address)
        {
        }
    }
}
