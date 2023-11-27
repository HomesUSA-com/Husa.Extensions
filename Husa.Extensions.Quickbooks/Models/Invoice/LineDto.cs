namespace Husa.Extensions.Quickbooks.Models.Invoice
{
    public class LineDto
    {
        public double Amount { get; set; }

        public string DetailType { get; set; }

        public string Description { get; set; }

        public SalesItemLineDetailDto SalesItemLineDetail { get; set; }
    }
}
