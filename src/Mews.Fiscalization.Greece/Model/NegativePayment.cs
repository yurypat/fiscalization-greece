using Mews.Fiscalization.Greece.Model.Types;

namespace Mews.Fiscalization.Greece.Model
{
    public class NegativePayment : PaymentBase
    {
        public NegativePayment(NegativeAmount amount, PaymentType paymentType)
            : base(amount, paymentType)
        {
        }
    }
}
