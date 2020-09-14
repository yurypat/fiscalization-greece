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
                new List<InvoiceRecord>()
                {
                    new InvoiceRecord(null, null, null,
                        new InvoiceRecordParty(new TaxIdentifier(UserVatNumber), new NonNegativeInt(0), null, new CountryCode("GR"), null),
                        null,
                        new InvoiceRecordHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, BillType.RetailSalesReceipt, new CurrencyCode("EUR"), null),
                        new List<InvoiceRecordPaymentMethodDetails>
                        {
                            new InvoiceRecordPaymentMethodDetails(new Amount(66.53m), PaymentType.Cash)
                        },
                        new List<InvoiceRecordDetail>
                        {
                            new InvoiceRecordDetail(new PositiveInt(1), new Amount(53.65m), TaxType.Vat6, new Amount(12.88m), null, new List<InvoiceRecordIncomeClassification>
                            {
                                new InvoiceRecordIncomeClassification(ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome, new Amount(53.65m))
                            })
                        },
                        new InvoiceRecordSummary(new Amount(53.65m),new Amount( 12.88m), new Amount(66.53m),new List<InvoiceRecordIncomeClassification>
                        {
                            new InvoiceRecordIncomeClassification(ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome, new Amount(53.65m))
                        })
                    )
                });
        }

        private InvoiceDocument GetInvalidTestInvoiceDocument()
        {
            return new InvoiceDocument(
                new List<InvoiceRecord>()
                {
                    new InvoiceRecord(null, null, null,
                        new InvoiceRecordParty(new TaxIdentifier(UserVatNumber), new NonNegativeInt(0), null, new CountryCode("GR"), null),
                        null,
                        new InvoiceRecordHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, BillType.RetailSalesReceipt, new CurrencyCode("EUR"), null),
                        new List<InvoiceRecordPaymentMethodDetails>
                        {
                            new InvoiceRecordPaymentMethodDetails(new Amount(66.53m), PaymentType.DomesticPaymentsAccountNumber)
                        },
                        new List<InvoiceRecordDetail>
                        {
                            new InvoiceRecordDetail(new PositiveInt(1), new Amount(53.65m), TaxType.Vat6, new Amount(12.88m), null, new List<InvoiceRecordIncomeClassification>
                            {
                                new InvoiceRecordIncomeClassification(ClassificationType.OtherSalesOfGoodsAndServices, ClassificationCategory.ProvisionOfServicesIncome, new Amount(53.65m))
                            })
                        },
                        new InvoiceRecordSummary(new Amount(53.65m),new Amount( 12.88m), new Amount(66.53m),new List<InvoiceRecordIncomeClassification>
                        {
                            new InvoiceRecordIncomeClassification(ClassificationType.OtherSalesOfGoodsAndServices, ClassificationCategory.ProvisionOfServicesIncome, new Amount(53.65m))
                        })
                    )
                });
        }
    }
}

