﻿using Mews.Fiscalization.Greece.Model.Types;
using System;

namespace Mews.Fiscalization.Greece.Model
{
    public abstract class PaymentBase
    {
        public PaymentBase(LimitedDecimal amount, PaymentType paymentType)
        {
            Amount = amount ?? throw new ArgumentNullException(nameof(amount));
            PaymentType = paymentType;
        }

        public LimitedDecimal Amount { get; }

        public PaymentType PaymentType { get; }
    }
}
