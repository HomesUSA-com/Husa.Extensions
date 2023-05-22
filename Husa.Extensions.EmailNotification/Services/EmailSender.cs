namespace Husa.Extensions.EmailNotification.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Husa.Extensions.EmailNotification.Classes;
    using Husa.Extensions.EmailNotification.Configuration.Options;
    using Husa.Extensions.EmailNotification.Enums;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using sib_api_v3_sdk.Api;
    using sib_api_v3_sdk.Model;

    public class EmailSender : IEmailSender
    {
        private readonly EmailOptions<TemplateType> options;
        private readonly ITransactionalEmailsApi transactionalEmailsApi;
        private readonly ILogger<EmailSender> logger;

        public EmailSender(
            IOptions<EmailOptions<TemplateType>> options,
            ITransactionalEmailsApi transactionalEmailsApi,
            ILogger<EmailSender> logger)
        {
            this.options = options.Value ?? throw new ArgumentNullException(nameof(options));
            this.transactionalEmailsApi = transactionalEmailsApi ?? throw new ArgumentNullException(nameof(transactionalEmailsApi));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void SendEmail<TEmailParameterValue>(
            string recipient,
            string name,
            IReadOnlyDictionary<EmailParameter, TEmailParameterValue> emailParameters,
            TemplateType templateType = TemplateType.NoTemplate)
        {
            this.SendEmail<EmailParameter, TEmailParameterValue>(recipient, name, emailParameters, templateType);
        }

        public void SendEmail<TEmailParameterKey, TEmailParameterValue>(
            string recipient,
            string name,
            IReadOnlyDictionary<TEmailParameterKey, TEmailParameterValue> emailParameters,
            TemplateType templateType = TemplateType.NoTemplate,
            string[] ccRecipients = null)
            where TEmailParameterKey : struct
        {
            if (emailParameters is null)
            {
                this.logger.LogError("The email parameters must be informed for email '{email}' of user '{user}'", recipient, name);
                throw new ArgumentNullException(nameof(emailParameters));
            }

            this.logger.LogInformation("Sending email to '{email}' of user '{user}'", recipient, name);
            var sender = new SendSmtpEmailSender(this.options.Sender.Name, this.options.Sender.Email);
            var recipients = new List<SendSmtpEmailTo>
            {
                new(recipient, name),
            };

            var bccRecipients = new List<SendSmtpEmailBcc>();
            foreach (var bccRecipient in this.options.BccRecipients)
            {
                bccRecipients.Add(new(bccRecipient.Email, bccRecipient.Name));
            }

            var smtpCcRecipients = ccRecipients != null ? ccRecipients.Select(email => new SendSmtpEmailCc(email)).ToList() : null;

            var templateOptions = this.options.EmailTemplates.Single(e => e.TemplateType == templateType);

            var paramToUse = EmailParameters<TemplateType>.ConfigureForTemplate(emailParameters, templateOptions);

            var sendSmtpEmail = new SendSmtpEmail(
                sender: sender,
                to: recipients,
                bcc: bccRecipients,
                cc: smtpCcRecipients,
                subject: paramToUse.Subject,
                templateId: paramToUse.TemplateId,
                _params: paramToUse.Parameters);

            this.transactionalEmailsApi.SendTransacEmail(sendSmtpEmail);
        }
    }
}
