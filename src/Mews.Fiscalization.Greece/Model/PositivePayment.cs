using Mews.Fiscalization.Greece.Model.Types;

namespace Mews.Fiscalization.Greece.Model
{
    public class PositivePayment : Payment
    {
        public PositivePayment(PositiveAmount amount, PaymentType paymentType)
            : base(amount, paymentType)
        {
        }
    }
}
