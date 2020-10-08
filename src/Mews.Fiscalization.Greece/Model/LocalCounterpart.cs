using Mews.Fiscalization.Greece.Model.Types;

namespace Mews.Fiscalization.Greece.Model
{
    public class LocalCounterpart : Counterpart
    {
        private const string GreeceCountryCode = "GR";

        public LocalCounterpart(GreekTaxIdentifier taxIdentifier, NonNegativeInt branch = null, string name = null, Address address = null)
            : base(new CountryCode(GreeceCountryCode), taxIdentifier: taxIdentifier, branch, name ,address)
        {
        }
    }
}
