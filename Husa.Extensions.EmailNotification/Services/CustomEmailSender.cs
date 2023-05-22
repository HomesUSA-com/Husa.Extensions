namespace Husa.Extensions.EmailNotification.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Husa.Extensions.EmailNotification.Classes;
    using Husa.Extensions.EmailNotification.Configuration.Options;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using sib_api_v3_sdk.Api;
    using sib_api_v3_sdk.Model;

    public abstract class CustomEmailSender<TTemplateType> : ICustomEmailSender<TTemplateType>
        where TTemplateType : struct, Enum
    {
        private readonly ITransactionalEmailsApi transactionalEmailsApi;
        private readonly ILogger logger;

        protected CustomEmailSender(
            IOptions<EmailOptions<TTemplateType>> options,
            ITransactionalEmailsApi transactionalEmailsApi,
            ILogger logger)
        {
            this.Options = options.Value ?? throw new ArgumentNullException(nameof(options));
            this.transactionalEmailsApi = transactionalEmailsApi ?? throw new ArgumentNullException(nameof(transactionalEmailsApi));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public EmailOptions<TTemplateType> Options { get; set; }

        public void SendEmail(
            IEnumerable<Recipient> toRecipients,
            EmailParameters<TTemplateType> emailParams,
            IEnumerable<Recipient> ccRecipients = null)
        {
            if (emailParams is null)
            {
                this.logger.LogError("The email parameters must be informed");
                throw new ArgumentNullException(nameof(emailParams));
            }

            if (toRecipients is null || !toRecipients.Any())
            {
                this.logger.LogError("The email recipients must be informed");
                throw new ArgumentNullException(nameof(toRecipients));
            }

            this.logger.LogInformation("Sending email to '{email}''", string.Join(",", toRecipients.Select(x => x.Email)));
            var sender = new SendSmtpEmailSender(this.Options.Sender.Name, this.Options.Sender.Email);

            var smtpToRecipients = toRecipients.Select(r => new SendSmtpEmailTo(r.Email, r.Name)).ToList();
            var smtpBccRecipients = this.Options.BccRecipients.Select(r => new SendSmtpEmailBcc(r.Email, r.Name)).ToList();
            var smtpCcRecipients = ccRecipients != null ? ccRecipients.Select(r => new SendSmtpEmailCc(r.Email, r.Name)).ToList() : null;

            var sendSmtpEmail = new SendSmtpEmail(
                sender: sender,
                to: smtpToRecipients,
                bcc: smtpBccRecipients,
                cc: smtpCcRecipients,
                subject: emailParams.Subject,
                templateId: emailParams.TemplateId,
                _params: emailParams.Parameters);

            this.transactionalEmailsApi.SendTransacEmail(sendSmtpEmail);
        }
    }
}
