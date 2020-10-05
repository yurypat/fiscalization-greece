using Mews.Fiscalization.Greece.Model.Types;

namespace Mews.Fiscalization.Greece.Model
{
    public class LocalCompany : Company
    {
        private const string GreeceCountryCode = "GR";

        public LocalCompany(TaxIdentifier taxNumber, NonNegativeInt branch = null, StringIdentifier name = null, Address address = null)
            : base(taxNumber, new CountryCode(GreeceCountryCode), branch, name ,address)
        {
        }
    }
}
