using Mews.Fiscalization.Greece.Mapper;
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
        private AadeLogger Logger { get; }
        private RestClient RestClient { get; }

        public AadeClient(string userId, string subscriptionKey, AadeEnvironment environment = AadeEnvironment.Production, AadeLogger logger = null)
        {
            UserId = userId;
            SubscriptionKey = subscriptionKey;
            Logger = logger;
            RestClient = new RestClient(userId, subscriptionKey, environment, logger);
        }

        public async Task<SendInvoicesResult> SendInvoicesAsync(InvoiceDocument invoiceDocument)
        {
            var mapper = new InvoiceDocumentMapper(invoiceDocument);
            var responseDoc = await RestClient.SendRequestAsync(mapper.GetInvoiceDoc());

            return new SendInvoicesResult(responseDoc);
        }
    }
}
