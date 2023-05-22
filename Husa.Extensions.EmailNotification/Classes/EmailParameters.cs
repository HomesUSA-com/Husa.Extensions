namespace Husa.Extensions.EmailNotification.Classes
{
    using System;
    using System.Collections.Generic;
    using Husa.Extensions.EmailNotification.Configuration.Settings;
    using Husa.Extensions.EmailNotification.Enums;

    public class EmailParameters<TTemplateType>
        where TTemplateType : struct, Enum
    {
        private const string DefaultSubject = "HomesUSA.com";

        private EmailParameters(TTemplateType templateType, string subject = null)
        {
            this.TemplateType = templateType;
            this.Subject = subject ?? DefaultSubject;
            this.Parameters = new();
        }

        public int TemplateId => (int)(object)this.TemplateType;

        public TTemplateType TemplateType { get; set; }

        public string Subject { get; set; }

        public Dictionary<string, object> Parameters { get; set; }

        public static EmailParameters<TTemplateType> ConfigureForTemplate<TEmailParameterKey, TEmailParameterValue>(
            IReadOnlyDictionary<TEmailParameterKey, TEmailParameterValue> emailParameters,
            EmailTemplate<TTemplateType> templateOptions)
        where TEmailParameterKey : struct
        {
            var parameters = new EmailParameters<TTemplateType>(templateOptions.TemplateType, templateOptions.Subject);

            foreach (var parameter in templateOptions.Parameters)
            {
                if (!Enum.TryParse(typeof(TEmailParameterKey), parameter, out var paramEnum) || !emailParameters.TryGetValue((TEmailParameterKey)paramEnum, out var emailParam))
                {
                    throw new ArgumentException($"The value of '{parameter}' in '{nameof(emailParameters)}' cannot be null or whitespace.", nameof(emailParameters));
                }

                var enumDescription = EnumExtensions.GetEnumDescription<TEmailParameterKey>(parameter);
                parameters.Parameters.Add(enumDescription, emailParam);
            }

            return parameters;
        }

        public static EmailParameters<TTemplateType> ConfigureForTemplate(EmailTemplate<TTemplateType> templateOptions)
        {
            return new EmailParameters<TTemplateType>(templateOptions.TemplateType, templateOptions.Subject);
        }

        public void ConfigureParameters<TEmailParameterKey>(IReadOnlyDictionary<TEmailParameterKey, object> emailParameters)
            where TEmailParameterKey : Enum
        {
            foreach (var parameterKey in emailParameters.Keys)
            {
                var enumDescription = EnumExtensions.GetEnumDescription(parameterKey);
                this.Parameters.Add(enumDescription, emailParameters[parameterKey]);
            }
        }
    }
}
