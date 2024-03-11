namespace Husa.Extensions.Document.Tests.Configuration
{
    using System.ComponentModel.DataAnnotations;

    public class DocumentDbSettings
    {
        public const string Section = "CosmosDb";

        [Required]
        public string Endpoint { get; set; }

        [Required]
        public string AuthToken { get; set; }

        [Required]
        public string DatabaseName { get; set; }

        [Required]
        public string CollectionName { get; set; }
    }
}
