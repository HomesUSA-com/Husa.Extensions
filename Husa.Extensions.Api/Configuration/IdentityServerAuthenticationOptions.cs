namespace Husa.Extensions.Api.Configuration
{
    using System.ComponentModel.DataAnnotations;

    public class IdentityServerAuthenticationOptions
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "A Authority must be provided.")]
        public string Authority { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "A ApiName must be provided.")]
        public string ApiName { get; set; }

        public bool EnableCaching { get; set; }
    }
}
