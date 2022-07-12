namespace Husa.Extensions.EmailNotification.Configuration.Settings
{
    using System.ComponentModel.DataAnnotations;

    public class EmailInfoSettings
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "An email must be provided.")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "A recipient must be provided.")]
        public string Name { get; set; }
    }
}
