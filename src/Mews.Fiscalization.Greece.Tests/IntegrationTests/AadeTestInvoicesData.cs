using Mews.Fiscalization.Greece.Model;
using Mews.Fiscalization.Greece.Model.Types;
using System;
using System.Collections.Generic;

namespace Mews.Fiscalization.Greece.Tests.IntegrationTests
{
    internal static class AadeTestInvoicesData
    {
        private static readonly string UserVatNumber = "";

        static AadeTestInvoicesData()
        {
            UserVatNumber = Environment.GetEnvironmentVariable("user_var_number") ?? "INSERT_USER_VAT_NUMBER";
        }

        public static IEnumerable<object[]> GetInvoices()
        {
            return new List<object[]>
                {
                    new object[] { InvoiceWithEmptyCounterpart() },
                    new object[] { InvoiceWithDomesticCompanyCounterpart() },
                    new object[] { InvoiceWithForeignCompanyCounterpart("CZ", BillType.SalesInvoiceIntraCommunitySupplies, ClassificationType.IntraCommunityForeignSalesOfGoodsAndServices) },
                    new object[] { InvoiceWithForeignCompanyCounterpart("US", BillType.SalesInvoiceThirdCountrySupplies, ClassificationType.ThirdCountryForeignSalesOfGoodsAndServices) },
                };
        }

        /// <summary>
        /// Test case for bills: #1.2, 2.4, 2.5
        /// </summary>
        private static InvoiceDocument InvoiceWithEmptyCounterpart()
        {
            return new InvoiceDocument(
                new List<InvoiceRecord>()
                {
                    new InvoiceRecord(null, null, null,
                        new InvoiceRecordParty(new NotEmptyString(UserVatNumber), new NonNegativeInt(0), null, new CountryCode("GR"), null),
                        null,
                        // "InvoiceType": "Receipt"
                        new InvoiceRecordHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, BillType.RetailSalesReceipt, new CurrencyCode("EUR"), null),
                        new List<InvoiceRecordPaymentMethodDetails>
                        {
                            // "PaymentType": "Cash"
                            new InvoiceRecordPaymentMethodDetails(new Amount(100m), PaymentType.Cash)
                        },
                        new List<InvoiceRecordDetail>
                        {
                            new InvoiceRecordDetail(new PositiveInt(1), new Amount(88.50m), TaxType.Vat13, new Amount(11.50m), null, new List<InvoiceRecordIncomeClassification>
                            {
                                new InvoiceRecordIncomeClassification(ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProvisionOfServicesIncome, new Amount(88.50m))
                            })
                        },
                        new InvoiceRecordSummary(new Amount(88.50m),new Amount(11.50m), new Amount(100m),new List<InvoiceRecordIncomeClassification>
                        {
                            new InvoiceRecordIncomeClassification(ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProvisionOfServicesIncome, new Amount(88.50m))
                        })
                    )
                });
        }

        /// <summary>
        /// Test case for bills: #1.3 with domestic counterpart
        /// </summary>
        private static InvoiceDocument InvoiceWithDomesticCompanyCounterpart()
        {
            return new InvoiceDocument(
                new List<InvoiceRecord>()
                {
                    new InvoiceRecord(null, null, null,
                        new InvoiceRecordParty(new NotEmptyString(UserVatNumber), new NonNegativeInt(0), null, new CountryCode("GR"), null),
                        new InvoiceRecordParty(new NotEmptyString("090701500"), new NonNegativeInt(0), null, new CountryCode("GR"), null),                        
                        // "InvoiceType": "Sales"
                        new InvoiceRecordHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, BillType.SalesInvoice, new CurrencyCode("EUR"), null),
                        new List<InvoiceRecordPaymentMethodDetails>
                        {
                            // "PaymentType": "Cash"
                            new InvoiceRecordPaymentMethodDetails(new Amount(100m), PaymentType.Cash)
                        },
                        new List<InvoiceRecordDetail>
                        {
                            new InvoiceRecordDetail(new PositiveInt(1), new Amount(88.50m), TaxType.Vat13, new Amount(11.50m), null, new List<InvoiceRecordIncomeClassification>
                            {
                                new InvoiceRecordIncomeClassification(ClassificationType.OtherSalesOfGoodsAndServices, ClassificationCategory.ProductSaleIncome, new Amount(88.50m))
                            })
                        },
                        new InvoiceRecordSummary(new Amount(88.50m),new Amount(11.50m), new Amount(100m),new List<InvoiceRecordIncomeClassification>
                        {
                            new InvoiceRecordIncomeClassification(ClassificationType.OtherSalesOfGoodsAndServices, ClassificationCategory.ProductSaleIncome, new Amount(88.50m))
                        })
                    )
                });
        }

        /// <summary>
        /// Test case for bills: #1.3 with foreign counterpart
        /// </summary>
        private static InvoiceDocument InvoiceWithForeignCompanyCounterpart(string countryCode, BillType billType, ClassificationType classificationType)
        {
            return new InvoiceDocument(
                new List<InvoiceRecord>()
                {
                    new InvoiceRecord(null, null, null,
                        new InvoiceRecordParty(new NotEmptyString(UserVatNumber), new NonNegativeInt(0), null, new CountryCode("GR"), null),
                        new InvoiceRecordParty(new NotEmptyString("12348765"), new NonNegativeInt(0), new StringIdentifier("Name"), new CountryCode(countryCode), new InvoiceRecordPartyAddress(null, null, postalCode: new NotEmptyString("12"), city: new NotEmptyString("City"))),                       
                        // "InvoiceType": "Sales"
                        new InvoiceRecordHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, billType, new CurrencyCode("EUR"), null),
                        new List<InvoiceRecordPaymentMethodDetails>
                        {
                            // "PaymentType": "Cash"
                            new InvoiceRecordPaymentMethodDetails(new Amount(100m), PaymentType.Cash)
                        },
                        new List<InvoiceRecordDetail>
                        {
                            new InvoiceRecordDetail(new PositiveInt(1), new Amount(100m), TaxType.WithoutVat, new Amount(0m), null, new List<InvoiceRecordIncomeClassification>
                            {
                                new InvoiceRecordIncomeClassification(classificationType, ClassificationCategory.ProvisionOfServicesIncome, new Amount(100m))
                            })
                        },
                        new InvoiceRecordSummary(new Amount(100m),new Amount(0m), new Amount(100m),new List<InvoiceRecordIncomeClassification>
                        {
                            new InvoiceRecordIncomeClassification(classificationType, ClassificationCategory.ProvisionOfServicesIncome, new Amount(100m))
                        })
                    )
                });
        }
    }
}
