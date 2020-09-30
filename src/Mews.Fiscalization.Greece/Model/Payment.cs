﻿using Mews.Fiscalization.Greece.Model.Types;

namespace Mews.Fiscalization.Greece.Model
{
    public class Payment : PaymentBase
    {
        public Payment(Amount amount, PaymentType paymentType)
            : base(amount, paymentType)
        {
        }
    }
}
