using System;
using System.Collections.Generic;
using System.Text;

namespace Mews.Fiscalization.Greece.Model
{
    public class InvoiceRecordSummary
    {
        public InvoiceRecordSummary(decimal totalNetValue, decimal totalVatAmount, decimal totalWithheldAmount, decimal totalFeesAmount,
            decimal totalStampDutyAmount, decimal totalOtherTaxesAmount, decimal totalDeductionsAmount, decimal totalGrossValue)
        {
            TotalNetValue = totalNetValue;
            TotalVatAmount = totalVatAmount;
            TotalWithheldAmount = totalWithheldAmount;
            TotalFeesAmount = totalFeesAmount;
            TotalStampDutyAmount = totalStampDutyAmount;
            TotalOtherTaxesAmount = totalOtherTaxesAmount;
            TotalDeductionsAmount = totalDeductionsAmount;
            TotalGrossValue = totalGrossValue;
        }

        public decimal TotalNetValue { get; }

        public decimal TotalVatAmount { get; }

        public decimal TotalWithheldAmount { get; }

        public decimal TotalFeesAmount { get; }

        public decimal TotalStampDutyAmount { get; }

        public decimal TotalOtherTaxesAmount { get; }

        public decimal TotalDeductionsAmount { get; }

        public decimal TotalGrossValue { get; }
    }
}
