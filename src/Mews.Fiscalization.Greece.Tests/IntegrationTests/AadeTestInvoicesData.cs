using Mews.Fiscalization.Greece.Model;
using Mews.Fiscalization.Greece.Model.Collections;
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
            UserVatNumber = Environment.GetEnvironmentVariable("user_vat_number") ?? "INSERT_USER_VAT_NUMBER";
        }

        public static IEnumerable<object[]> GetInvoices()
        {
            return new List<object[]>
                {
                    new object[] { SimpleValidInvoice() },
                    new object[] { SimpleValidInvoiceWithCityTax() },
                    new object[] { InvoiceWithEmptyCounterpart(PaymentType.Cash) },
                    new object[] { InvoiceWithEmptyCounterpart(PaymentType.DomesticPaymentsAccountNumber) },
                    new object[] { InvoiceWithDomesticCompanyCounterpart(PaymentType.Cash) },
                    new object[] { InvoiceWithForeignCompanyCounterpart("CZ", BillType.SalesInvoiceIntraCommunitySupplies, ClassificationType.IntraCommunityForeignSalesOfGoodsAndServices, PaymentType.Cash) },
                    new object[] { InvoiceWithForeignCompanyCounterpart("US", BillType.SalesInvoiceThirdCountrySupplies, ClassificationType.ThirdCountryForeignSalesOfGoodsAndServices, PaymentType.Cash) },
                    new object[] { InvoiceWithDomesticCompanyCounterpart(PaymentType.OnCredit) },
                    new object[] { InvoiceWithForeignCompanyCounterpart("CZ", BillType.SalesInvoiceIntraCommunitySupplies, ClassificationType.IntraCommunityForeignSalesOfGoodsAndServices, PaymentType.OnCredit) },
                    new object[] { InvoiceWithForeignCompanyCounterpart("US", BillType.SalesInvoiceThirdCountrySupplies, ClassificationType.ThirdCountryForeignSalesOfGoodsAndServices, PaymentType.OnCredit) },
                    new object[] { SimplifiedInvoiceForCustomer() },
                    new object[] { InvoiceWithConversionRate() },
                    new object[] { InvoiceWithRebateOfItems() },
                    new object[] { InvoiceWithVariousPaymentMethods() },
                    new object[] { InvoiceForDepositCashPayment() },
                    new object[] { InvoiceWithVariousOrderItemTypes() },
                    new object[] { InvoiceForCompanyWithoutDetails() },
                    new object[] { SimpleInvoiceForCompany() },
                    new object[] { CreditInvoiceWithNegativeAmounts() }
                };
        }

        /// <summary>
        /// Simple invoice to pass validation
        /// </summary>
        /// <returns></returns>
        private static InvoiceDocument SimpleValidInvoice()
        {
            return new InvoiceDocument(
                SequentialEnumerable.FromPreordered(new List<PositiveInvoice>()
                {
                    new PositiveInvoice(
                        issuer: new LocalCompany(new TaxIdentifier(UserVatNumber)),
                        header: new PositiveInvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, BillType.RetailSalesReceipt, currencyCode: new CurrencyCode("EUR")),
                        revenueItems: new List<PositiveRevenue>
                        {
                            new PositiveRevenue(new PositiveAmount(53.65m), TaxType.Vat6, new PositiveAmount(12.88m), ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome)
                        },
                        payments: new List<PositivePayment>
                        {
                            new PositivePayment(new PositiveAmount(66.53m), PaymentType.Cash)
                        }
                    )
                }, 0));
        }

        /// <summary>
        /// Simple invoice with city tax
        /// </summary>
        /// <returns></returns>
        private static InvoiceDocument SimpleValidInvoiceWithCityTax()
        {
            return new InvoiceDocument(
                SequentialEnumerable.FromPreordered(new List<PositiveInvoice>()
                {
                    new PositiveInvoice(
                        issuer: new LocalCompany(new TaxIdentifier(UserVatNumber)),
                        header: new PositiveInvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, BillType.RetailSalesReceipt, currencyCode: new CurrencyCode("EUR")),
                        revenueItems: new List<PositiveRevenue>
                        {
                            new PositiveRevenue(new PositiveAmount(53.65m), TaxType.Vat24, new PositiveAmount(12.88m), ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome, cityTax: new CityTax(CityTaxType.Hotels5Stars, new PositiveAmount(4.00m)))
                        },
                        payments: new List<PositivePayment>
                        {
                            new PositivePayment(new PositiveAmount(70.53m), PaymentType.Cash)
                        }
                    )
                }, 0));
        }

        /// <summary>
        /// Test case for bills: #1.2, #1.8, #1.16, #2.4
        /// </summary>
        private static InvoiceDocument InvoiceWithEmptyCounterpart(PaymentType paymentType)
        {
            return new InvoiceDocument(
                SequentialEnumerable.FromPreordered(new List<PositiveInvoice>()
                {
                    new PositiveInvoice(
                        issuer: new LocalCompany(new TaxIdentifier(UserVatNumber)),
                        header: new PositiveInvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, BillType.RetailSalesReceipt, currencyCode: new CurrencyCode("EUR")),
                        revenueItems: new List<PositiveRevenue>
                        {
                            new PositiveRevenue(new PositiveAmount(88.50m), TaxType.Vat13, new PositiveAmount(11.50m), ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProvisionOfServicesIncome)
                        },
                        payments: new List<PositivePayment>
                        {
                            new PositivePayment(new PositiveAmount(100m), paymentType)
                        }
                    )
                }, 0));
        }

        /// <summary>
        /// Test case for bills: #1.3
        /// </summary>
        private static InvoiceDocument SimpleInvoiceForCompany()
        {
            return new InvoiceDocument(
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
        }

        /// <summary>
        /// Test case for bill: #1.14 
        /// </summary>
        private static InvoiceDocument InvoiceForDepositCashPayment()
        {
            return new InvoiceDocument(
                SequentialEnumerable.FromPreordered(new List<PositiveInvoice>()
                {
                    new PositiveInvoice(
                        issuer: new LocalCompany(new TaxIdentifier(UserVatNumber)),
                        header: new PositiveInvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, BillType.RetailSalesReceipt, currencyCode: new CurrencyCode("EUR")),
                        revenueItems: new List<PositiveRevenue>
                        {
                            new PositiveRevenue(new PositiveAmount(200.00m), TaxType.Vat0, new PositiveAmount(0.00m), ClassificationType.OtherSalesOfGoodsAndServices,
                                ClassificationCategory.OtherIncomeAndProfits, null, VatExemptionType.VatIncludedArticle46)
                        },
                        payments: new List<PositivePayment>
                        {
                            new PositivePayment(new PositiveAmount(200m), PaymentType.Cash)
                        }
                    )
                }, 0));
        }

        /// <summary>
        /// Test case for bills: #1.4 with domestic counterpart(payment type: cash) and #1.4 with domestic counterpart(with payment type: onCredit)
        /// </summary>
        private static InvoiceDocument InvoiceWithDomesticCompanyCounterpart(PaymentType paymentType)
        {
            return new InvoiceDocument(
                SequentialEnumerable.FromPreordered(new List<PositiveInvoice>()
                {
                    new PositiveInvoice(
                        issuer: new LocalCompany(new TaxIdentifier(UserVatNumber)),
                        header: new PositiveInvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, BillType.SalesInvoice, currencyCode: new CurrencyCode("EUR")),
                        revenueItems: new List<PositiveRevenue>
                        {
                            new PositiveRevenue(new PositiveAmount(88.50m), TaxType.Vat13, new PositiveAmount(11.50m), ClassificationType.OtherSalesOfGoodsAndServices, ClassificationCategory.ProvisionOfServicesIncome)
                        },
                        payments: new List<PositivePayment>
                        {
                            new PositivePayment(new PositiveAmount(100m), paymentType)
                        },
                        counterpart: new ForeignCompany(new NotEmptyString("090701900"), new CountryCode("GR"))
                    )
                }, 0));
        }

        /// <summary>
        /// Test case for bills: #1.4 with foreign counterpart(payment type: cash) and #1.4 with foreign counterpart(with payment type: onCredit)
        /// </summary>
        private static InvoiceDocument InvoiceWithForeignCompanyCounterpart(string countryCode, BillType billType, ClassificationType classificationType, PaymentType paymentType)
        {
            return new InvoiceDocument(
                SequentialEnumerable.FromPreordered(new List<PositiveInvoice>()
                {
                    new PositiveInvoice(
                        issuer: new LocalCompany(new TaxIdentifier(UserVatNumber)),
                        header: new PositiveInvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, billType, currencyCode: new CurrencyCode("EUR")),
                        revenueItems: new List<PositiveRevenue>
                            {
                                new PositiveRevenue(new PositiveAmount(100m), TaxType.WithoutVat, new PositiveAmount(0m), classificationType, ClassificationCategory.ProvisionOfServicesIncome)
                            },
                        payments: new List<PositivePayment>
                            {
                                new PositivePayment(new PositiveAmount(100m), paymentType)
                            },
                        counterpart: new ForeignCompany(new NotEmptyString("12348765"), new CountryCode(countryCode), new NonNegativeInt(0), new StringIdentifier("Name"), new Address(postalCode: new NotEmptyString("12"), city: new NotEmptyString("City")))
                    )
                }, 0));
        }

        private static InvoiceDocument InvoiceWithVariousPaymentMethods()
        {
            return new InvoiceDocument(
                SequentialEnumerable.FromPreordered(new List<PositiveInvoice>()
                {
                    new PositiveInvoice(
                        issuer: new LocalCompany(new TaxIdentifier(UserVatNumber)),
                        header: new PositiveInvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, BillType.RetailSalesReceipt, currencyCode: new CurrencyCode("EUR")),
                        revenueItems: new List<PositiveRevenue>
                        {
                            new PositiveRevenue(new PositiveAmount(23.00m), TaxType.Vat0, new PositiveAmount(0.00m), ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome, vatExemption: VatExemptionType.VatIncludedArticle44)
                        },
                        payments: new List<PositivePayment>
                        {
                            //ToDo - validate mapping for external payments
                            new PositivePayment(new PositiveAmount(1m), PaymentType.OnCredit), //External payment (Bacs)
                            new PositivePayment(new PositiveAmount(1m), PaymentType.OnCredit), //External payment (Bad debts)
                            new PositivePayment(new PositiveAmount(1m), PaymentType.OnCredit), //External payment (Bancontact)
                            new PositivePayment(new PositiveAmount(1m), PaymentType.OnCredit), //External payment (Bank charges)
                            new PositivePayment(new PositiveAmount(1m), PaymentType.OnCredit), //External payment (Barter)
                            new PositivePayment(new PositiveAmount(1m), PaymentType.OnCredit), //External payment (Cash)
                            new PositivePayment(new PositiveAmount(1m), PaymentType.OnCredit), //External payment (Cheque)
                            new PositivePayment(new PositiveAmount(1m), PaymentType.OnCredit), //External payment (Chèque vacances)
                            new PositivePayment(new PositiveAmount(1m), PaymentType.OnCredit), //External payment (Comission)
                            new PositivePayment(new PositiveAmount(1m), PaymentType.OnCredit), //External payment (Complimentary)
                            new PositivePayment(new PositiveAmount(1m), PaymentType.OnCredit), //External payment (Credit card)
                            new PositivePayment(new PositiveAmount(1m), PaymentType.OnCredit), //External payment (Cross settlement)
                            new PositivePayment(new PositiveAmount(1m), PaymentType.OnCredit), //External payment (Exchange rate difference)
                            new PositivePayment(new PositiveAmount(1m), PaymentType.OnCredit), //External payment (Exchange rounding difference)
                            new PositivePayment(new PositiveAmount(1m), PaymentType.OnCredit), //External payment (Gift card)
                            new PositivePayment(new PositiveAmount(1m), PaymentType.OnCredit), //External payment (iDeal)
                            new PositivePayment(new PositiveAmount(1m), PaymentType.OnCredit), //External payment (Invoice)
                            new PositivePayment(new PositiveAmount(1m), PaymentType.OnCredit), //External payment (Loyalty points)
                            new PositivePayment(new PositiveAmount(1m), PaymentType.OnCredit), //External payment (PayPal)
                            new PositivePayment(new PositiveAmount(1m), PaymentType.OnCredit), //External payment (Prepayment)
                            new PositivePayment(new PositiveAmount(1m), PaymentType.OnCredit), //External payment (Reseller)
                            new PositivePayment(new PositiveAmount(1m), PaymentType.OnCredit), //External payment (Unspecified)
                            new PositivePayment(new PositiveAmount(1m), PaymentType.OnCredit)  //External payment (Wife transfer)
                        }
                    )
                }, 0));
        }

        /// <summary>
        /// Test case for bills: #1.5
        /// </summary>
        private static InvoiceDocument SimplifiedInvoiceForCustomer()
        {
            return new InvoiceDocument(
                SequentialEnumerable.FromPreordered(new List<PositiveInvoice>()
                {
                    new PositiveInvoice(
                        issuer: new LocalCompany(new TaxIdentifier(UserVatNumber)),
                        header: new PositiveInvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, BillType.SimplifiedInvoice, currencyCode: new CurrencyCode("EUR")),
                        revenueItems: new List<PositiveRevenue>
                        {
                            new PositiveRevenue(new PositiveAmount(88.50m), TaxType.Vat13, new PositiveAmount(11.50m), ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome)
                        },
                        payments: new List<PositivePayment>
                        {
                            new PositivePayment(new PositiveAmount(100m), PaymentType.OnCredit)
                        }
                    )
                }, 0));
        }

        /// <summary>
        /// Test case for bills: #1.6
        /// </summary>
        private static InvoiceDocument InvoiceWithConversionRate()
        {
            return new InvoiceDocument(
                SequentialEnumerable.FromPreordered(new List<PositiveInvoice>()
                {
                    new PositiveInvoice(
                        issuer: new LocalCompany(new TaxIdentifier(UserVatNumber)),
                        header: new PositiveInvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, BillType.RetailSalesReceipt, currencyCode: new CurrencyCode("EUR")),
                        revenueItems: new List<PositiveRevenue>
                        {
                            new PositiveRevenue(new PositiveAmount(4.03m), TaxType.Vat24, new PositiveAmount(0.97m), ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome, new PositiveInt(1)),
                            new PositiveRevenue(new PositiveAmount(4.03m), TaxType.Vat24, new PositiveAmount(0.97m), ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome, new PositiveInt(2))
                        },
                        payments: new List<PositivePayment>
                        {
                            new PositivePayment(new PositiveAmount(10m), PaymentType.Cash)
                        }
                    )
                }, 0));
        }

        /// <summary>
        /// Test case for bills: #1.7
        /// </summary>
        private static InvoiceDocument InvoiceWithRebateOfItems()
        {
            return new InvoiceDocument(
                SequentialEnumerable.FromPreordered(new List<PositiveInvoice>()
                {
                    new PositiveInvoice(
                        issuer: new LocalCompany(new TaxIdentifier(UserVatNumber)),
                        header: new PositiveInvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, BillType.OtherIncomeAdjustmentRegularisationEntriesAccountingBase, currencyCode: new CurrencyCode("EUR")),
                        revenueItems: new List<PositiveRevenue>
                        {
                            new PositiveRevenue(new PositiveAmount(10m), TaxType.WithoutVat, new PositiveAmount(0m), ClassificationType.CreditExchangeDifferences, ClassificationCategory.OtherIncomeAdjustmentAndRegularisationEntries)
                        }
                    )
                }, 0));
        }

        /// <summary>
        /// Test case for bills: #2.2 with various order item types(product order, service charge, cancellation fee, deposit, space order
        /// </summary>
        /// <returns></returns>
        private static InvoiceDocument InvoiceWithVariousOrderItemTypes()
        {
            return new InvoiceDocument(
                SequentialEnumerable.FromPreordered(new List<PositiveInvoice>()
                {
                    new PositiveInvoice(
                        issuer: new LocalCompany(new TaxIdentifier(UserVatNumber)),
                        header: new PositiveInvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, BillType.RetailSalesReceipt, currencyCode: new CurrencyCode("EUR")),
                        revenueItems: new List<PositiveRevenue>
                        {
                            //Night 9/16/2020
                            new PositiveRevenue(new PositiveAmount(88.50m), TaxType.Vat13, new PositiveAmount(11.50m), ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProvisionOfServicesIncome, new PositiveInt(1)),
                            //Service / Product
                            new PositiveRevenue(new PositiveAmount(5.00m), TaxType.Vat0, new PositiveAmount(0.00m), ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome, new PositiveInt(2),
                                VatExemptionType.VatIncludedArticle43),
                            //Garage
                            new PositiveRevenue(new PositiveAmount(16.13m), TaxType.Vat24, new PositiveAmount(3.87m), ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome, new PositiveInt(3)),
                            //CancellationFee
                            new PositiveRevenue(new PositiveAmount(100.00m), TaxType.Vat0, new PositiveAmount(0.00m), ClassificationType.OtherSalesOfGoodsAndServices, ClassificationCategory.OtherIncomeAndProfits, new PositiveInt(4),
                                VatExemptionType.VatIncludedArticle44),
                            //Deposit
                            new PositiveRevenue(new PositiveAmount(100.00m), TaxType.Vat0, new PositiveAmount(0.00m), ClassificationType.OtherSalesOfGoodsAndServices, ClassificationCategory.OtherIncomeAndProfits, new PositiveInt(5),
                                VatExemptionType.VatIncludedArticle46),
                            //Deposit
                            new PositiveRevenue(new PositiveAmount(100.00m), TaxType.Vat0, new PositiveAmount(0.00m), ClassificationType.OtherSalesOfGoodsAndServices, ClassificationCategory.OtherIncomeAndProfits, new PositiveInt(6),
                                VatExemptionType.WithoutVatArticle13)
                        },
                        payments: new List<PositivePayment>
                        {
                            new PositivePayment(new PositiveAmount(425.00m), PaymentType.Cash),
                        }
                    )
                }, 0));
        }

        /// <summary>
        /// Test case for bills: #2.5
        /// </summary>
        private static InvoiceDocument InvoiceForCompanyWithoutDetails()
        {
            return new InvoiceDocument(
                SequentialEnumerable.FromPreordered(new List<PositiveInvoice>()
                {
                    new PositiveInvoice(
                        issuer: new LocalCompany(new TaxIdentifier(UserVatNumber)),
                        header: new PositiveInvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, BillType.SimplifiedInvoice, currencyCode: new CurrencyCode("EUR")),
                        revenueItems: new List<PositiveRevenue>
                        {
                            new PositiveRevenue(new PositiveAmount(88.50m), TaxType.Vat13, new PositiveAmount(11.50m), ClassificationType.OtherSalesOfGoodsAndServices, ClassificationCategory.OtherIncomeAndProfits)
                        },
                        payments: new List<PositivePayment>
                        {
                            new PositivePayment(new PositiveAmount(100m), PaymentType.Cash)
                        }
                    )
                }, 0));
        }

        private static InvoiceDocument CreditInvoiceWithNegativeAmounts()
        {
            return new InvoiceDocument(
                SequentialEnumerable.FromPreordered(new List<Invoice>()
                {
                    new NegativeInvoice(
                        issuer: new LocalCompany(new TaxIdentifier(UserVatNumber)),
                        header: new NegativeInvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, BillType.CreditInvoiceNonAssociated, currencyCode: new CurrencyCode("EUR")),
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
        }
    }
}
