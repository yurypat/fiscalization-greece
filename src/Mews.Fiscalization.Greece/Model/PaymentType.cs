using System;
using System.Collections.Generic;
using System.Text;

namespace Mews.Fiscalization.Greece.Model
{
    public enum PaymentType
    {
        DomesticPaymentsAccountNumber = 1,
        ForeignMethodsAccountNumber = 2,
        Cash = 3,
        Check = 4,
        OnCredit = 5
    }
}
