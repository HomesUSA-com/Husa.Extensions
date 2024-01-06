namespace Husa.Extensions.Quickbooks.Extensions
{
    using System;
    using Husa.Extensions.Quickbooks.Interfaces;
    using Husa.Extensions.Quickbooks.Models;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Refit;

    public static class QuickbooksBootstrapper
    {
        public static IServiceCollection ConfigureInvoiceClient(this IServiceCollection services, IConfiguration configuration)
        {
            var applicationOptions = configuration.GetSection($"Application:{InvoiceSettings.Section}").Get<InvoiceSettings>();

            services.AddRefitClient<IQuickbooksApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(applicationOptions.ServiceURL));

            return services;
        }
    }
}
