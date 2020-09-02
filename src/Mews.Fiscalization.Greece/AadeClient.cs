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

        public AadeClient(string userId, string subscriptionKey)
        {
            UserId = userId;
            SubscriptionKey = subscriptionKey;
        }

        public async Task<SendInvoicesResult> SendInvoicesASync(InvoiceDocument invoiceDocument)
        {
            throw new NotImplementedException();
        }
    }
}
