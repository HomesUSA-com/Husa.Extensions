namespace Husa.Extensions.Quickbooks.Models.Invoice.InvoiceResponses
{
    public class LineResponseDto
    {
        public string Description { get; set; }
        public string DetailType { get; set; }
        public SalesItemLineDetailResponseDto SalesItemLineDetail { get; set; }
        public int LineNum { get; set; }
        public decimal Amount { get; set; }
        public string Id { get; set; }
        public object SubTotalLineDetail { get; set; }
    }
}
