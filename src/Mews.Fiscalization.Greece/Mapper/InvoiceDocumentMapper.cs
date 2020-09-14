using Mews.Fiscalization.Greece.Dto.Xsd;
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

        public InvoicesDoc GetInvoiceDoc()
        {
            return new InvoicesDoc
            {
                Invoices = InvoiceDocument.InvoiceRecords.Select(invoiceRecord => GetInvoice(invoiceRecord)).ToArray()
            };
        }

        private Invoice GetInvoice(InvoiceRecord invoiceRecord)
        {
            return new Invoice
            {
                InvoiceMarkSpecified = invoiceRecord.InvoiceRegistrationNumber.IsDefined(),
                InvoiceMark = invoiceRecord.InvoiceRegistrationNumber.GetOrDefault(),
                InvoiceCancelationMarkSpecified = invoiceRecord.CanceledByInvoiceRegistrationNumber.IsDefined(),
                InvoiceCancelationMark = invoiceRecord.CanceledByInvoiceRegistrationNumber.GetOrDefault(),
                InvoiceId = invoiceRecord.InvoiceIdentifier.GetOrDefault(),
                InvoiceIssuer = GetInvoiceParty(invoiceRecord.Issuer),
                InvoiceCounterpart = GetInvoiceParty(invoiceRecord.Counterpart),
                InvoiceSummary = GetInvoiceSummary(invoiceRecord),
                InvoiceHeader = GetInvoiceHeader(invoiceRecord),
                InvoiceDetails = invoiceRecord.InvoiceDetails.Select(invoiceDetail => GetInvoiceDetail(invoiceDetail)).ToArray(),
                PaymentMethods = invoiceRecord.PaymentMethods?.Select(paymentMethod => new PaymentMethod
                {
                    Amount = paymentMethod.Amount.Value,
                    PaymentMethodType = MapPaymentMethodType(paymentMethod.PaymentType)
                }).ToArray()
            };
        }

        private InvoiceParty GetInvoiceParty(InvoiceRecordParty invoiceRecordParty)
        {
            if (invoiceRecordParty != null)
            {
                return new InvoiceParty
                {
                    Country = (Country)Enum.Parse(typeof(Country), invoiceRecordParty.CountryCode.Value, true),
                    Branch = invoiceRecordParty.Branch.Value,
                    Name = invoiceRecordParty.Name.GetOrDefault(),
                    VatNumber = invoiceRecordParty.TaxNumber.Value,
                    Address = GetAddress(invoiceRecordParty.Address)
                };
            }

            return null;
        }

        private Address GetAddress(InvoiceRecordPartyAddress invoiceRecordPartyAddress)
        {
            if (invoiceRecordPartyAddress != null)
            {
                return new Address
                {
                    City = invoiceRecordPartyAddress.City.Value,
                    Number = invoiceRecordPartyAddress.Number.GetOrDefault(),
                    PostalCode = invoiceRecordPartyAddress.PostalCode.Value,
                    Street = invoiceRecordPartyAddress.Street.GetOrDefault()
                };
            }

            return null;
        }

        private InvoiceHeader GetInvoiceHeader(InvoiceRecord invoiceRecord)
        {
            var invoiceHeader = new InvoiceHeader
            {
                InvoiceType = MapInvoiceType(invoiceRecord.InvoiceHeader.BillType),
                IssueDate = invoiceRecord.InvoiceHeader.InvoiceIssueDate,
                SerialNumber = invoiceRecord.InvoiceHeader.InvoiceSerialNumber.Value,
                Series = invoiceRecord.InvoiceHeader.InvoiceSeries.Value,
                CurrencySpecified = invoiceRecord.InvoiceHeader.CurrencyCode.IsDefined(),
                ExchangeRateSpecified = invoiceRecord.InvoiceHeader.ExchangeRate.IsDefined(),
                ExchangeRate = invoiceRecord.InvoiceHeader.ExchangeRate.GetOrDefault()
            };

            if (invoiceRecord.InvoiceHeader.CurrencyCode.IsDefined())
            {
                invoiceHeader.Currency = (Currency)Enum.Parse(typeof(Currency), invoiceRecord.InvoiceHeader.CurrencyCode.Value, true);
            }

            return invoiceHeader;
        }

        private InvoiceDetail GetInvoiceDetail(InvoiceRecordDetail invoiceRecordDetail)
        {
            var invoiceDetail = new InvoiceDetail
            {
                LineNumber = invoiceRecordDetail.LineNumber.Value,
                NetValue = invoiceRecordDetail.NetValue.Value,
                VatAmount = invoiceRecordDetail.VatAmount.Value,
                VatCategory = MapVatCategory(invoiceRecordDetail.TaxType),
                IncomeClassification = invoiceRecordDetail.InvoiceRecordIncomeClassification.Select(invoiceIncomeClassification => GetIncomeClassification(invoiceIncomeClassification)).ToArray(),
                DiscountOptionSpecified = invoiceRecordDetail.DiscountOption.IsDefined(),
                DiscountOption = invoiceRecordDetail.DiscountOption.GetOrDefault(),
            };

            if (invoiceRecordDetail.VatExemption.HasValue)
            {
                invoiceDetail.VatExemptionCategory = MapVatExemptionCategory(invoiceRecordDetail.VatExemption.Value);
                invoiceDetail.VatExemptionCategorySpecified = true;
            }

            return invoiceDetail;
        }

        private InvoiceSummary GetInvoiceSummary(InvoiceRecord invoiceRecord)
        {
            return new InvoiceSummary
            {
                TotalNetValue = invoiceRecord.InvoiceSummary.TotalNetValue.Value,
                TotalVatAmount = invoiceRecord.InvoiceSummary.TotalVatValue.Value,
                TotalGrossValue = invoiceRecord.InvoiceSummary.TotalGrossValue.Value,
                IncomeClassification = invoiceRecord.InvoiceSummary.InvoiceRecordIncomeClassification.Select(invoiceIncomeClassification => GetIncomeClassification(invoiceIncomeClassification)).ToArray(),
            };
        }

        private IncomeClassification GetIncomeClassification(InvoiceRecordIncomeClassification invoiceRecordIncomeClassification)
        {
            return new IncomeClassification
            {
                Amount = invoiceRecordIncomeClassification.Amount.Value,
                ClassificationCategory = MapIncomeClassificationCategory(invoiceRecordIncomeClassification.ClassificationCategory),
                ClassificationType = MapIncomeClassificationType(invoiceRecordIncomeClassification.ClassificationType)
            };
        }

        private InvoiceType MapInvoiceType(BillType billType)
        {
            switch (billType)
            {
                case BillType.RetailSalesReceipt:
                    return InvoiceType.RetailSalesReceipt;
                case BillType.SimplifiedInvoice:
                    return InvoiceType.SimplifiedInvoice;
                case BillType.SalesInvoice:
                    return InvoiceType.SalesInvoice;
                case BillType.SalesInvoiceIntraCommunitySupplies:
                    return InvoiceType.SalesInvoiceIntraCommunitySupplies;
                case BillType.SalesInvoiceThirdCountrySupplies:
                    return InvoiceType.SalesInvoiceThirdCountrySupplies;
                case BillType.InvoiceIntraCommunityServicesReceipt:
                    return InvoiceType.InvoiceIntraCommunityServicesReceipt;
                case BillType.InvoiceThirdCountryServicesReceipt:
                    return InvoiceType.InvoiceThirdCountryServicesReceipt;
                default:
                    throw new ArgumentException($"Cannot map BillType {billType} to InvoiceType.");
            }
        }

        private IncomeClassificationCategory MapIncomeClassificationCategory(ClassificationCategory classificationCategory)
        {
            switch (classificationCategory)
            {
                case ClassificationCategory.ProductSaleIncome:
                    return IncomeClassificationCategory.ProductSaleIncome;
                case ClassificationCategory.ProvisionOfServicesIncome:
                    return IncomeClassificationCategory.ProvisionOfServicesIncome;
                case ClassificationCategory.OtherIncomeAndProfits:
                    return IncomeClassificationCategory.OtherIncomeAndProfits;
                default:
                    throw new ArgumentException($"Cannot map ClassificationCategory {classificationCategory} to IncomeClassificationCategory.");
            }
        }

        private IncomeClassificationType MapIncomeClassificationType(ClassificationType classificationType)
        {
            switch (classificationType)
            {
                case ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele:
                    return IncomeClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele;
                case ClassificationType.RetailSalesOfGoodsAndServicesPursuantToArticle39A:
                    return IncomeClassificationType.RetailSalesOfGoodsAndServicesPursuantToArticle39A;
                case ClassificationType.OtherSalesOfGoodsAndServices:
                    return IncomeClassificationType.OtherSalesOfGoodsAndServices;
                case ClassificationType.OtherOrdinaryIncome:
                    return IncomeClassificationType.OtherOrdinaryIncome;
                case ClassificationType.CreditExchangeDifferences:
                    return IncomeClassificationType.CreditExchangeDifferences;
                case ClassificationType.IntraCommunityForeignSalesOfGoodsAndServices:
                    return IncomeClassificationType.IntraCommunityForeignSalesOfGoodsAndServices;
                case ClassificationType.ThirdCountryForeignSalesOfGoodsAndServices:
                    return IncomeClassificationType.ThirdCountryForeignSalesOfGoodsAndServices;
                default:
                    throw new ArgumentException($"Cannot map ClassificationType {classificationType} to IncomeClassificationType.");
            }
        }

        private VatCategory MapVatCategory(TaxType taxType)
        {
            switch (taxType)
            {
                case TaxType.Vat24:
                    return VatCategory.Vat24;
                case TaxType.Vat13:
                    return VatCategory.Vat13;
                case TaxType.Vat6:
                    return VatCategory.Vat6;
                case TaxType.Vat0:
                    return VatCategory.Vat0;
                case TaxType.WithoutVat:
                    return VatCategory.WithoutVat;
                default:
                    throw new ArgumentException($"Cannot map TaxType {taxType} to VatCategory.");
            }
        }

        private PaymentMethodType MapPaymentMethodType(PaymentType paymentType)
        {
            switch (paymentType)
            {
                case PaymentType.DomesticPaymentsAccountNumber:
                    return PaymentMethodType.DomesticPaymentsAccountNumber;
                case PaymentType.ForeignMethodsAccountNumber:
                    return PaymentMethodType.ForeignMethodsAccountNumber;
                case PaymentType.Check:
                    return PaymentMethodType.Check;
                case PaymentType.OnCredit:
                    return PaymentMethodType.OnCredit;
                case PaymentType.Cash:
                    return PaymentMethodType.Cash;
                default:
                    throw new ArgumentException($"Cannot map PaymentType {paymentType} to PaymentMethodType.");
            }
        }

        private VatExemptionCategory MapVatExemptionCategory(VatExemption vatExemption)
        {
            switch(vatExemption)
            {
                case VatExemption.VatIncludedArticle43:
                    return VatExemptionCategory.VatIncludedArticle43;
                case VatExemption.VatIncludedArticle44:
                    return VatExemptionCategory.VatIncludedArticle44;
                case VatExemption.VatIncludedArticle45:
                    return VatExemptionCategory.VatIncludedArticle45;
                case VatExemption.VatIncludedArticle46:
                    return VatExemptionCategory.VatIncludedArticle46;
                case VatExemption.WithoutVatArticle13:
                    return VatExemptionCategory.WithoutVatArticle13;
                case VatExemption.WithoutVatArticle14:
                    return VatExemptionCategory.WithoutVatArticle14;
                case VatExemption.WithoutVatArticle16:
                    return VatExemptionCategory.WithoutVatArticle16;
                case VatExemption.WithoutVatArticle19:
                    return VatExemptionCategory.WithoutVatArticle19;
                case VatExemption.WithoutVatArticle22:
                    return VatExemptionCategory.WithoutVatArticle22;
                case VatExemption.WithoutVatArticle24:
                    return VatExemptionCategory.WithoutVatArticle24;
                case VatExemption.WithoutVatArticle25:
                    return VatExemptionCategory.WithoutVatArticle25;
                case VatExemption.WithoutVatArticle26:
                    return VatExemptionCategory.WithoutVatArticle26;
                case VatExemption.WithoutVatArticle27:
                    return VatExemptionCategory.WithoutVatArticle27;
                case VatExemption.WithoutVatArticle271CSeagoingVessels:
                    return VatExemptionCategory.WithoutVatArticle271CSeagoingVessels;
                case VatExemption.WithoutVatArticle27SeagoingVessels:
                    return VatExemptionCategory.WithoutVatArticle27SeagoingVessels;
                case VatExemption.WithoutVatArticle28:
                    return VatExemptionCategory.WithoutVatArticle28;
                case VatExemption.WithoutVatArticle3:
                    return VatExemptionCategory.WithoutVatArticle3;
                case VatExemption.WithoutVatArticle39:
                    return VatExemptionCategory.WithoutVatArticle39;
                case VatExemption.WithoutVatArticle39A:
                    return VatExemptionCategory.WithoutVatArticle39A;
                case VatExemption.WithoutVatArticle40:
                    return VatExemptionCategory.WithoutVatArticle40;
                case VatExemption.WithoutVatArticle41:
                    return VatExemptionCategory.WithoutVatArticle41;
                case VatExemption.WithoutVatArticle47:
                    return VatExemptionCategory.WithoutVatArticle47;
                case VatExemption.WithoutVatArticle5:
                    return VatExemptionCategory.WithoutVatArticle5;
                default:
                    throw new ArgumentException($"Cannot map VatExemption {vatExemption} to VatExemptionCategory.");
            }
        }
    }
}
