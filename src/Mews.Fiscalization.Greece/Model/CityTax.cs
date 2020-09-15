using Mews.Fiscalization.Greece.Model.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mews.Fiscalization.Greece.Model
{
    public class CityTax
    {
        public CityTax(CityTaxType cityTaxType, Amount cityTaxAmount)
        {
            CityTaxType = cityTaxType;
            CityTaxAmount = cityTaxAmount ?? throw new ArgumentNullException(nameof(cityTaxAmount));
        }

        public CityTaxType CityTaxType { get; }

        public Amount CityTaxAmount { get; }
    }
}
