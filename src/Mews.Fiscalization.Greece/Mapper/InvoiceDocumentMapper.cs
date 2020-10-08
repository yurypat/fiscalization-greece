using Mews.Fiscalization.Greece.Extensions;
using Mews.Fiscalization.Greece.Model;
using System;
using System.Linq;
using Mews.Fiscalization.Greece.Model.Collections;
using Mews.Fiscalization.Greece.Model.Types;
using TaxType = Mews.Fiscalization.Greece.Model.TaxType;

namespace Mews.Fiscalization.Greece.Mapper
{
    public static class InvoiceDocumentMapper
    {
        public static Dto.Xsd.InvoicesDoc GetInvoiceDoc(ISequentialEnumerable<Invoice> invoices)
        {
            return new Dto.Xsd.InvoicesDoc
            {
                Invoices = invoices.Items.Select(invoice => GetInvoice(invoice)).ToArray()
            };
        }

        private static Dto.Xsd.Invoice GetInvoice(Invoice invoice)
        {
            return new Dto.Xsd.Invoice
            {
                InvoiceMarkSpecified = invoice.InvoiceRegistrationNumber.IsNotNull(),
                InvoiceMark = invoice.InvoiceRegistrationNumber?.Value ?? 0,
                InvoiceCancelationMarkSpecified = invoice.CanceledByInvoiceRegistrationNumber.IsNotNull(),
                InvoiceCancelationMark = invoice.CanceledByInvoiceRegistrationNumber?.Value  ?? 0,
                InvoiceId = invoice.Header.InvoiceIdentifier,
                InvoiceIssuer = GetInvoiceParty(invoice.Issuer),
                InvoiceCounterpart = GetInvoiceParty(invoice.Counterpart),
                InvoiceSummary = GetInvoiceSummary(invoice),
                InvoiceHeader = GetInvoiceHeader(invoice),
                InvoiceDetails = invoice.RevenueItems.Select(invoiceDetail => GetInvoiceDetail(invoiceDetail)).ToArray(),
                PaymentMethods = invoice.Payments?.Select(paymentMethod => new Dto.Xsd.PaymentMethod
                {
                    Amount = paymentMethod.Amount.Value,
                    PaymentMethodType = MapPaymentMethodType(paymentMethod.PaymentType)
                }).ToArray()
            };
        }

        private static Dto.Xsd.InvoiceParty GetInvoiceParty(Counterpart counterpart)
        {
            if (counterpart != null)
            {
                return new Dto.Xsd.InvoiceParty
                {
                    Country = (Dto.Xsd.Country)Enum.Parse(typeof(Dto.Xsd.Country), counterpart.CountryCode.Value, true),
                    Branch = counterpart.Branch.Value,
                    Name = counterpart.Name,
                    VatNumber = counterpart.TaxIdentifier.Value,
                    Address = GetAddress(counterpart.Address)
                };
            }

            return null;
        }

        private static Dto.Xsd.Address GetAddress(Address address)
        {
            if (address != null)
            {
                return new Dto.Xsd.Address
                {
                    City = address.City.Value,
                    Number = address.Number,
                    PostalCode = address.PostalCode.Value,
                    Street = address.Street
                };
            }

            return null;
        }

        private static Dto.Xsd.InvoiceHeader GetInvoiceHeader(Invoice invoice)
        {
            var invoiceHeader = new Dto.Xsd.InvoiceHeader
            {
                InvoiceType = MapInvoiceType(invoice.BillType),
                IssueDate = invoice.Header.InvoiceIssueDate,
                SerialNumber = invoice.Header.InvoiceSerialNumber.Value,
                Series = invoice.Header.InvoiceSeries.Value,
                CurrencySpecified = invoice.Header.CurrencyCode.IsNotNull(),
                ExchangeRateSpecified = invoice.Header.ExchangeRate.IsNotNull(),
                ExchangeRate = invoice.Header.ExchangeRate?.Value ?? 0,
                CorrelatedInvoicesSpecified = invoice.CorrelatedInvoice.IsNotNull(),
                CorrelatedInvoices = invoice.CorrelatedInvoice?.Value ?? 0

            };

            if (invoice.Header.CurrencyCode.IsNotNull())
            {
                invoiceHeader.Currency = (Dto.Xsd.Currency)Enum.Parse(typeof(Dto.Xsd.Currency), invoice.Header.CurrencyCode.Value, true);
            }

            return invoiceHeader;
        }

        private static Dto.Xsd.InvoiceDetail GetInvoiceDetail(Revenue revenueItem)
        {
            var invoiceDetail = new Dto.Xsd.InvoiceDetail
            {
                LineNumber = revenueItem.LineNumber.Value,
                NetValue = revenueItem.NetValue.Value,
                VatAmount = revenueItem.VatValue.Value,
                VatCategory = MapVatCategory(revenueItem.TaxType),
                IncomeClassification = new [] { GetIncomeClassification(revenueItem) }
            };

            if (revenueItem.VatExemption.HasValue)
            {
                invoiceDetail.VatExemptionCategory = MapVatExemptionCategory(revenueItem.VatExemption.Value);
                invoiceDetail.VatExemptionCategorySpecified = true;
            }

            return invoiceDetail;
        }

        private static Dto.Xsd.InvoiceSummary GetInvoiceSummary(Invoice invoice)
        {
            var invoiceSummary = new Dto.Xsd.InvoiceSummary
            {
                TotalNetValue = invoice.RevenueItems.Sum(x => x.NetValue.Value),
                TotalVatAmount = invoice.RevenueItems.Sum(x => x.VatValue.Value)
            };

            invoiceSummary.IncomeClassification = invoice.RevenueItems.GroupBy(
                keySelector: m => (m.ClassificationCategory, m.ClassificationType),
                resultSelector: (key, revenueItems) => new Dto.Xsd.IncomeClassification
                {
                    ClassificationCategory = MapIncomeClassificationCategory(key.ClassificationCategory),
                    ClassificationType = MapIncomeClassificationType(key.ClassificationType),
                    Amount = revenueItems.Sum(i => i.NetValue.Value)
                }
            ).ToArray();


            invoiceSummary.TotalGrossValue = invoiceSummary.TotalNetValue + invoiceSummary.TotalVatAmount + invoiceSummary.TotalOtherTaxesAmount;

            return invoiceSummary;
        }

        private static Dto.Xsd.IncomeClassification GetIncomeClassification(Revenue revenue)
        {
            return new Dto.Xsd.IncomeClassification
            {
                Amount = revenue.NetValue.Value,
                ClassificationCategory = MapIncomeClassificationCategory(revenue.ClassificationCategory),
                ClassificationType = MapIncomeClassificationType(revenue.ClassificationType)
            };
        }

        private static Dto.Xsd.InvoiceType MapInvoiceType(BillType billType)
        {
            switch (billType)
            {
                case BillType.RetailSalesReceipt:
                    return Dto.Xsd.InvoiceType.RetailSalesReceipt;
                case BillType.SimplifiedInvoice:
                    return Dto.Xsd.InvoiceType.SimplifiedInvoice;
                case BillType.SalesInvoice:
                    return Dto.Xsd.InvoiceType.SalesInvoice;
                case BillType.SalesInvoiceIntraCommunitySupplies:
                    return Dto.Xsd.InvoiceType.SalesInvoiceIntraCommunitySupplies;
                case BillType.SalesInvoiceThirdCountrySupplies:
                    return Dto.Xsd.InvoiceType.SalesInvoiceThirdCountrySupplies;
                case BillType.OtherIncomeAdjustmentRegularisationEntriesAccountingBase:
                    return Dto.Xsd.InvoiceType.OtherIncomeAdjustmentRegularisationEntriesAccountingBase;
                case BillType.CreditInvoice:
                    return Dto.Xsd.InvoiceType.CreditInvoiceAssociated;
                case BillType.CreditInvoiceNonAssociated:
                    return Dto.Xsd.InvoiceType.CreditInvoiceNonAssociated;
                default:
                    throw new ArgumentException($"Cannot map BillType {billType} to {nameof(Dto.Xsd.InvoiceType)}.");
            }
        }

        private static Dto.Xsd.IncomeClassificationCategory MapIncomeClassificationCategory(ClassificationCategory classificationCategory)
        {
            switch (classificationCategory)
            {
                case ClassificationCategory.ProductSaleIncome:
                    return Dto.Xsd.IncomeClassificationCategory.ProductSaleIncome;
                case ClassificationCategory.ProvisionOfServicesIncome:
                    return Dto.Xsd.IncomeClassificationCategory.ProvisionOfServicesIncome;
                case ClassificationCategory.OtherIncomeAndProfits:
                    return Dto.Xsd.IncomeClassificationCategory.OtherIncomeAndProfits;
                case ClassificationCategory.OtherIncomeAdjustmentAndRegularisationEntries:
                    return Dto.Xsd.IncomeClassificationCategory.OtherIncomeAdjustmentAndRegularisationEntries;
                default:
                    throw new ArgumentException($"Cannot map ClassificationCategory {classificationCategory} to {nameof(Dto.Xsd.IncomeClassificationCategory)}.");
            }
        }

        private static Dto.Xsd.IncomeClassificationType MapIncomeClassificationType(ClassificationType classificationType)
        {
            switch (classificationType)
            {
                case ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele:
                    return Dto.Xsd.IncomeClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele;
                case ClassificationType.OtherSalesOfGoodsAndServices:
                    return Dto.Xsd.IncomeClassificationType.OtherSalesOfGoodsAndServices;
                case ClassificationType.OtherOrdinaryIncome:
                    return Dto.Xsd.IncomeClassificationType.OtherOrdinaryIncome;
                case ClassificationType.CreditExchangeDifferences:
                    return Dto.Xsd.IncomeClassificationType.CreditExchangeDifferences;
                case ClassificationType.IntraCommunityForeignSalesOfGoodsAndServices:
                    return Dto.Xsd.IncomeClassificationType.IntraCommunityForeignSalesOfGoodsAndServices;
                case ClassificationType.ThirdCountryForeignSalesOfGoodsAndServices:
                    return Dto.Xsd.IncomeClassificationType.ThirdCountryForeignSalesOfGoodsAndServices;
                default:
                    throw new ArgumentException($"Cannot map ClassificationType {classificationType} to {nameof(Dto.Xsd.IncomeClassificationType)}.");
            }
        }

        private static Dto.Xsd.VatCategory MapVatCategory(TaxType taxType)
        {
            switch (taxType)
            {
                case TaxType.Vat24:
                    return Dto.Xsd.VatCategory.Vat24;
                case TaxType.Vat13:
                    return Dto.Xsd.VatCategory.Vat13;
                case TaxType.Vat6:
                    return Dto.Xsd.VatCategory.Vat6;
                case TaxType.Vat0:
                    return Dto.Xsd.VatCategory.Vat0;
                case TaxType.WithoutVat:
                    return Dto.Xsd.VatCategory.WithoutVat;
                default:
                    throw new ArgumentException($"Cannot map TaxType {taxType} to {nameof(Dto.Xsd.VatCategory)}.");
            }
        }

        private static Dto.Xsd.PaymentMethodType MapPaymentMethodType(PaymentType paymentType)
        {
            switch (paymentType)
            {
                case PaymentType.DomesticPaymentsAccountNumber:
                    return Dto.Xsd.PaymentMethodType.DomesticPaymentsAccountNumber;
                case PaymentType.ForeignMethodsAccountNumber:
                    return Dto.Xsd.PaymentMethodType.ForeignMethodsAccountNumber;
                case PaymentType.Check:
                    return Dto.Xsd.PaymentMethodType.Check;
                case PaymentType.OnCredit:
                    return Dto.Xsd.PaymentMethodType.OnCredit;
                case PaymentType.Cash:
                    return Dto.Xsd.PaymentMethodType.Cash;
                default:
                    throw new ArgumentException($"Cannot map PaymentType {paymentType} to {nameof(Dto.Xsd.PaymentMethodType)}.");
            }
        }

        private static Dto.Xsd.VatExemptionCategory MapVatExemptionCategory(VatExemptionType vatExemption)
        {
            switch (vatExemption)
            {
                case VatExemptionType.VatIncludedArticle43:
                    return Dto.Xsd.VatExemptionCategory.VatIncludedArticle43;
                case VatExemptionType.VatIncludedArticle44:
                    return Dto.Xsd.VatExemptionCategory.VatIncludedArticle44;
                case VatExemptionType.VatIncludedArticle45:
                    return Dto.Xsd.VatExemptionCategory.VatIncludedArticle45;
                case VatExemptionType.VatIncludedArticle46:
                    return Dto.Xsd.VatExemptionCategory.VatIncludedArticle46;
                case VatExemptionType.WithoutVatArticle13:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle13;
                case VatExemptionType.WithoutVatArticle14:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle14;
                case VatExemptionType.WithoutVatArticle16:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle16;
                case VatExemptionType.WithoutVatArticle19:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle19;
                case VatExemptionType.WithoutVatArticle22:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle22;
                case VatExemptionType.WithoutVatArticle24:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle24;
                case VatExemptionType.WithoutVatArticle25:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle25;
                case VatExemptionType.WithoutVatArticle26:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle26;
                case VatExemptionType.WithoutVatArticle27:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle27;
                case VatExemptionType.WithoutVatArticle271CSeagoingVessels:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle271CSeagoingVessels;
                case VatExemptionType.WithoutVatArticle27SeagoingVessels:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle27SeagoingVessels;
                case VatExemptionType.WithoutVatArticle28:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle28;
                case VatExemptionType.WithoutVatArticle3:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle3;
                case VatExemptionType.WithoutVatArticle39:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle39;
                case VatExemptionType.WithoutVatArticle39A:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle39A;
                case VatExemptionType.WithoutVatArticle40:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle40;
                case VatExemptionType.WithoutVatArticle41:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle41;
                case VatExemptionType.WithoutVatArticle47:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle47;
                case VatExemptionType.WithoutVatArticle5:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle5;
                default:
                    throw new ArgumentException($"Cannot map VatExemption {vatExemption} to Dto.Xsd.{nameof(Dto.Xsd.VatExemptionCategory)}.");
            }
        }
    }
}
