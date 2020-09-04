using Mews.Fiscalization.Greece.Model;
using Mews.Fiscalization.Greece.Model.Result;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mews.Fiscalization.Greece
{
    public sealed class AadeClient
    {
        public string UserId { get; }
        public string SubscriptionKey { get; }
        public string Url { get; }

        public AadeClient(string userId, string subscriptionKey, AadeEnvironment environment = AadeEnvironment.Production)
        {
            UserId = userId;
            SubscriptionKey = subscriptionKey;
        }

        public async Task<SendInvoicesResult> SendInvoicesAsync(InvoiceDocument invoiceDocument)
        {
            throw new NotImplementedException();
        }
    }
}
