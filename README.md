![Build Status](https://github.com/MewsSystems/fiscalization-greece/workflows/Build%20and%20test/badge.svg)

# Mews.Fiscalization.Greece

## Key features
- No Greek abbreviations.
- Early data validation.
- Intuitive immutable DTOs.
- Logging support

## Usage
We tend to use immutable DTOs wherever possible, especially to ensure data validity.
We want the library to throw an error as soon as possible, i.e. when constructing corresponding data structures.
That is why we even introduce wrappers for simple datatypes.

### Simplest usage example
```csharp
var record = new InvoiceDocument(
new List<Invoice>()
{
    new Invoice(
        issuer: new InvoiceParty(new NotEmptyString({UserVatNumber}), new CountryCode("GR")),
        header: new InvoiceHeader(new LimitedString1to50("0"), new LimitedString1to50("50020"), DateTime.Now, BillType.RetailSalesReceipt, new CurrencyCode("EUR")),
        revenueItems: new List<RevenueItem>
        {
            new RevenueItem(new Amount(88.50m), TaxType.Vat13, new Amount(11.50m), ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProvisionOfServicesIncome, new PositiveInt(1)),
            new RevenueItem(new Amount(5.00m), TaxType.Vat0, new Amount(0.00m), ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome, new PositiveInt(2), VatExemptionType.VatIncludedArticle43),
            new RevenueItem(new Amount(16.13m), TaxType.Vat24, new Amount(3.87m), ClassificationType.RetailSalesOfGoodsAndServicesPrivateClientele, ClassificationCategory.ProductSaleIncome, new PositiveInt(3))
        },
        payments: new List<Payment>
        {
            new Payment(new Amount(125.00m), PaymentType.Cash),
        }
    )
});

var client = new AadeClient({UserId}, {UserSubscriptionKey});
var response = await client.SendInvoicesAsync(invoiceDoc);
```

# NuGet
