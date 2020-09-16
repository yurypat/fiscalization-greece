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
                    new object[] { SimpleValidInvoice() },
                    new object[] { InvoiceWithEmptyCounterpart() },
                    new object[] { InvoiceWithDomesticCompanyCounterpart(PaymentType.Cash) },
                    new object[] { InvoiceWithForeignCompanyCounterpart("CZ", BillType.SalesInvoiceIntraCommunitySupplies, ClassificationType.IntraCommunityForeignSalesOfGoodsAndServices, PaymentType.Cash) },
                    new object[] { InvoiceWithForeignCompanyCounterpart("US", BillType.SalesInvoiceThirdCountrySupplies, ClassificationType.ThirdCountryForeignSalesOfGoodsAndServices, PaymentType.Cash) },
                    new object[] { InvoiceWithDomesticCompanyCounterpart(PaymentType.OnCredit) },
                    new object[] { InvoiceWithForeignCompanyCounterpart("CZ", BillType.SalesInvoiceIntraCommunitySupplies, ClassificationType.IntraCommunityForeignSalesOfGoodsAndServices, PaymentType.OnCredit) },
                    new object[] { InvoiceWithForeignCompanyCounterpart("US", BillType.SalesInvoiceThirdCountrySupplies, ClassificationType.ThirdCountryForeignSalesOfGoodsAndServices, PaymentType.OnCredit) },
                    new object[] { SimplifiedInvoiceForCustomer() },
                    new object[] { InvoiceWithConversionRate() },
                    new object[] { InvoiceWithVariousOrderItemTypes() }
                };
        }

        /// <summary>
        /// Simple invoice to pass validation
        /// </summary>
        /// <returns></returns>
        private static InvoiceDocument SimpleValidInvoice()
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

        /// <summary>
        /// Test case for bills: #1.2, #1.3, #2.4, #2.5
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
                            new InvoiceRecordDetail(new PositiveInt(1), new Amount(88.50m), TaxType.Vat13, null, new Amount(11.50m), null, new List<InvoiceRecordIncomeClassification>
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
        /// Test case for bills: #1.4 with domestic counterpart(payment type: cash) and #1.4 with domestic counterpart(with payment type: onCredit)
        /// </summary>
        private static InvoiceDocument InvoiceWithDomesticCompanyCounterpart(PaymentType paymentType)
        {
            return new InvoiceDocument(
                new List<InvoiceRecord>()
                {
                    new InvoiceRecord(null, null, null,
                        new InvoiceRecordParty(new NotEmptyString(UserVatNumber), new NonNegativeInt(0), null, new CountryCode("GR"), null),
                        new InvoiceRecordParty(new NotEmptyString("090701500"), new NonNegativeInt(0), null, new CountryCode("GR"), null),                        
                        new InvoiceRecordHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, BillType.SalesInvoice, new CurrencyCode("EUR"), null),
                        new List<InvoiceRecordPaymentMethodDetails>
                        {
                            new InvoiceRecordPaymentMethodDetails(new Amount(100m), paymentType)
                        },
                        new List<InvoiceRecordDetail>
                        {
                            new InvoiceRecordDetail(new PositiveInt(1), new Amount(88.50m), TaxType.Vat13, null, new Amount(11.50m), null, new List<InvoiceRecordIncomeClassification>
                            {
                                new InvoiceRecordIncomeClassification(ClassificationType.OtherSalesOfGoodsAndServices, ClassificationCategory.ProvisionOfServicesIncome, new Amount(88.50m))
                            })
                        },
                        new InvoiceRecordSummary(new Amount(88.50m),new Amount(11.50m), new Amount(100m),new List<InvoiceRecordIncomeClassification>
                        {
                            new InvoiceRecordIncomeClassification(ClassificationType.OtherSalesOfGoodsAndServices, ClassificationCategory.ProvisionOfServicesIncome, new Amount(88.50m))
                        })
                    )
                });
        }

        /// <summary>
        /// Test case for bills: #1.4 with foreign counterpart(payment type: cash) and #1.4 with foreign counterpart(with payment type: onCredit)
        /// </summary>
        private static InvoiceDocument InvoiceWithForeignCompanyCounterpart(string countryCode, BillType billType, ClassificationType classificationType, PaymentType paymentType)
        {
            return new InvoiceDocument(
                new List<InvoiceRecord>()
                {
                    new InvoiceRecord(null, null, null,
                        new InvoiceRecordParty(new NotEmptyString(UserVatNumber), new NonNegativeInt(0), null, new CountryCode("GR"), null),
                        new InvoiceRecordParty(new NotEmptyString("12348765"), new NonNegativeInt(0), new StringIdentifier("Name"), new CountryCode(countryCode), new InvoiceRecordPartyAddress(null, null, postalCode: new NotEmptyString("12"), city: new NotEmptyString("City"))),                       
                        new InvoiceRecordHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, billType, new CurrencyCode("EUR"), null),
                        new List<InvoiceRecordPaymentMethodDetails>
                        {
                            new InvoiceRecordPaymentMethodDetails(new Amount(100m), paymentType)
                        },
                        new List<InvoiceRecordDetail>
                        {
                            new InvoiceRecordDetail(new PositiveInt(1), new Amount(100m), TaxType.WithoutVat, null, new Amount(0m), null, new List<InvoiceRecordIncomeClassification>
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

        /// <summary>
        /// Test case for bills: #1.5
        /// </summary>
        private static InvoiceDocument SimplifiedInvoiceForCustomer()
        {
            return new InvoiceDocument(
                new List<InvoiceRecord>()
                {
                    new InvoiceRecord(null, null, null,
                        new InvoiceRecordParty(new NotEmptyString(UserVatNumber), new NonNegativeInt(0), null, new CountryCode("GR"), null),
                        null,
                        new InvoiceRecordHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, BillType.SimplifiedInvoice, new CurrencyCode("EUR"), null),
                        new List<InvoiceRecordPaymentMethodDetails>
                        {
                            new InvoiceRecordPaymentMethodDetails(new Amount(100m), PaymentType.OnCredit)
                        },
                        new List<InvoiceRecordDetail>
                        {
                            new InvoiceRecordDetail(new PositiveInt(1), new Amount(88.50m), TaxType.Vat13, null, new Amount(11.50m), null, new List<InvoiceRecordIncomeClassification>
                            {
                                new InvoiceRecordIncomeClassification(ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome, new Amount(88.50m))
                            })
                        },
                        new InvoiceRecordSummary(new Amount(88.50m),new Amount(11.50m), new Amount(100m),new List<InvoiceRecordIncomeClassification>
                        {
                            new InvoiceRecordIncomeClassification(ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome, new Amount(88.50m))
                        })
                    )
                });
        }

        /// <summary>
        /// Test case for bills: #1.6
        /// </summary>
        private static InvoiceDocument InvoiceWithConversionRate()
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
                            new InvoiceRecordPaymentMethodDetails(new Amount(10m), PaymentType.Cash)
                        },
                        new List<InvoiceRecordDetail>
                        {
                            new InvoiceRecordDetail(new PositiveInt(1), new Amount(4.03m), TaxType.Vat24, null, new Amount(0.97m), null, new List<InvoiceRecordIncomeClassification>
                            {
                                new InvoiceRecordIncomeClassification(ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome, new Amount(4.03m))
                            }),
                            new InvoiceRecordDetail(new PositiveInt(2), new Amount(4.03m), TaxType.Vat24, null, new Amount(0.97m), null, new List<InvoiceRecordIncomeClassification>
                            {
                                new InvoiceRecordIncomeClassification(ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome, new Amount(4.03m))
                            })
                        },
                        new InvoiceRecordSummary(new Amount(8.06m),new Amount(1.94m), new Amount(10m),new List<InvoiceRecordIncomeClassification>
                        {
                            new InvoiceRecordIncomeClassification(ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome, new Amount(8.06m))
                        })
                    )
                });
        }

        /// <summary>
        /// Test case for bills: #2.2 with various order item types(product order, service charge, cancellation fee, deposit, space order
        /// </summary>
        /// <returns></returns>
        private static InvoiceDocument InvoiceWithVariousOrderItemTypes()
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
