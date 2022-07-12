namespace Husa.Extensions.EmailNotification.Services
{
    using System.Collections.Generic;
    using Husa.Extensions.EmailNotification.Enums;

    public interface IEmailSender
    {
        void SendEmail(string recipient, string name, IReadOnlyDictionary<EmailParameter, string> emailParameters, TemplateType templateType = TemplateType.NoTemplate);
    }
}
