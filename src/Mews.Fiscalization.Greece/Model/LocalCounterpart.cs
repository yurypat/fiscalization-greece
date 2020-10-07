using Mews.Fiscalization.Greece.Model.Types;

namespace Mews.Fiscalization.Greece.Model
{
    public class LocalCounterpart : Counterpart
    {
        private const string GreeceCountryCode = "GR";

        public LocalCounterpart(TaxIdentifier taxNumber, NonNegativeInt branch = null, string name = null, Address address = null)
            : base(taxNumber, new CountryCode(GreeceCountryCode), branch, name ,address)
        {
        }
    }
}
