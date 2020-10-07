using Mews.Fiscalization.Greece.Model.Types;

namespace Mews.Fiscalization.Greece.Model
{
    public class PositivePayment : Payment
    {
        public PositivePayment(NonNegativeAmount amount, PaymentType paymentType)
            : base(amount, paymentType)
        {
        }
    }
}
