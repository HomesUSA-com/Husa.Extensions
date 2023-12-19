namespace Husa.Extensions.Quickbooks.Models
{
    using System.ComponentModel.DataAnnotations;

    public class InvoiceSettings
    {
        public const string Section = "InvoiceSettings";

        [Required]
        public string ServiceURL { get; set; }
        [Required]
        public string SalesItemLineId { get; set; }
        [Required]
        public string ClassRefId { get; set; }
    }
}
