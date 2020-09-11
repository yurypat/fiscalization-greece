using Mews.Fiscalization.Greece.Dto.Xsd;
using Mews.Fiscalization.Greece.Extensions;
using Mews.Fiscalization.Greece.Model;
using Mews.Fiscalization.Greece.Model.Types;
using System;
using System.Collections.Generic;

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
                Invoices = GetInvoices()
            };
        }

        private Invoice[] GetInvoices()
        {
            var invoices = new List<Invoice>();

            foreach (var invoiceRecord in InvoiceDocument.InvoiceRecords)
            {
                invoices.Add(GetInvoice(invoiceRecord));
            }

            return invoices.ToArray();
        }

        private Invoice GetInvoice(InvoiceRecord invoiceRecord)
        {
            return new Invoice
            {
                InvoiceMarkSpecified = invoiceRecord.InvoiceRegistrationNumber.IsDefined(),
                InvoiceMark = invoiceRecord.InvoiceRegistrationNumber.GetOrDefault(),
                InvoiceCancellationMarkSpecified = invoiceRecord.CancelledByInvoiceRegistrationNumber.IsDefined(),
                InvoiceCancellationMark = invoiceRecord.CancelledByInvoiceRegistrationNumber.GetOrDefault(),
                InvoiceId = invoiceRecord.InvoiceIdentifier.GetOrDefault(),
                InvoiceIssuer = GetInvoiceParty(invoiceRecord.Issuer),
                InvoiceCounterpart = GetInvoiceParty(invoiceRecord.Counterpart),
                PaymentMethods = GetInvoicePaymentMethods(invoiceRecord.PaymentMethods),
                InvoiceSummary = GetInvoiceSummary(invoiceRecord),
                InvoiceDetails = GetInvoiceDetails(invoiceRecord),
                InvoiceHeader = GetInvoiceHeader(invoiceRecord)
            };
        }

        private PaymentMethod[] GetInvoicePaymentMethods(IEnumerable<InvoiceRecordPaymentMethodDetails> paymentMethods)
        {
            var result = new List<PaymentMethod>();

            foreach (var paymentMethod in paymentMethods)
            {
                result.Add(new PaymentMethod
                {
                    Amount = paymentMethod.Amount.Value,
                    PaymentMethodType = paymentMethod.PaymentType.ConvertToEnum<PaymentMethodType>()
                });
            }

            return result.ToArray();
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
                    VatNumber = invoiceRecordParty.VatNumber.Value,
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
                InvoiceType = invoiceRecord.InvoiceHeader.BillType.ConvertToEnum<InvoiceType>(),
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

        private InvoiceDetail[] GetInvoiceDetails(InvoiceRecord invoiceRecord)
        {
            var invoiceDetails = new List<InvoiceDetail>();

            foreach (var invoiceDetail in invoiceRecord.InvoiceDetails)
            {
                invoiceDetails.Add(GetInvoiceDetail(invoiceDetail));
            }

            return invoiceDetails.ToArray();
        }

        private InvoiceDetail GetInvoiceDetail(InvoiceRecordDetail invoiceDetail)
        {
            return new InvoiceDetail
            {
                LineNumber = invoiceDetail.LineNumber.Value,
                NetValue = invoiceDetail.NetValue.Value,
                VatAmount = invoiceDetail.VatAmount.Value,
                VatCategory = invoiceDetail.VatType.ConvertToEnum<VatCategory>(),
                IncomeClassification = GetIncomeClassifications(invoiceDetail.InvoiceRecordIncomeClassification),
                DiscountOptionSpecified = invoiceDetail.DiscountOption.IsDefined(),
                DiscountOption = invoiceDetail.DiscountOption.GetOrDefault(),
            };
        }

        private InvoiceSummary GetInvoiceSummary(InvoiceRecord invoiceRecord)
        {
            return new InvoiceSummary
            {
                TotalNetValue = invoiceRecord.InvoiceSummary.TotalNetValue.Value,
                TotalVatAmount = invoiceRecord.InvoiceSummary.TotalVatAmount.Value,
                TotalGrossValue = invoiceRecord.InvoiceSummary.TotalGrossValue.Value,
                IncomeClassification = GetIncomeClassifications(invoiceRecord.InvoiceSummary.InvoiceRecordIncomeClassification)
            };
        }

        private IncomeClassification[] GetIncomeClassifications(IEnumerable<InvoiceRecordIncomeClassification> invoiceRecordIncomeClassification)
        {
            var incomeClassifications = new List<IncomeClassification>();

            foreach (var invoiceIncomeClassification in invoiceRecordIncomeClassification)
            {
                incomeClassifications.Add(GetIncomeClassification(invoiceIncomeClassification));
            }

            return incomeClassifications.ToArray();
        }

        private IncomeClassification GetIncomeClassification(InvoiceRecordIncomeClassification invoiceRecordIncomeClassification)
        {
            return new IncomeClassification
            {
                Amount = invoiceRecordIncomeClassification.Amount.Value,
                ClassificationCategory = invoiceRecordIncomeClassification.ClassificationCategory.ConvertToEnum<IncomeClassificationCategory>(),
                ClassificationType = invoiceRecordIncomeClassification.ClassificationType.ConvertToEnum<IncomeClassificationType>()
            };
        }
    }
}
