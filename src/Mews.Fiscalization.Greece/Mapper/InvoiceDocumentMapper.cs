using Mews.Fiscalization.Greece.Extensions;
using Mews.Fiscalization.Greece.Model;
using System;
using System.Linq;
using TaxType = Mews.Fiscalization.Greece.Model.TaxType;

namespace Mews.Fiscalization.Greece.Mapper
{
    public class InvoiceDocumentMapper
    {
        public InvoiceDocumentMapper(InvoiceDocument invoiceDocument)
        {
            InvoiceDocument = invoiceDocument;
        }

        private InvoiceDocument InvoiceDocument { get; }

        public Dto.Xsd.InvoicesDoc GetInvoiceDoc()
        {
            return new Dto.Xsd.InvoicesDoc
            {
                Invoices = InvoiceDocument.InvoiceRecords.Select(invoiceRecord => GetInvoice(invoiceRecord)).ToArray()
            };
        }

        private Dto.Xsd.Invoice GetInvoice(Invoice invoice)
        {
            return new Dto.Xsd.Invoice
            {
                InvoiceMarkSpecified = invoice.InvoiceRegistrationNumber.IsDefined(),
                InvoiceMark = invoice.InvoiceRegistrationNumber.GetOrDefault(),
                InvoiceCancelationMarkSpecified = invoice.CanceledByInvoiceRegistrationNumber.IsDefined(),
                InvoiceCancelationMark = invoice.CanceledByInvoiceRegistrationNumber.GetOrDefault(),
                InvoiceId = invoice.InvoiceIdentifier.GetOrDefault(),
                InvoiceIssuer = GetInvoiceParty(invoice.Issuer),
                InvoiceCounterpart = GetInvoiceParty(invoice.Counterpart),
                InvoiceSummary = GetInvoiceSummary(invoice),
                InvoiceHeader = GetInvoiceHeader(invoice),
                InvoiceDetails = invoice.InvoiceDetails.Select(invoiceDetail => GetInvoiceDetail(invoiceDetail)).ToArray(),
                PaymentMethods = invoice.PaymentMethods?.Select(paymentMethod => new Dto.Xsd.PaymentMethod
                {
                    Amount = paymentMethod.Amount.Value,
                    PaymentMethodType = MapPaymentMethodType(paymentMethod.PaymentType)
                }).ToArray()
            };
        }

        private Dto.Xsd.InvoiceParty GetInvoiceParty(InvoiceParty invoiceRecordParty)
        {
            if (invoiceRecordParty != null)
            {
                return new Dto.Xsd.InvoiceParty
                {
                    Country = (Dto.Xsd.Country)Enum.Parse(typeof(Dto.Xsd.Country), invoiceRecordParty.CountryCode.Value, true),
                    Branch = invoiceRecordParty.Branch.Value,
                    Name = invoiceRecordParty.Name.GetOrDefault(),
                    VatNumber = invoiceRecordParty.TaxNumber.Value,
                    Address = GetAddress(invoiceRecordParty.Address)
                };
            }

            return null;
        }

        private Dto.Xsd.Address GetAddress(Address address)
        {
            if (address != null)
            {
                return new Dto.Xsd.Address
                {
                    City = address.City.Value,
                    Number = address.Number.GetOrDefault(),
                    PostalCode = address.PostalCode.Value,
                    Street = address.Street.GetOrDefault()
                };
            }

            return null;
        }

        private Dto.Xsd.InvoiceHeader GetInvoiceHeader(Invoice invoice)
        {
            var invoiceHeader = new Dto.Xsd.InvoiceHeader
            {
                InvoiceType = MapInvoiceType(invoice.InvoiceHeader.BillType),
                IssueDate = invoice.InvoiceHeader.InvoiceIssueDate,
                SerialNumber = invoice.InvoiceHeader.InvoiceSerialNumber.Value,
                Series = invoice.InvoiceHeader.InvoiceSeries.Value,
                CurrencySpecified = invoice.InvoiceHeader.CurrencyCode.IsDefined(),
                ExchangeRateSpecified = invoice.InvoiceHeader.ExchangeRate.IsDefined(),
                ExchangeRate = invoice.InvoiceHeader.ExchangeRate.GetOrDefault()
            };

            if (invoice.InvoiceHeader.CurrencyCode.IsDefined())
            {
                invoiceHeader.Currency = (Dto.Xsd.Currency)Enum.Parse(typeof(Dto.Xsd.Currency), invoice.InvoiceHeader.CurrencyCode.Value, true);
            }

            return invoiceHeader;
        }

        private Dto.Xsd.InvoiceDetail GetInvoiceDetail(RevenueItem revenueItem)
        {
            var invoiceDetail = new Dto.Xsd.InvoiceDetail
            {
                LineNumber = revenueItem.LineNumber.Value,
                NetValue = revenueItem.NetValue.Value,
                VatAmount = revenueItem.VatValue.Value,
                VatCategory = MapVatCategory(revenueItem.TaxType),
                IncomeClassification = revenueItem.InvoiceRecordIncomeClassification.Select(invoiceIncomeClassification => GetIncomeClassification(invoiceIncomeClassification)).ToArray()
            };

            if (revenueItem.VatExemption.HasValue)
            {
                invoiceDetail.VatExemptionCategory = MapVatExemptionCategory(revenueItem.VatExemption.Value);
                invoiceDetail.VatExemptionCategorySpecified = true;
            }

            if (revenueItem.CityTax != null)
            {
                invoiceDetail.OtherTaxesCategory = MapOtherTaxCategory(revenueItem.CityTax.CityTaxType);
                invoiceDetail.OtherTaxesAmount = revenueItem.CityTax.CityTaxAmount.Value;
                invoiceDetail.OtherTaxesCategorySpecified = true;
                invoiceDetail.OtherTaxesAmountSpecified = true;
            }

            return invoiceDetail;
        }

        private Dto.Xsd.InvoiceSummary GetInvoiceSummary(Invoice invoice)
        {
            var invoiceSummary = new Dto.Xsd.InvoiceSummary
            {
                TotalNetValue = invoice.InvoiceSummary.TotalNetValue.Value,
                TotalVatAmount = invoice.InvoiceSummary.TotalVatValue.Value,
                TotalGrossValue = invoice.InvoiceSummary.TotalGrossValue.Value,
                IncomeClassification = invoice.InvoiceSummary.InvoiceRecordIncomeClassification.Select(invoiceIncomeClassification => GetIncomeClassification(invoiceIncomeClassification)).ToArray(),
            };

            if (invoice.InvoiceSummary.TotalOtherTaxesAmount != null)
            {
                invoiceSummary.TotalOtherTaxesAmount = invoice.InvoiceSummary.TotalOtherTaxesAmount.Value;
            }

            return invoiceSummary;
        }

        private Dto.Xsd.IncomeClassification GetIncomeClassification(ItemIncomeClassification invoiceRecordIncomeClassification)
        {
            return new Dto.Xsd.IncomeClassification
            {
                Amount = invoiceRecordIncomeClassification.Amount.Value,
                ClassificationCategory = MapIncomeClassificationCategory(invoiceRecordIncomeClassification.ClassificationCategory),
                ClassificationType = MapIncomeClassificationType(invoiceRecordIncomeClassification.ClassificationType)
            };
        }

        private Dto.Xsd.InvoiceType MapInvoiceType(BillType billType)
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
                default:
                    throw new ArgumentException($"Cannot map BillType {billType} to InvoiceType.");
            }
        }

        private Dto.Xsd.IncomeClassificationCategory MapIncomeClassificationCategory(ClassificationCategory classificationCategory)
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
                    throw new ArgumentException($"Cannot map ClassificationCategory {classificationCategory} to IncomeClassificationCategory.");
            }
        }

        private Dto.Xsd.IncomeClassificationType MapIncomeClassificationType(ClassificationType classificationType)
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
                    throw new ArgumentException($"Cannot map ClassificationType {classificationType} to IncomeClassificationType.");
            }
        }

        private Dto.Xsd.VatCategory MapVatCategory(TaxType taxType)
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
                    throw new ArgumentException($"Cannot map TaxType {taxType} to VatCategory.");
            }
        }

        private Dto.Xsd.PaymentMethodType MapPaymentMethodType(PaymentType paymentType)
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
                    throw new ArgumentException($"Cannot map PaymentType {paymentType} to PaymentMethodType.");
            }
        }

        private Dto.Xsd.VatExemptionCategory MapVatExemptionCategory(VatExemption vatExemption)
        {
            switch(vatExemption)
            {
                case VatExemption.VatIncludedArticle43:
                    return Dto.Xsd.VatExemptionCategory.VatIncludedArticle43;
                case VatExemption.VatIncludedArticle44:
                    return Dto.Xsd.VatExemptionCategory.VatIncludedArticle44;
                case VatExemption.VatIncludedArticle45:
                    return Dto.Xsd.VatExemptionCategory.VatIncludedArticle45;
                case VatExemption.VatIncludedArticle46:
                    return Dto.Xsd.VatExemptionCategory.VatIncludedArticle46;
                case VatExemption.WithoutVatArticle13:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle13;
                case VatExemption.WithoutVatArticle14:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle14;
                case VatExemption.WithoutVatArticle16:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle16;
                case VatExemption.WithoutVatArticle19:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle19;
                case VatExemption.WithoutVatArticle22:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle22;
                case VatExemption.WithoutVatArticle24:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle24;
                case VatExemption.WithoutVatArticle25:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle25;
                case VatExemption.WithoutVatArticle26:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle26;
                case VatExemption.WithoutVatArticle27:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle27;
                case VatExemption.WithoutVatArticle271CSeagoingVessels:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle271CSeagoingVessels;
                case VatExemption.WithoutVatArticle27SeagoingVessels:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle27SeagoingVessels;
                case VatExemption.WithoutVatArticle28:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle28;
                case VatExemption.WithoutVatArticle3:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle3;
                case VatExemption.WithoutVatArticle39:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle39;
                case VatExemption.WithoutVatArticle39A:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle39A;
                case VatExemption.WithoutVatArticle40:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle40;
                case VatExemption.WithoutVatArticle41:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle41;
                case VatExemption.WithoutVatArticle47:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle47;
                case VatExemption.WithoutVatArticle5:
                    return Dto.Xsd.VatExemptionCategory.WithoutVatArticle5;
                default:
                    throw new ArgumentException($"Cannot map VatExemption {vatExemption} to Dto.Xsd.VatExemptionCategory.");
            }
        }

        private Dto.Xsd.OtherTaxCategory MapOtherTaxCategory(CityTaxType cityTaxType)
        {
            switch(cityTaxType)
            {
                case CityTaxType.Hotels1Or2Stars:
                    return Dto.Xsd.OtherTaxCategory.Hotels1Or2Stars;
                case CityTaxType.Hotels3Stars:
                    return Dto.Xsd.OtherTaxCategory.Hotels3Stars;
                case CityTaxType.Hotels4Stars:
                    return Dto.Xsd.OtherTaxCategory.Hotels4Stars;
                case CityTaxType.Hotels5Stars:
                    return Dto.Xsd.OtherTaxCategory.Hotels5Stars;
                case CityTaxType.RoomsOrApartments:
                    return Dto.Xsd.OtherTaxCategory.RoomsOrApartments;
                default:
                    throw new ArgumentException($"Cannot map CityTaxType {cityTaxType} to OtherTaxCategory.");
            }
        }
    }
}
