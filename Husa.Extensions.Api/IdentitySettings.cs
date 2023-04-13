namespace Husa.Extensions.Api
{
    using System.ComponentModel.DataAnnotations;

    public class IdentitySettings
    {
        public const string Section = "Identity";

        [Required(AllowEmptyStrings = false)]
        public string Uri { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string ClientSecret { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string ClientId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Scope { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
