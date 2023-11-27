namespace Husa.Extensions.Media.Models
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
