using System;
using System.Collections.Generic;
using System.Text;

namespace Mews.Fiscalization.Greece.Model
{
    public class InvoiceRecordPartyAddress
    {
        public InvoiceRecordPartyAddress(string street, string number, string postalCode, string city)
        {
            Street = street;
            Number = number;
            PostalCode = postalCode;
            City = city;
        }

        public string Street { get; }

        public string Number { get; }

        public string PostalCode { get; }

        public string City { get; }
    }
}
