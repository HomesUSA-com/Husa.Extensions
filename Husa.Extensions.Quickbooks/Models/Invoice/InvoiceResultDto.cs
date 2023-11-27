namespace Husa.Extensions.Quickbooks.Models.Invoice
{
    using System;

    public class InvoiceResultDto
    {
        public InvoiceResultDto()
        {
            this.HasFailed = false;
            this.ErrorMessage = string.Empty;
            this.InvoiceId = string.Empty;
            this.InvoiceDocNumber = string.Empty;
        }

        public bool HasFailed { get; set; }

        public string InvoiceId { get; set; }

        public string InvoiceDocNumber { get; set; }

        public DateTime CreatedDate { get; set; }

        public string ErrorMessage { get; set; }
    }
}
