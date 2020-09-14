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

        private InvoiceDetail GetInvoiceDetail(InvoiceRecordDetail invoiceDetail)
        {
            return new InvoiceDetail
            {
                LineNumber = invoiceDetail.LineNumber.Value,
                NetValue = invoiceDetail.NetValue.Value,
                VatAmount = invoiceDetail.VatAmount.Value,
                VatCategory = MapVatCategory(invoiceDetail.TaxType),
                IncomeClassification = invoiceDetail.InvoiceRecordIncomeClassification.Select(invoiceIncomeClassification => GetIncomeClassification(invoiceIncomeClassification)).ToArray(),
                DiscountOptionSpecified = invoiceDetail.DiscountOption.IsDefined(),
                DiscountOption = invoiceDetail.DiscountOption.GetOrDefault(),
            };
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
    }
}
