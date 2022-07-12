namespace Husa.Extensions.EmailNotification
{
    using Husa.Extensions.EmailNotification.Configuration.Options;
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
            services.AddScoped<ITransactionalEmailsApi, TransactionalEmailsApi>();
            services.ConfigureEmailApi(configuration);
            services.ConfigureEmailOptions();

            return services;
        }

        private static void ConfigureEmailApi(this IServiceCollection services, IConfiguration configuration)
        {
            var emailOptions = configuration.GetSection(EmailOptions.Section).Get<EmailOptions>() ?? new EmailOptions();

            if (SendingBlueConfiguration.Default.ApiKey.ContainsKey(EmailOptions.ApiKey))
            {
                return;
            }

            SendingBlueConfiguration.Default.ApiKey.Add(EmailOptions.ApiKey, emailOptions.ApiKeySendinblue);
        }

        private static IServiceCollection ConfigureEmailOptions(this IServiceCollection services)
        {
            services
                .AddOptions<EmailOptions>()
                .Configure<IConfiguration>((settings, config) => config.GetSection(EmailOptions.Section).Bind(settings))
                .ValidateDataAnnotations();

            return services;
        }
    }
}
