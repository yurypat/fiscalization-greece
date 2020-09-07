using System;
using System.Collections.Generic;
using System.Text;

namespace Mews.Fiscalization.Greece.Model
{
    public class InvoiceRecordParty
    {
        public InvoiceRecordParty(string vatNumber, int branch, string name, InvoiceRecordPartyAddress invoiceRecordPartyAddress)
        {
            VatNumber = vatNumber;
            Branch = branch;
            Name = name;
            Address = invoiceRecordPartyAddress;
        }

        public string VatNumber { get; }

        public int Branch { get; }

        public string Name { get; }

        public InvoiceRecordPartyAddress Address { get; }
    }
}
