using System;
using System.Collections.Generic;
using System.Text;

namespace Mews.Fiscalization.Greece.Dto
{
    public class InvoiceRecordPaymentMethodDetails
    {
        public InvoiceRecordPaymentMethodDetails(decimal amount, string paymentMethodInfo)
        {
            Amount = amount;
            PaymentMethodInfo = paymentMethodInfo;
        }

        public decimal Amount { get; }

        public string PaymentMethodInfo { get; }
    }
}
