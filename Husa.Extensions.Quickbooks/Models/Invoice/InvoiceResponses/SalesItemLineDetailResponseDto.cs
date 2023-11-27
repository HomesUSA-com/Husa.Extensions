namespace Husa.Extensions.Quickbooks.Models.Invoice.InvoiceResponses
{
    public class SalesItemLineDetailResponseDto
    {
        public TaxCodeRefDto TaxCodeRef { get; set; }
        public int Qty { get; set; }
        public decimal UnitPrice { get; set; }
        public ItemRefDto ItemRef { get; set; }
    }
}
