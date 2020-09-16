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
        private static readonly string UserVatNumber = "";

        static AadeClientTests()
        {
            UserId = Environment.GetEnvironmentVariable("user_id") ?? "INSERT_USER_ID";
            UserSubscriptionKey = Environment.GetEnvironmentVariable("user_subscription_key") ?? "INSERT_SUBSCRIPTION_KEY";
            UserVatNumber = Environment.GetEnvironmentVariable("user_var_number") ?? "INSERT_USER_VAT_NUMBER";
        }

        [Fact(Skip = "not ready yet")]
        public async Task ValidInvoiceDocumentSendInvoicesWorks()
        {
            var client = new AadeClient(UserId, UserSubscriptionKey, AadeEnvironment.Sandbox);

            var response = await client.SendInvoicesAsync(GetValidTestInvoiceDocument());

            Assert.NotEmpty(response.SendInvoiceResults.Single().InvoiceIdentifier);
            Assert.NotNull(response.SendInvoiceResults.Single().InvoiceRegistrationNumber);
            Assert.True(response.SendInvoiceResults.All(x => x.Errors == null));
        }

        [Fact(Skip = "not ready yet")]
        public async Task InvalidInvoiceDocumentSendInvoicesGetsVidationError()
        {
            var client = new AadeClient(UserId, UserSubscriptionKey, AadeEnvironment.Sandbox);

            var response = await client.SendInvoicesAsync(GetInvalidTestInvoiceDocument());

            Assert.Null(response.SendInvoiceResults.Single().InvoiceIdentifier);
            Assert.Null(response.SendInvoiceResults.Single().InvoiceRegistrationNumber);
            Assert.NotNull(response.SendInvoiceResults.Single().Errors.Single());
        }

        private InvoiceDocument GetValidTestInvoiceDocument()
        {
            return new InvoiceDocument(
                new List<Invoice>()
                {
                    new Invoice(
                        issuer: new InvoiceParty(new NotEmptyString(UserVatNumber), new CountryCode("GR")),
                        invoiceHeader: new InvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, BillType.RetailSalesReceipt, new CurrencyCode("EUR")),
                        invoiceDetails: new List<RevenueItem>
                        {
                            new RevenueItem(new Amount(53.65m), TaxType.Vat6, new Amount(12.88m), ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome)
                        },
                        invoiceSummary: new InvoiceSummary(new Amount(53.65m),new Amount( 12.88m), new Amount(66.53m), new List<ItemIncomeClassification>
                        {
                            new ItemIncomeClassification(ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome, new Amount(53.65m))
                        }),
                        paymentMethods: new List<Payment>
                        {
                            new Payment(new Amount(66.53m), PaymentType.Cash)
                        })
                });
        }

        private InvoiceDocument GetInvalidTestInvoiceDocument()
        {
            return new InvoiceDocument(
                new List<Invoice>()
                {
                    new Invoice(
                        issuer: new InvoiceParty(new NotEmptyString(UserVatNumber), new CountryCode("GR")),
                        invoiceHeader: new InvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, BillType.RetailSalesReceipt, new CurrencyCode("EUR")),
                        invoiceDetails: new List<RevenueItem>
                        {
                            new RevenueItem(new Amount(53.65m), TaxType.Vat6, new Amount(12.88m), ClassificationType.OtherSalesOfGoodsAndServices, ClassificationCategory.ProvisionOfServicesIncome)
                        },
                        invoiceSummary: new InvoiceSummary(new Amount(53.65m),new Amount( 12.88m), new Amount(66.53m), new List<ItemIncomeClassification>
                        {
                            new ItemIncomeClassification(ClassificationType.OtherSalesOfGoodsAndServices, ClassificationCategory.ProvisionOfServicesIncome, new Amount(53.65m))
                        }),
                        paymentMethods: new List<Payment>
                        {
                            new Payment(new Amount(66.53m), PaymentType.DomesticPaymentsAccountNumber)
                        }
                    )
                });
        }
    }
}

