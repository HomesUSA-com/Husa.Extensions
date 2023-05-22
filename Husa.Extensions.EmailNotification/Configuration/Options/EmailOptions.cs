namespace Husa.Extensions.EmailNotification.Configuration.Options
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Husa.Extensions.EmailNotification.Configuration.Settings;

    public class EmailOptions<TTemplateType>
        where TTemplateType : struct, Enum
    {
        public const string Section = "Email";

        public const string ApiKey = "api-key";

        [Required(AllowEmptyStrings = false, ErrorMessage = "A ApiKey must be provided.")]
        public string ApiKeySendinblue { get; set; }

        public EmailInfoSettings Sender { get; set; }

        public IEnumerable<EmailInfoSettings> BccRecipients { get; set; }

        public IEnumerable<EmailTemplate<TTemplateType>> EmailTemplates { get; set; }
    }
}
