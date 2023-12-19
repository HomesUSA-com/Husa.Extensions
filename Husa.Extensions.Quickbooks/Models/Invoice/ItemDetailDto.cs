namespace Husa.Extensions.Quickbooks.Models.Invoice
{
    using System;

    public class ItemDetailDto
    {
        public ItemDetailDto(string itemRefId, string classRefId)
        {
            this.NewListing = new SalesItemLineDetailDto(itemRefId, classRefId);
            this.Relist = new SalesItemLineDetailDto(itemRefId, classRefId);
            this.Comparable = new SalesItemLineDetailDto(itemRefId, classRefId);
            this.ActiveTransfer = new SalesItemLineDetailDto(itemRefId, classRefId);
            this.PendingTransfer = new SalesItemLineDetailDto(itemRefId, classRefId);
            this.NewListing.TotalAmount = this.Relist.TotalAmount = this.Comparable.TotalAmount = this.ActiveTransfer.TotalAmount = this.PendingTransfer.TotalAmount = 0;
            this.TotalInvoice = 0;
        }

        public SalesItemLineDetailDto NewListing { get; set; }
        public SalesItemLineDetailDto Relist { get; set; }
        public SalesItemLineDetailDto Comparable { get; set; }
        public SalesItemLineDetailDto ActiveTransfer { get; set; }
        public SalesItemLineDetailDto PendingTransfer { get; set; }
        public double TotalInvoice { get; set; }
        public string CustomerRef { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string InvoiceType { get; set; }
    }
}
