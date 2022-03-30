namespace Husa.Extensions.Media.Models
{
    using System.ComponentModel.DataAnnotations;

    public class AzureSettings
    {
        public const string Section = "Azure";

        [Required]
        public string AccountName { get; set; }

        [Required]
        public string AccountKey { get; set; }
    }
}
