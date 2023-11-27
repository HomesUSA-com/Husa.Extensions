namespace Husa.Extensions.Quickbooks.Models.Invoice
{
    public class InvoiceAttachDto
    {
        public InvoiceAttachDto(string invoiceId, string fileName, string entityTypeRef, string contentType, string fileContent)
        {
            this.InvoiceId = invoiceId;
            this.FileName = fileName;
            this.EntityTypeRef = entityTypeRef;
            this.ContentType = contentType;
            this.FileContent = fileContent;
        }

        public string InvoiceId { get; set; }

        public string FileName { get; set; }

        public string EntityTypeRef { get; set; }

        public string ContentType { get; set; }

        public string FileContent { get; set; }
    }
}
