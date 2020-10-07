using Mews.Fiscalization.Greece.Model;
using Mews.Fiscalization.Greece.Model.Collections;
using Mews.Fiscalization.Greece.Model.Types;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var invoices = new List<object>
            {
                SimpleValidInvoice(),
                InvoiceWithEmptyCounterpart(PaymentType.Cash),
                InvoiceWithEmptyCounterpart(PaymentType.DomesticPaymentsAccountNumber),
                InvoiceWithDomesticCompanyCounterpart(PaymentType.Cash),
                InvoiceWithForeignCompanyCounterpart("CZ", BillType.SalesInvoiceIntraCommunitySupplies, ClassificationType.IntraCommunityForeignSalesOfGoodsAndServices, PaymentType.Cash),
                InvoiceWithForeignCompanyCounterpart("US", BillType.SalesInvoiceThirdCountrySupplies, ClassificationType.ThirdCountryForeignSalesOfGoodsAndServices, PaymentType.Cash),
                InvoiceWithDomesticCompanyCounterpart(PaymentType.OnCredit),
                InvoiceWithForeignCompanyCounterpart("CZ", BillType.SalesInvoiceIntraCommunitySupplies, ClassificationType.IntraCommunityForeignSalesOfGoodsAndServices, PaymentType.OnCredit),
                InvoiceWithForeignCompanyCounterpart("US", BillType.SalesInvoiceThirdCountrySupplies, ClassificationType.ThirdCountryForeignSalesOfGoodsAndServices, PaymentType.OnCredit),
                SimplifiedInvoiceForCustomer(),
                InvoiceWithConversionRate(),
                InvoiceWithRebateOfItems(),
                InvoiceWithVariousPaymentMethods(),
                InvoiceForDepositCashPayment(),
                InvoiceWithVariousOrderItemTypes(),
                InvoiceForCompanyWithoutDetails(),
                SimpleInvoiceForCompany(),
                CreditInvoiceWithNegativeAmounts()
            };
            return invoices.Select(i => new [] { i });
        }

        /// <summary>
        /// Simple invoice to pass validation
        /// </summary>
        /// <returns></returns>
        private static ISequentialEnumerable<Invoice> SimpleValidInvoice()
        {
            return SequentialEnumerable.FromPreordered(
                new NonNegativeInvoice(
                    issuer: new LocalCounterpart(new GreekTaxIdentifier(UserVatNumber)),
                    billType: BillType.RetailSalesReceipt,
                    header: new InvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, currencyCode: new CurrencyCode("EUR")),
                    revenueItems: new List<NonNegativeRevenue>
                    {
                        new NonNegativeRevenue(new NonNegativeAmount(53.65m), new NonNegativeAmount(12.88m), TaxType.Vat6, ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome)
                    },
                    payments: new List<NonNegativePayment>
                    {
                        new NonNegativePayment(new NonNegativeAmount(66.53m), PaymentType.Cash)
                    }
                )
            );
        }

        /// <summary>
        /// Test case for bills: #1.2, #1.8, #1.16, #2.4
        /// </summary>
        private static ISequentialEnumerable<Invoice> InvoiceWithEmptyCounterpart(PaymentType paymentType)
        {
            return SequentialEnumerable.FromPreordered(
                new NonNegativeInvoice(
                    issuer: new LocalCounterpart(new GreekTaxIdentifier(UserVatNumber)),
                    billType: BillType.RetailSalesReceipt,
                    header: new InvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, currencyCode: new CurrencyCode("EUR")),
                    revenueItems: new List<NonNegativeRevenue>
                    {
                        new NonNegativeRevenue(new NonNegativeAmount(88.50m), new NonNegativeAmount(11.50m), TaxType.Vat13, ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProvisionOfServicesIncome)
                    },
                    payments: new List<NonNegativePayment>
                    {
                        new NonNegativePayment(new NonNegativeAmount(100m), paymentType)
                    }
                )
            );
        }

        /// <summary>
        /// Test case for bills: #1.3
        /// </summary>
        private static ISequentialEnumerable<Invoice> SimpleInvoiceForCompany()
        {
            return SequentialEnumerable.FromPreordered(
                new NonNegativeInvoice(
                    issuer: new LocalCounterpart(new GreekTaxIdentifier(UserVatNumber)),
                    billType: BillType.SalesInvoice,
                    header: new InvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, currencyCode: new CurrencyCode("EUR")),
                    revenueItems: new List<NonNegativeRevenue>
                    {
                        new NonNegativeRevenue(new NonNegativeAmount(88.50m), new NonNegativeAmount(11.50m), TaxType.Vat13, ClassificationType.OtherOrdinaryIncome, ClassificationCategory.OtherIncomeAndProfits)
                    },
                    payments: new List<NonNegativePayment>
                    {
                        new NonNegativePayment(new NonNegativeAmount(100m), PaymentType.Cash)
                    },
                    counterpart: new LocalCounterpart(new GreekTaxIdentifier("090701900"))
                )
            );
        }

        /// <summary>
        /// Test case for bill: #1.14 
        /// </summary>
        private static ISequentialEnumerable<Invoice> InvoiceForDepositCashPayment()
        {
            return SequentialEnumerable.FromPreordered(
                new NonNegativeInvoice(
                    issuer: new LocalCounterpart(new GreekTaxIdentifier(UserVatNumber)),
                    billType: BillType.RetailSalesReceipt,
                    header: new InvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, currencyCode: new CurrencyCode("EUR")),
                    revenueItems: new List<NonNegativeRevenue>
                    {
                        new NonNegativeRevenue(new NonNegativeAmount(200.00m), new NonNegativeAmount(0.00m), TaxType.Vat0, ClassificationType.OtherSalesOfGoodsAndServices,
                            ClassificationCategory.OtherIncomeAndProfits, null, VatExemptionType.VatIncludedArticle46)
                    },
                    payments: new List<NonNegativePayment>
                    {
                        new NonNegativePayment(new NonNegativeAmount(200m), PaymentType.Cash)
                    }
                )
            );
        }

        /// <summary>
        /// Test case for bills: #1.4 with domestic counterpart(payment type: cash) and #1.4 with domestic counterpart(with payment type: onCredit)
        /// </summary>
        private static ISequentialEnumerable<Invoice> InvoiceWithDomesticCompanyCounterpart(PaymentType paymentType)
        {
            return SequentialEnumerable.FromPreordered(
                new NonNegativeInvoice(
                    issuer: new LocalCounterpart(new GreekTaxIdentifier(UserVatNumber)),
                    billType: BillType.SalesInvoice,
                    header: new InvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, currencyCode: new CurrencyCode("EUR")),
                    revenueItems: new List<NonNegativeRevenue>
                    {
                        new NonNegativeRevenue(new NonNegativeAmount(88.50m), new NonNegativeAmount(11.50m), TaxType.Vat13, ClassificationType.OtherSalesOfGoodsAndServices, ClassificationCategory.ProvisionOfServicesIncome)
                    },
                    payments: new List<NonNegativePayment>
                    {
                        new NonNegativePayment(new NonNegativeAmount(100m), paymentType)
                    },
                    counterpart: new LocalCounterpart(new GreekTaxIdentifier("090701900"))
                )
            );
        }

        /// <summary>
        /// Test case for bills: #1.4 with foreign counterpart(payment type: cash) and #1.4 with foreign counterpart(with payment type: onCredit)
        /// </summary>
        private static ISequentialEnumerable<Invoice> InvoiceWithForeignCompanyCounterpart(string countryCode, BillType billType, ClassificationType classificationType, PaymentType paymentType)
        {
            return SequentialEnumerable.FromPreordered(
                new NonNegativeInvoice(
                    issuer: new LocalCounterpart(new GreekTaxIdentifier(UserVatNumber)),
                    billType: billType,
                    header: new InvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, currencyCode: new CurrencyCode("EUR")),
                    revenueItems: new List<NonNegativeRevenue>
                    {
                        new NonNegativeRevenue(new NonNegativeAmount(100m), new NonNegativeAmount(0m), TaxType.WithoutVat, classificationType, ClassificationCategory.ProvisionOfServicesIncome)
                    },
                    payments: new List<NonNegativePayment>
                    {
                        new NonNegativePayment(new NonNegativeAmount(100m), paymentType)
                    },
                    counterpart: new Counterpart(new CountryCode(countryCode), new NonEmptyString("12348765"), new NonNegativeInt(0), "Name", new Address(postalCode: new NonEmptyString("12"), city: new NonEmptyString("City")))
                )
            );
        }

        private static ISequentialEnumerable<Invoice> InvoiceWithVariousPaymentMethods()
        {
            return SequentialEnumerable.FromPreordered(
                new NonNegativeInvoice(
                    issuer: new LocalCounterpart(new GreekTaxIdentifier(UserVatNumber)),
                    billType: BillType.RetailSalesReceipt,
                    header: new InvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, currencyCode: new CurrencyCode("EUR")),
                    revenueItems: new List<NonNegativeRevenue>
                    {
                        new NonNegativeRevenue(new NonNegativeAmount(23.00m), new NonNegativeAmount(0.00m), TaxType.Vat0, ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome, vatExemption: VatExemptionType.VatIncludedArticle44)
                    },
                    payments: new List<NonNegativePayment>
                    {
                        //ToDo - validate mapping for external payments
                        new NonNegativePayment(new NonNegativeAmount(1m), PaymentType.OnCredit), //External payment (Bacs)
                        new NonNegativePayment(new NonNegativeAmount(1m), PaymentType.OnCredit), //External payment (Bad debts)
                        new NonNegativePayment(new NonNegativeAmount(1m), PaymentType.OnCredit), //External payment (Bancontact)
                        new NonNegativePayment(new NonNegativeAmount(1m), PaymentType.OnCredit), //External payment (Bank charges)
                        new NonNegativePayment(new NonNegativeAmount(1m), PaymentType.OnCredit), //External payment (Barter)
                        new NonNegativePayment(new NonNegativeAmount(1m), PaymentType.OnCredit), //External payment (Cash)
                        new NonNegativePayment(new NonNegativeAmount(1m), PaymentType.OnCredit), //External payment (Cheque)
                        new NonNegativePayment(new NonNegativeAmount(1m), PaymentType.OnCredit), //External payment (Chèque vacances)
                        new NonNegativePayment(new NonNegativeAmount(1m), PaymentType.OnCredit), //External payment (Comission)
                        new NonNegativePayment(new NonNegativeAmount(1m), PaymentType.OnCredit), //External payment (Complimentary)
                        new NonNegativePayment(new NonNegativeAmount(1m), PaymentType.OnCredit), //External payment (Credit card)
                        new NonNegativePayment(new NonNegativeAmount(1m), PaymentType.OnCredit), //External payment (Cross settlement)
                        new NonNegativePayment(new NonNegativeAmount(1m), PaymentType.OnCredit), //External payment (Exchange rate difference)
                        new NonNegativePayment(new NonNegativeAmount(1m), PaymentType.OnCredit), //External payment (Exchange rounding difference)
                        new NonNegativePayment(new NonNegativeAmount(1m), PaymentType.OnCredit), //External payment (Gift card)
                        new NonNegativePayment(new NonNegativeAmount(1m), PaymentType.OnCredit), //External payment (iDeal)
                        new NonNegativePayment(new NonNegativeAmount(1m), PaymentType.OnCredit), //External payment (Invoice)
                        new NonNegativePayment(new NonNegativeAmount(1m), PaymentType.OnCredit), //External payment (Loyalty points)
                        new NonNegativePayment(new NonNegativeAmount(1m), PaymentType.OnCredit), //External payment (PayPal)
                        new NonNegativePayment(new NonNegativeAmount(1m), PaymentType.OnCredit), //External payment (Prepayment)
                        new NonNegativePayment(new NonNegativeAmount(1m), PaymentType.OnCredit), //External payment (Reseller)
                        new NonNegativePayment(new NonNegativeAmount(1m), PaymentType.OnCredit), //External payment (Unspecified)
                        new NonNegativePayment(new NonNegativeAmount(1m), PaymentType.OnCredit)  //External payment (Wife transfer)
                    }
                )
            );
        }

        /// <summary>
        /// Test case for bills: #1.5
        /// </summary>
        private static ISequentialEnumerable<Invoice> SimplifiedInvoiceForCustomer()
        {
            return SequentialEnumerable.FromPreordered(
                new NonNegativeInvoice(
                    issuer: new LocalCounterpart(new GreekTaxIdentifier(UserVatNumber)),
                    billType: BillType.SimplifiedInvoice,
                    header: new InvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, currencyCode: new CurrencyCode("EUR")),
                    revenueItems: new List<NonNegativeRevenue>
                    {
                        new NonNegativeRevenue(new NonNegativeAmount(88.50m), new NonNegativeAmount(11.50m), TaxType.Vat13, ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome)
                    },
                    payments: new List<NonNegativePayment>
                    {
                        new NonNegativePayment(new NonNegativeAmount(100m), PaymentType.OnCredit)
                    }
                )
            );
        }

        /// <summary>
        /// Test case for bills: #1.6
        /// </summary>
        private static ISequentialEnumerable<Invoice> InvoiceWithConversionRate()
        {
            return SequentialEnumerable.FromPreordered(
                new NonNegativeInvoice(
                    issuer: new LocalCounterpart(new GreekTaxIdentifier(UserVatNumber)),
                    billType: BillType.RetailSalesReceipt,
                    header: new InvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, currencyCode: new CurrencyCode("EUR")),
                    revenueItems: new List<NonNegativeRevenue>
                    {
                        new NonNegativeRevenue(new NonNegativeAmount(4.03m), new NonNegativeAmount(0.97m), TaxType.Vat24, ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome, new PositiveInt(1)),
                        new NonNegativeRevenue(new NonNegativeAmount(4.03m), new NonNegativeAmount(0.97m), TaxType.Vat24, ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome, new PositiveInt(2))
                    },
                    payments: new List<NonNegativePayment>
                    {
                        new NonNegativePayment(new NonNegativeAmount(10m), PaymentType.Cash)
                    }
                )
            );
        }

        /// <summary>
        /// Test case for bills: #1.7
        /// </summary>
        private static ISequentialEnumerable<Invoice> InvoiceWithRebateOfItems()
        {
            return SequentialEnumerable.FromPreordered(
                new NonNegativeInvoice(
                    issuer: new LocalCounterpart(new GreekTaxIdentifier(UserVatNumber)),
                    billType: BillType.OtherIncomeAdjustmentRegularisationEntriesAccountingBase,
                    header: new InvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, currencyCode: new CurrencyCode("EUR")),
                    revenueItems: new List<NonNegativeRevenue>
                    {
                        new NonNegativeRevenue(new NonNegativeAmount(10m), new NonNegativeAmount(0m), TaxType.WithoutVat, ClassificationType.CreditExchangeDifferences, ClassificationCategory.OtherIncomeAdjustmentAndRegularisationEntries)
                    }
                )
            );
        }

        /// <summary>
        /// Test case for bills: #2.2 with various order item types(product order, service charge, cancellation fee, deposit, space order
        /// </summary>
        /// <returns></returns>
        private static ISequentialEnumerable<Invoice> InvoiceWithVariousOrderItemTypes()
        {
            return SequentialEnumerable.FromPreordered(
                new NonNegativeInvoice(
                    issuer: new LocalCounterpart(new GreekTaxIdentifier(UserVatNumber)),
                    billType: BillType.RetailSalesReceipt,
                    header: new InvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, currencyCode: new CurrencyCode("EUR")),
                    revenueItems: new List<NonNegativeRevenue>
                    {
                        //Night 9/16/2020
                        new NonNegativeRevenue(new NonNegativeAmount(88.50m), new NonNegativeAmount(11.50m), TaxType.Vat13, ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProvisionOfServicesIncome, new PositiveInt(1)),
                        //Service / Product
                        new NonNegativeRevenue(new NonNegativeAmount(5.00m), new NonNegativeAmount(0.00m), TaxType.Vat0, ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome, new PositiveInt(2),
                            VatExemptionType.VatIncludedArticle43),
                        //Garage
                        new NonNegativeRevenue(new NonNegativeAmount(16.13m), new NonNegativeAmount(3.87m), TaxType.Vat24, ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome, new PositiveInt(3)),
                        //CancellationFee
                        new NonNegativeRevenue(new NonNegativeAmount(100.00m), new NonNegativeAmount(0.00m), TaxType.Vat0, ClassificationType.OtherSalesOfGoodsAndServices, ClassificationCategory.OtherIncomeAndProfits, new PositiveInt(4),
                            VatExemptionType.VatIncludedArticle44),
                        //Deposit
                        new NonNegativeRevenue(new NonNegativeAmount(100.00m), new NonNegativeAmount(0.00m), TaxType.Vat0, ClassificationType.OtherSalesOfGoodsAndServices, ClassificationCategory.OtherIncomeAndProfits, new PositiveInt(5),
                            VatExemptionType.VatIncludedArticle46),
                        //Deposit
                        new NonNegativeRevenue(new NonNegativeAmount(100.00m), new NonNegativeAmount(0.00m), TaxType.Vat0, ClassificationType.OtherSalesOfGoodsAndServices, ClassificationCategory.OtherIncomeAndProfits, new PositiveInt(6),
                            VatExemptionType.WithoutVatArticle13)
                    },
                    payments: new List<NonNegativePayment>
                    {
                        new NonNegativePayment(new NonNegativeAmount(425.00m), PaymentType.Cash),
                    }
                )
            );
        }

        /// <summary>
        /// Test case for bills: #2.5
        /// </summary>
        private static ISequentialEnumerable<Invoice> InvoiceForCompanyWithoutDetails()
        {
            return SequentialEnumerable.FromPreordered(
                new NonNegativeInvoice(
                    issuer: new LocalCounterpart(new GreekTaxIdentifier(UserVatNumber)),
                    billType: BillType.SimplifiedInvoice,
                    header: new InvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, currencyCode: new CurrencyCode("EUR")),
                    revenueItems: new List<NonNegativeRevenue>
                    {
                        new NonNegativeRevenue(new NonNegativeAmount(88.50m), new NonNegativeAmount(11.50m), TaxType.Vat13, ClassificationType.OtherSalesOfGoodsAndServices, ClassificationCategory.OtherIncomeAndProfits)
                    },
                    payments: new List<NonNegativePayment>
                    {
                        new NonNegativePayment(new NonNegativeAmount(100m), PaymentType.Cash)
                    }
                )
            );
        }

        private static ISequentialEnumerable<Invoice> CreditInvoiceWithNegativeAmounts()
        {
            return SequentialEnumerable.FromPreordered(
                new NegativeInvoice(
                    issuer: new LocalCounterpart(new GreekTaxIdentifier(UserVatNumber)),
                    header: new InvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, currencyCode: new CurrencyCode("EUR")),
                    revenueItems: new List<NegativeRevenue>
                    {
                        new NegativeRevenue(new NegativeAmount(-53.65m), new NegativeAmount(-12.88m), TaxType.Vat6, ClassificationType.OtherSalesOfGoodsAndServices, ClassificationCategory.ProductSaleIncome)
                    },
                    payments: new List<NegativePayment>
                    {
                        new NegativePayment(new NegativeAmount(-66.53m), PaymentType.Cash)
                    },
                    counterpart: new LocalCounterpart(new GreekTaxIdentifier("090701900"), new NonNegativeInt(0), address: new Address(postalCode: new NonEmptyString("12"), city: new NonEmptyString("City")))
                )
            );
        }
    }
}
