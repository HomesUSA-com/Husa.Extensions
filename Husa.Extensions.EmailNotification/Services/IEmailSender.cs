namespace Husa.Extensions.EmailNotification.Services
{
    using System.Collections.Generic;
    using Husa.Extensions.EmailNotification.Enums;

    public interface IEmailSender
    {
        void SendEmail<TEmailParameterKey, TEmailParameterValue>(string recipient, string name, IReadOnlyDictionary<TEmailParameterKey, TEmailParameterValue> emailParameters, TemplateType templateType = TemplateType.NoTemplate)
            where TEmailParameterKey : struct;
    }
}
