namespace Husa.Extensions.EmailNotification
{
    using System;
    using Husa.Extensions.EmailNotification.Configuration.Options;
    using Husa.Extensions.EmailNotification.Enums;
    using Husa.Extensions.EmailNotification.Services;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using sib_api_v3_sdk.Api;
    using SendingBlueConfiguration = sib_api_v3_sdk.Client.Configuration;

    public static class EmailBootstrapper
    {
        public static IServiceCollection EmailNotificationRegister(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEmailSender, EmailSender>();
            services.EmailNotificationRegister<TemplateType>(configuration);
            return services;
        }

        public static IServiceCollection EmailNotificationRegister<TTemplateType>(this IServiceCollection services, IConfiguration configuration)
            where TTemplateType : struct, Enum
        {
            services.AddScoped<ITransactionalEmailsApi, TransactionalEmailsApi>();
            services.ConfigureEmailApi<TTemplateType>(configuration);
            services.ConfigureEmailOptions<TTemplateType>();
            return services;
        }

        private static void ConfigureEmailApi<TTemplateType>(this IServiceCollection services, IConfiguration configuration)
            where TTemplateType : struct, Enum
        {
            var emailOptions = configuration.GetSection(EmailOptions<TTemplateType>.Section).Get<EmailOptions<TTemplateType>>() ?? new EmailOptions<TTemplateType>();

            if (SendingBlueConfiguration.Default.ApiKey.ContainsKey(EmailOptions<TTemplateType>.ApiKey))
            {
                return;
            }

            SendingBlueConfiguration.Default.ApiKey.Add(EmailOptions<TTemplateType>.ApiKey, emailOptions.ApiKeySendinblue);
        }

        private static IServiceCollection ConfigureEmailOptions<TTemplateType>(this IServiceCollection services)
            where TTemplateType : struct, Enum
        {
            services
                .AddOptions<EmailOptions<TTemplateType>>()
                .Configure<IConfiguration>((settings, config) => config.GetSection(EmailOptions<TTemplateType>.Section).Bind(settings))
                .ValidateDataAnnotations();

            return services;
        }
    }
}
