using Mews.Fiscalization.Greece.Model;
using Mews.Fiscalization.Greece.Model.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [Fact]
        public async Task ValidInvoiceDocumentSendInvoicesWorks()
        {
            // Arrange
            var client = new AadeClient(UserId, UserSubscriptionKey, AadeEnvironment.Sandbox);

            // Act
            var response = await client.SendInvoicesAsync(GetValidTestInvoiceDocument());

            // Assert
            Assert.NotEmpty(response.SendInvoiceResults.Single().InvoiceIdentifier);
            Assert.NotNull(response.SendInvoiceResults.Single().InvoiceRegistrationNumber);
            Assert.True(response.SendInvoiceResults.All(x => x.Errors == null));
        }

        [Fact]
        public async Task InvalidInvoiceDocumentSendInvoicesGetsVidationError()
        {
            // Arrange
            var client = new AadeClient(UserId, UserSubscriptionKey, AadeEnvironment.Sandbox);

            // Act
            var response = await client.SendInvoicesAsync(GetInvalidTestInvoiceDocument());

            // Assert
            Assert.Null(response.SendInvoiceResults.Single().InvoiceIdentifier);
            Assert.Null(response.SendInvoiceResults.Single().InvoiceRegistrationNumber);
            Assert.NotNull(response.SendInvoiceResults.Single().Errors.Single());
        }

        [Theory]
        [MemberData(nameof(AadeTestInvoicesData.GetInvoices), MemberType = typeof(AadeTestInvoicesData))]
        public async Task ValidInvoiceDocumentWorks(InvoiceDocument invoiceDoc)
        {
            // Arrange
            var client = new AadeClient(UserId, UserSubscriptionKey, AadeEnvironment.Sandbox);

            // Act
            var response = await client.SendInvoicesAsync(invoiceDoc);

            // Assert
            Assert.NotEmpty(response.SendInvoiceResults.Single().InvoiceIdentifier);
            Assert.NotNull(response.SendInvoiceResults.Single().InvoiceRegistrationNumber);
            Assert.True(response.SendInvoiceResults.All(x => x.Errors == null));
        }

        private static InvoiceDocument GetValidTestInvoiceDocument()
        {
            return new InvoiceDocument(
                new List<InvoiceRecord>()
                {
                    new InvoiceRecord(null, null, null,
                        new InvoiceRecordParty(new NotEmptyString(UserVatNumber), new NonNegativeInt(0), null, new CountryCode("GR"), null),
                        null,
                        new InvoiceRecordHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, BillType.RetailSalesReceipt, new CurrencyCode("EUR"), null),
                        new List<InvoiceRecordPaymentMethodDetails>
                        {
                            new InvoiceRecordPaymentMethodDetails(new Amount(66.53m), PaymentType.Cash)
                        },
                        new List<InvoiceRecordDetail>
                        {
                            new InvoiceRecordDetail(new PositiveInt(1), new Amount(53.65m), TaxType.Vat6, null, new Amount(12.88m), null, new List<InvoiceRecordIncomeClassification>
                            {
                                new InvoiceRecordIncomeClassification(ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome, new Amount(53.65m))
                            })
                        },
                        new InvoiceRecordSummary(new Amount(53.65m),new Amount(12.88m), new Amount(66.53m),new List<InvoiceRecordIncomeClassification>
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
                        new InvoiceRecordParty(new NotEmptyString(UserVatNumber), new NonNegativeInt(0), null, new CountryCode("GR"), null),
                        null,
                        new InvoiceRecordHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, BillType.RetailSalesReceipt, new CurrencyCode("EUR"), null),
                        new List<InvoiceRecordPaymentMethodDetails>
                        {
                            new InvoiceRecordPaymentMethodDetails(new Amount(66.53m), PaymentType.DomesticPaymentsAccountNumber)
                        },
                        new List<InvoiceRecordDetail>
                        {
                            new InvoiceRecordDetail(new PositiveInt(1), new Amount(53.65m), TaxType.Vat6, null, new Amount(12.88m), null, new List<InvoiceRecordIncomeClassification>
                            {
                                new InvoiceRecordIncomeClassification(ClassificationType.OtherSalesOfGoodsAndServices, ClassificationCategory.ProvisionOfServicesIncome, new Amount(53.65m))
                            })
                        },
                        new InvoiceRecordSummary(new Amount(53.65m),new Amount(12.88m), new Amount(66.53m),new List<InvoiceRecordIncomeClassification>
                        {
                            new InvoiceRecordIncomeClassification(ClassificationType.OtherSalesOfGoodsAndServices, ClassificationCategory.ProvisionOfServicesIncome, new Amount(53.65m))
                        })
                    )
                });
        }

        [Description("Bill 2 - Bill with various order item types(product order, service charge, cancellation fee, deposit, space order")]
        private InvoiceDocument GetInvoiceDocumentVariousOrderItemTypes()
        {
            return new InvoiceDocument(
                new List<InvoiceRecord>()
                {
                    new InvoiceRecord(null, null, null,
                        new InvoiceRecordParty(new NotEmptyString(UserVatNumber), new NonNegativeInt(0), null, new CountryCode("GR"), null),
                        null,
                        new InvoiceRecordHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, BillType.RetailSalesReceipt, new CurrencyCode("EUR"), null),
                        new List<InvoiceRecordPaymentMethodDetails>
                        {
                            new InvoiceRecordPaymentMethodDetails(new Amount(425.00m), PaymentType.Cash),
                        },
                        new List<InvoiceRecordDetail>
                        {
                            //Night 9/16/2020
                            new InvoiceRecordDetail(new PositiveInt(1), new Amount(88.50m), TaxType.Vat13, null, new Amount(11.50m), null, new List<InvoiceRecordIncomeClassification>
                            {
                                new InvoiceRecordIncomeClassification(ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProvisionOfServicesIncome, new Amount(88.50m))
                            }),
                            //Service / Product
                            new InvoiceRecordDetail(new PositiveInt(2), new Amount(5.00m), TaxType.Vat0, VatExemption.VatIncludedArticle43, new Amount(0.00m), null, new List<InvoiceRecordIncomeClassification>
                            {
                                new InvoiceRecordIncomeClassification(ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome, new Amount(5.00m))
                            }),
                            //Garage
                            new InvoiceRecordDetail(new PositiveInt(3), new Amount(16.13m), TaxType.Vat24, null, new Amount(3.87m), null, new List<InvoiceRecordIncomeClassification>
                            {
                                new InvoiceRecordIncomeClassification(ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome, new Amount(16.13m))
                            }),
                            //CancellationFee
                            new InvoiceRecordDetail(new PositiveInt(4), new Amount(100.00m), TaxType.Vat0, VatExemption.VatIncludedArticle44, new Amount(0.00m), null, new List<InvoiceRecordIncomeClassification>
                            {
                                new InvoiceRecordIncomeClassification(ClassificationType.OtherOrdinaryIncome, ClassificationCategory.OtherIncomeAndProfits, new Amount(100.00m))
                            }),
                            //Deposit
                            new InvoiceRecordDetail(new PositiveInt(5), new Amount(100.00m), TaxType.Vat0, VatExemption.VatIncludedArticle46, new Amount(0.00m), null, new List<InvoiceRecordIncomeClassification>
                            {
                                new InvoiceRecordIncomeClassification(ClassificationType.OtherOrdinaryIncome, ClassificationCategory.OtherIncomeAndProfits, new Amount(100.00m))
                            }),
                            //Deposit
                            new InvoiceRecordDetail(new PositiveInt(6), new Amount(100.00m), TaxType.Vat0, VatExemption.WithoutVatArticle13, new Amount(0.00m), null, new List<InvoiceRecordIncomeClassification>
                            {
                                new InvoiceRecordIncomeClassification(ClassificationType.OtherOrdinaryIncome, ClassificationCategory.OtherIncomeAndProfits, new Amount(100.00m))
                            })
                        },
                        new InvoiceRecordSummary(new Amount(409.63m), new Amount(15.37m), new Amount(425.00m),new List<InvoiceRecordIncomeClassification>
                        {
                            new InvoiceRecordIncomeClassification(ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProvisionOfServicesIncome, new Amount(88.50m)),
                            new InvoiceRecordIncomeClassification(ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome, new Amount(21.13m)),
                            new InvoiceRecordIncomeClassification(ClassificationType.OtherOrdinaryIncome, ClassificationCategory.OtherIncomeAndProfits, new Amount(300.00m))
                        })
                    )
                });
        }
        }
}

