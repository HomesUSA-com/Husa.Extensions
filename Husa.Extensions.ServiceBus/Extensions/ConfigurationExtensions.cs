namespace Husa.Extensions.ServiceBus.Extensions
{
    using System;
    using System.Threading.Tasks;
    using Azure.Messaging.ServiceBus;
    using Husa.Extensions.ServiceBus.Handlers;
    using Husa.Extensions.ServiceBus.Interfaces;
    using Husa.Extensions.ServiceBus.Services;
    using Husa.Extensions.ServiceBus.Subscribers;
    using Microsoft.Azure.ServiceBus;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ConfigurationExtensions
    {
        public static void ConfigureClient<THandler>(this IProvideSubscriptionClient provider, THandler handler)
            where THandler : IHandleMessages
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            provider.Client.RegisterMessageHandler(handler.HandleMessage, MessageOptions(handler.HandleException));
        }

        public static Task CloseClient(this IProvideSubscriptionClient provider)
        {
            ArgumentNullException.ThrowIfNull(provider);

            return CloseClientAsync(provider);
        }

        public static IServiceCollection RegisterBusServices(
            this IServiceCollection services,
            IConfiguration configuration,
            string configurationSection = "ServiceBus",
            string configurationKey = "ConnectionString")
        {
            // Register the ServiceBusClient as a singleton
            services.AddSingleton(serviceProvider =>
            {
                var connectionString = configuration.GetSection(configurationSection)[configurationKey];
                return new ServiceBusClient(connectionString);
            });

            // Register the ServiceBusSenderFactory as a singleton
            services.AddSingleton<IServiceBusSenderFactory, ServiceBusSenderFactory>();

            return services;
        }

        private static async Task CloseClientAsync(IProvideSubscriptionClient provider)
        {
            if (!provider.Client.IsClosedOrClosing)
            {
                await provider.Client.CloseAsync();
            }
        }

        private static MessageHandlerOptions MessageOptions(Func<ExceptionReceivedEventArgs, Task> exceptionHandler)
        {
            var options = new MessageHandlerOptions(exceptionHandler)
            {
                AutoComplete = false,
                MaxConcurrentCalls = 1,
            };

            return options;
        }
    }
}
