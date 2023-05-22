namespace Husa.Extensions.EmailNotification.Services
{
    using System;
    using System.Collections.Generic;
    using Husa.Extensions.EmailNotification.Classes;
    using Husa.Extensions.EmailNotification.Configuration.Options;

    public interface ICustomEmailSender<TTemplateType>
        where TTemplateType : struct, Enum
    {
        public EmailOptions<TTemplateType> Options { get; set; }
        public void SendEmail(
            IEnumerable<Recipient> toRecipients,
            EmailParameters<TTemplateType> emailParams,
            IEnumerable<Recipient> ccRecipients = null);
    }
}
