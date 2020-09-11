using Mews.Fiscalization.Greece.Model.Types;
using System;

namespace Mews.Fiscalization.Greece.Model
{
    public class InvoiceRecordPaymentMethodDetails
    {
        public InvoiceRecordPaymentMethodDetails(Amount amount, PaymentType paymentType)
        {
            Amount = amount ?? throw new ArgumentNullException(nameof(amount));
            PaymentType = paymentType;
        }

        public Amount Amount { get; }

        public PaymentType PaymentType { get; }
    }
}
