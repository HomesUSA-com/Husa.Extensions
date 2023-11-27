namespace Husa.Extensions.Quickbooks.Models.Invoice
{
    using System.Collections.Generic;

    public class CreateInvoiceDto
    {
        public CreateInvoiceDto(IEnumerable<LineDto> line, CustomerRefDto customerRef)
        {
            this.Line = line;
            this.CustomerRef = customerRef;
        }

        public IEnumerable<LineDto> Line { get; set; }

        public CustomerRefDto CustomerRef { get; set; }
    }
}
