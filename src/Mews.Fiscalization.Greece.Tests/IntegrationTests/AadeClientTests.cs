using Mews.Fiscalization.Greece.Model;
using Mews.Fiscalization.Greece.Model.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Mews.Fiscalization.Greece.Tests.IntegrationTests
{
    public class AadeClientTests
    {
        private static readonly string UserId = "";
        private static readonly string UserSubscriptionKey = "";

        static AadeClientTests()
        {
            UserId = Environment.GetEnvironmentVariable("user_id") ?? "INSERT_USER_ID";
            UserSubscriptionKey = Environment.GetEnvironmentVariable("user_subscription_key") ?? "INSERT_SUBSCRIPTION_KEY";
        }

        [Theory(Skip = "Temporary skip")]
        [MemberData(nameof(AadeTestInvoicesData.GetInvoices), MemberType = typeof(AadeTestInvoicesData))]
        public async Task ValidInvoiceDocumentWorks(InvoiceDocument invoiceDoc)
        {
            // Arrange
            var client = new AadeClient(UserId, UserSubscriptionKey, AadeEnvironment.Sandbox);

            // Act
            var response = await client.SendInvoicesAsync(invoiceDoc);

            // Assert
            Assert.NotEmpty(response.SendInvoiceResults);
            Assert.True(response.SendInvoiceResults.Single().Item.IsSuccess);
        }
    }
}

