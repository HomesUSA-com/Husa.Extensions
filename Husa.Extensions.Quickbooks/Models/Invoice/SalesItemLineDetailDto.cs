namespace Husa.Extensions.Quickbooks.Models.Invoice
{
    using System;

    public class SalesItemLineDetailDto
    {
        public SalesItemLineDetailDto(string itemRef, string classRef)
        {
            this.Qty = 0;
            this.UnitPrice = 0;
            this.TotalAmount = 0;
            this.ItemRef = new ItemRefDto(itemRef);
            this.ClassRef = new ClassRefDto(classRef);
        }

        public ItemRefDto ItemRef { get; set; }

        public ClassRefDto ClassRef { get; set; }

        public decimal Qty { get; set; }

        public decimal UnitPrice { get; set; }

        public DateTime ServiceDate { get; set; }

        public double TotalAmount { get; set; }

        public string ActionName { get; set; }
    }
}
