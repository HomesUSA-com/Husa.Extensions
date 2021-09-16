namespace Husa.Extensions.Api.Configuration
{
    using System.ComponentModel.DataAnnotations;

    public class ApplicationOptions
    {
        public const string Section = "Application";

        [Required(AllowEmptyStrings = false, ErrorMessage = "A SubDomain must be provided.")]
        public string SubDomain { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "A Domain must be provided.")]
        public string Domain { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "A Protocol must be provided.")]
        public string Protocol { get; set; }

        public IdentityServerAuthenticationOptions ApiConfiguration { get; set; }

        public Policies Policies { get; set; }

        public string ApplicationUrl => $"{this.Protocol}{this.SubDomain}{this.Domain}";
    }
}
