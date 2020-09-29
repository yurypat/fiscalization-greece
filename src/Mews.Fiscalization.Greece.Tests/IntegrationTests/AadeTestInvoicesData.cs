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
                SequentialEnumerable.FromPreordered(new List<Invoice>()
                {
                    new Invoice(
                        issuer: new LocalCompany(new TaxIdentifier(UserVatNumber)),
                        header: new InvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, BillType.RetailSalesReceipt, new CurrencyCode("EUR")),
                        revenueItems: new List<RevenueItem>
                        {
                            new RevenueItem(new Amount(53.65m), TaxType.Vat6, new Amount(12.88m), ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome)
                        },
                        payments: new List<Payment>
                        {
                            new Payment(new Amount(66.53m), PaymentType.Cash)
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
                SequentialEnumerable.FromPreordered(new List<Invoice>()
                {
                    new Invoice(
                        issuer: new LocalCompany(new TaxIdentifier(UserVatNumber)),
                        header: new InvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, BillType.RetailSalesReceipt, new CurrencyCode("EUR")),
                        revenueItems: new List<RevenueItem>
                        {
                            new RevenueItem(new Amount(53.65m), TaxType.Vat24, new Amount(12.88m), ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome, cityTax: new CityTax(CityTaxType.Hotels5Stars, new Amount(4.00m)))
                        },
                        payments: new List<Payment>
                        {
                            new Payment(new Amount(70.53m), PaymentType.Cash)
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
                SequentialEnumerable.FromPreordered(new List<Invoice>()
                {
                    new Invoice(
                        issuer: new LocalCompany(new TaxIdentifier(UserVatNumber)),
                        header: new InvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, BillType.RetailSalesReceipt, new CurrencyCode("EUR")),
                        revenueItems: new List<RevenueItem>
                        {
                            new RevenueItem(new Amount(88.50m), TaxType.Vat13, new Amount(11.50m), ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProvisionOfServicesIncome)
                        },
                        payments: new List<Payment>
                        {
                            new Payment(new Amount(100m), paymentType)
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
                SequentialEnumerable.FromPreordered(new List<Invoice>()
                {
                    new Invoice(
                        issuer: new LocalCompany(new TaxIdentifier(UserVatNumber)),
                        header: new InvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, BillType.SalesInvoice, new CurrencyCode("EUR")),
                        revenueItems: new List<RevenueItem>
                        {
                            new RevenueItem(new Amount(88.50m), TaxType.Vat13, new Amount(11.50m), ClassificationType.OtherOrdinaryIncome, ClassificationCategory.OtherIncomeAndProfits)
                        },
                        payments: new List<Payment>
                        {
                            new Payment(new Amount(100m), PaymentType.Cash)
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
                SequentialEnumerable.FromPreordered(new List<Invoice>()
                {
                    new Invoice(
                        issuer: new LocalCompany(new TaxIdentifier(UserVatNumber)),
                        header: new InvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, BillType.RetailSalesReceipt, new CurrencyCode("EUR")),
                        revenueItems: new List<RevenueItem>
                        {
                            new RevenueItem(new Amount(200.00m), TaxType.Vat0, new Amount(0.00m), ClassificationType.OtherSalesOfGoodsAndServices,  
                                ClassificationCategory.OtherIncomeAndProfits, null, VatExemptionType.VatIncludedArticle46)
                        },
                        payments: new List<Payment>
                        {
                            new Payment(new Amount(200m), PaymentType.Cash)
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
                SequentialEnumerable.FromPreordered(new List<Invoice>()
                {
                    new Invoice(
                        issuer: new LocalCompany(new TaxIdentifier(UserVatNumber)),
                        header: new InvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, BillType.SalesInvoice, new CurrencyCode("EUR")),
                        revenueItems: new List<RevenueItem>
                        {
                            new RevenueItem(new Amount(88.50m), TaxType.Vat13, new Amount(11.50m), ClassificationType.OtherSalesOfGoodsAndServices, ClassificationCategory.ProvisionOfServicesIncome)
                        },
                        payments: new List<Payment>
                        {
                            new Payment(new Amount(100m), paymentType)
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
                SequentialEnumerable.FromPreordered(new List<Invoice>()
                {
                    new Invoice(
                        issuer: new LocalCompany(new TaxIdentifier(UserVatNumber)),
                        header: new InvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, billType, new CurrencyCode("EUR"), null),
                        revenueItems: new List<RevenueItem>
                            {
                                new RevenueItem(new Amount(100m), TaxType.WithoutVat, new Amount(0m), classificationType, ClassificationCategory.ProvisionOfServicesIncome)
                            },
                        payments: new List<Payment>
                            {
                                new Payment(new Amount(100m), paymentType)
                            },
                        counterpart: new ForeignCompany(new NotEmptyString("12348765"), new CountryCode(countryCode), new NonNegativeInt(0), new StringIdentifier("Name"), new Address(postalCode: new NotEmptyString("12"), city: new NotEmptyString("City")))
                    )
                }, 0));
        }

        private static InvoiceDocument InvoiceWithVariousPaymentMethods()
        {
            return new InvoiceDocument(
                SequentialEnumerable.FromPreordered(new List<Invoice>()
                {
                    new Invoice(
                        issuer: new LocalCompany(new TaxIdentifier(UserVatNumber)),
                        header: new InvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, BillType.RetailSalesReceipt, new CurrencyCode("EUR")),
                        revenueItems: new List<RevenueItem>
                        {
                            new RevenueItem(new Amount(23.00m), TaxType.Vat0, new Amount(0.00m), ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome, vatExemption: VatExemptionType.VatIncludedArticle44)
                        },
                        payments: new List<Payment>
                        {
                            //ToDo - validate mapping for external payments
                            new Payment(new Amount(1m), PaymentType.OnCredit), //External payment (Bacs)
                            new Payment(new Amount(1m), PaymentType.OnCredit), //External payment (Bad debts)
                            new Payment(new Amount(1m), PaymentType.OnCredit), //External payment (Bancontact)
                            new Payment(new Amount(1m), PaymentType.OnCredit), //External payment (Bank charges)
                            new Payment(new Amount(1m), PaymentType.OnCredit), //External payment (Barter)
                            new Payment(new Amount(1m), PaymentType.OnCredit), //External payment (Cash)
                            new Payment(new Amount(1m), PaymentType.OnCredit), //External payment (Cheque)
                            new Payment(new Amount(1m), PaymentType.OnCredit), //External payment (Chèque vacances)
                            new Payment(new Amount(1m), PaymentType.OnCredit), //External payment (Comission)
                            new Payment(new Amount(1m), PaymentType.OnCredit), //External payment (Complimentary)
                            new Payment(new Amount(1m), PaymentType.OnCredit), //External payment (Credit card)
                            new Payment(new Amount(1m), PaymentType.OnCredit), //External payment (Cross settlement)
                            new Payment(new Amount(1m), PaymentType.OnCredit), //External payment (Exchange rate difference)
                            new Payment(new Amount(1m), PaymentType.OnCredit), //External payment (Exchange rounding difference)
                            new Payment(new Amount(1m), PaymentType.OnCredit), //External payment (Gift card)
                            new Payment(new Amount(1m), PaymentType.OnCredit), //External payment (iDeal)
                            new Payment(new Amount(1m), PaymentType.OnCredit), //External payment (Invoice)
                            new Payment(new Amount(1m), PaymentType.OnCredit), //External payment (Loyalty points)
                            new Payment(new Amount(1m), PaymentType.OnCredit), //External payment (PayPal)
                            new Payment(new Amount(1m), PaymentType.OnCredit), //External payment (Prepayment)
                            new Payment(new Amount(1m), PaymentType.OnCredit), //External payment (Reseller)
                            new Payment(new Amount(1m), PaymentType.OnCredit), //External payment (Unspecified)
                            new Payment(new Amount(1m), PaymentType.OnCredit)  //External payment (Wife transfer)
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
                SequentialEnumerable.FromPreordered(new List<Invoice>()
                {
                    new Invoice(
                        issuer: new LocalCompany(new TaxIdentifier(UserVatNumber)),
                        header: new InvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, BillType.SimplifiedInvoice, new CurrencyCode("EUR")),
                        revenueItems: new List<RevenueItem>
                        {
                            new RevenueItem(new Amount(88.50m), TaxType.Vat13, new Amount(11.50m), ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome)
                        },
                        payments: new List<Payment>
                        {
                            new Payment(new Amount(100m), PaymentType.OnCredit)
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
                SequentialEnumerable.FromPreordered(new List<Invoice>()
                {
                    new Invoice(
                        issuer: new LocalCompany(new TaxIdentifier(UserVatNumber)),
                        header: new InvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, BillType.RetailSalesReceipt, new CurrencyCode("EUR")),
                        revenueItems: new List<RevenueItem>
                        {
                            new RevenueItem(new Amount(4.03m), TaxType.Vat24, new Amount(0.97m), ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome, new PositiveInt(1)),
                            new RevenueItem(new Amount(4.03m), TaxType.Vat24, new Amount(0.97m), ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome, new PositiveInt(2))
                        },
                        payments: new List<Payment>
                        {
                            new Payment(new Amount(10m), PaymentType.Cash)
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
                SequentialEnumerable.FromPreordered(new List<Invoice>()
                {
                    new Invoice(
                        issuer: new LocalCompany(new TaxIdentifier(UserVatNumber)),
                        header: new InvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, BillType.OtherIncomeAdjustmentRegularisationEntriesAccountingBase, new CurrencyCode("EUR"), null),
                        revenueItems: new List<RevenueItem>
                        {
                            new RevenueItem(new Amount(10m), TaxType.WithoutVat, new Amount(0m), ClassificationType.CreditExchangeDifferences, ClassificationCategory.OtherIncomeAdjustmentAndRegularisationEntries)
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
                SequentialEnumerable.FromPreordered(new List<Invoice>()
                {
                    new Invoice(
                        issuer: new LocalCompany(new TaxIdentifier(UserVatNumber)),
                        header: new InvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, BillType.RetailSalesReceipt, new CurrencyCode("EUR")),
                        revenueItems: new List<RevenueItem>
                        {
                            //Night 9/16/2020
                            new RevenueItem(new Amount(88.50m), TaxType.Vat13, new Amount(11.50m), ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProvisionOfServicesIncome, new PositiveInt(1)),
                            //Service / Product
                            new RevenueItem(new Amount(5.00m), TaxType.Vat0, new Amount(0.00m), ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome, new PositiveInt(2),
                                VatExemptionType.VatIncludedArticle43),
                            //Garage
                            new RevenueItem(new Amount(16.13m), TaxType.Vat24, new Amount(3.87m), ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome, new PositiveInt(3)),
                            //CancellationFee
                            new RevenueItem(new Amount(100.00m), TaxType.Vat0, new Amount(0.00m), ClassificationType.OtherSalesOfGoodsAndServices, ClassificationCategory.OtherIncomeAndProfits, new PositiveInt(4),
                                VatExemptionType.VatIncludedArticle44),
                            //Deposit
                            new RevenueItem(new Amount(100.00m), TaxType.Vat0, new Amount(0.00m), ClassificationType.OtherSalesOfGoodsAndServices, ClassificationCategory.OtherIncomeAndProfits, new PositiveInt(5),
                                VatExemptionType.VatIncludedArticle46),
                            //Deposit
                            new RevenueItem(new Amount(100.00m), TaxType.Vat0, new Amount(0.00m), ClassificationType.OtherSalesOfGoodsAndServices, ClassificationCategory.OtherIncomeAndProfits, new PositiveInt(6),
                                VatExemptionType.WithoutVatArticle13)
                        },
                        payments: new List<Payment>
                        {
                            new Payment(new Amount(425.00m), PaymentType.Cash),
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
                SequentialEnumerable.FromPreordered(new List<Invoice>()
                {
                    new Invoice(
                        issuer: new LocalCompany(new TaxIdentifier(UserVatNumber)),
                        header: new InvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, BillType.SimplifiedInvoice, new CurrencyCode("EUR")),
                        revenueItems: new List<RevenueItem>
                        {
                            new RevenueItem(new Amount(88.50m), TaxType.Vat13, new Amount(11.50m), ClassificationType.OtherSalesOfGoodsAndServices, ClassificationCategory.OtherIncomeAndProfits)
                        },
                        payments: new List<Payment>
                        {
                            new Payment(new Amount(100m), PaymentType.Cash)
                        }
                    )
                }, 0));
        }

        private static InvoiceDocument CreditInvoiceWithNegativeAmounts()
        {
            return new InvoiceDocument(
                SequentialEnumerable.FromPreordered(new List<InvoiceBase>()
                {
                    new NegativeInvoice(
                        issuer: new LocalCompany(new TaxIdentifier(UserVatNumber)),
                        header: new NegativeInvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, currencyCode: new CurrencyCode("EUR")),
                        revenueItems: new List<NegativeRevenueItem>
                        {
                            new NegativeRevenueItem(new NegativeAmount(-53.65m), TaxType.Vat6, new NegativeAmount(-12.88m), ClassificationType.OtherSalesOfGoodsAndServices, ClassificationCategory.ProductSaleIncome)
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
