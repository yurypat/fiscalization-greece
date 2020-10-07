using Mews.Fiscalization.Greece.Model.Types;

namespace Mews.Fiscalization.Greece.Model
{
    public class NonNegativePayment : Payment
    {
        public NonNegativePayment(NonNegativeAmount amount, PaymentType paymentType)
            : base(amount, paymentType)
        {
        }
    }
}
