﻿using Mews.Fiscalization.Greece.Model.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mews.Fiscalization.Greece.Model
{
    public class Invoice
    {
        public Invoice(
            InvoiceParty issuer,
            InvoiceHeader invoiceHeader,
            IEnumerable<RevenueItem> invoiceDetails,
            InvoiceSummary invoiceSummary,
            StringIdentifier invoiceIdentifier = null,
            InvoiceRegistrationNumber invoiceRegistrationNumber = null,
            InvoiceRegistrationNumber cancelledByInvoiceRegistrationNumber = null,
            InvoiceParty counterpart = null,
            IEnumerable<Payment> paymentMethods = null)
        {
            Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
            InvoiceHeader = invoiceHeader ?? throw new ArgumentNullException(nameof(invoiceHeader));
            InvoiceDetails = invoiceDetails ?? throw new ArgumentNullException(nameof(invoiceDetails));
            InvoiceSummary = invoiceSummary ?? throw new ArgumentNullException(nameof(invoiceSummary));
            InvoiceIdentifier = invoiceIdentifier;
            InvoiceRegistrationNumber = invoiceRegistrationNumber;
            CanceledByInvoiceRegistrationNumber = cancelledByInvoiceRegistrationNumber;
            Counterpart = counterpart;
            PaymentMethods = paymentMethods;

            if (invoiceDetails.Count() == 0)
            {
                throw new ArgumentException($"Minimal count of {nameof(invoiceDetails)} is 1.");
            }
        }

        public StringIdentifier InvoiceIdentifier { get; }

        public InvoiceRegistrationNumber InvoiceRegistrationNumber { get; }

        public InvoiceRegistrationNumber CanceledByInvoiceRegistrationNumber { get; }

        public InvoiceParty Issuer { get; }

        public InvoiceParty Counterpart { get; }

        public InvoiceHeader InvoiceHeader { get; set; }

        public IEnumerable<Payment> PaymentMethods { get; set; }

        public IEnumerable<RevenueItem> InvoiceDetails { get; }

        public InvoiceSummary InvoiceSummary { get; }
    }
}