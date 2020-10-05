using Mews.Fiscalization.Greece.Model;
using Mews.Fiscalization.Greece.Model.Collections;
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
        private static readonly string UserVatNumber = "";

        static AadeClientTests()
        {
            UserId = Environment.GetEnvironmentVariable("user_id") ?? "INSERT_USER_ID";
            UserSubscriptionKey = Environment.GetEnvironmentVariable("user_subscription_key") ?? "INSERT_SUBSCRIPTION_KEY";
            UserVatNumber = Environment.GetEnvironmentVariable("user_vat_number") ?? "INSERT_USER_VAT_NUMBER";
        }


        [Fact(Skip = "Temporary skip")]
        public async Task CheckUserCredentials()
        {
            // Arrange
            var client = new AadeClient(UserId, UserSubscriptionKey, AadeEnvironment.Sandbox);

            // Act
            var response = await client.CheckUserCredentialsAsync();

            // Assert
            Assert.True(response.IsSuccess);
            Assert.True(response.Success.IsAuthorized);
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

        [Fact(Skip = "Temporary skip")]
        public async Task ValidNegativeInvoiceWorks()
        {
            // Arrange
            var client = new AadeClient(UserId, UserSubscriptionKey, AadeEnvironment.Sandbox);

            // Act

            // Step 1 - regular invoice
            var invoiceDoc = new InvoiceDocument(
                SequentialEnumerable.FromPreordered(new List<PositiveInvoice>()
                {
                    new PositiveInvoice(
                        issuer: new LocalCompany(new TaxIdentifier(UserVatNumber)),
                        header: new PositiveInvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, BillType.SalesInvoice, currencyCode: new CurrencyCode("EUR")),
                        revenueItems: new List<PositiveRevenue>
                        {
                            new PositiveRevenue(new PositiveAmount(88.50m), TaxType.Vat13, new PositiveAmount(11.50m), ClassificationType.OtherOrdinaryIncome, ClassificationCategory.OtherIncomeAndProfits)
                        },
                        payments: new List<PositivePayment>
                        {
                            new PositivePayment(new PositiveAmount(100m), PaymentType.Cash)
                        },
                        counterpart: new ForeignCompany(new NotEmptyString("090701900"), new CountryCode("GR"))
                    )
                }, 0));

            var response = await client.SendInvoicesAsync(invoiceDoc);

            Assert.NotEmpty(response.SendInvoiceResults);
            Assert.True(response.SendInvoiceResults.Single().Item.IsSuccess);

            // We need to wait some time to allow external system to store the mark from the first call
            await Task.Delay(1000);

            // Step 2 - negative invoice
            var correlatedInvoice = response.SendInvoiceResults.First().Item.Success.InvoiceRegistrationNumber.Value;

            var negativeInvoice = new InvoiceDocument(
                SequentialEnumerable.FromPreordered(new List<Invoice>()
                {
                    new NegativeInvoice(
                        issuer: new LocalCompany(new TaxIdentifier(UserVatNumber)),
                        header: new NegativeInvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50021"), DateTime.Now, billType: BillType.CreditInvoice, correlatedInvoice: new InvoiceRegistrationNumber(correlatedInvoice), currencyCode: new CurrencyCode("EUR")),
                        revenueItems: new List<NegativeRevenue>
                        {
                            new NegativeRevenue(new NegativeAmount(-53.65m), TaxType.Vat6, new NegativeAmount(-12.88m), ClassificationType.OtherSalesOfGoodsAndServices, ClassificationCategory.ProductSaleIncome)
                        },
                        payments: new List<NegativePayment>
                        {
                            new NegativePayment(new NegativeAmount(-66.53m), PaymentType.Cash)
                        },
                        counterpart: new ForeignCompany(new NotEmptyString("090701900"), new CountryCode("GR"), new NonNegativeInt(0), address: new Address(postalCode: new NotEmptyString("12"), city: new NotEmptyString("City")))
                    )
                }, 0));

            var negativeInvoiceResponse = await client.SendInvoicesAsync(negativeInvoice);

            // Assert
            Assert.NotEmpty(negativeInvoiceResponse.SendInvoiceResults);
            Assert.True(negativeInvoiceResponse.SendInvoiceResults.Single().Item.IsSuccess);
        }
    }
}

