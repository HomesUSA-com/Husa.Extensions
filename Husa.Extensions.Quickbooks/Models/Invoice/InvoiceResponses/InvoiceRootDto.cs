namespace Husa.Extensions.Quickbooks.Models.Invoice.InvoiceResponses
{
    using System;

    public class InvoiceRootDto
    {
        public InvoiceResponseDto Invoice { get; set; }
        public DateTime Time { get; set; }
    }
}
