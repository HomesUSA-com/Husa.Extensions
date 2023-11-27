namespace Husa.Extensions.Quickbooks.Models.Invoice
{
    using System;
    using System.Collections.Generic;

    public class InvoiceDto
    {
        public Guid CompanyId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public IEnumerable<Guid> ListingIds { get; set; }
    }
}
