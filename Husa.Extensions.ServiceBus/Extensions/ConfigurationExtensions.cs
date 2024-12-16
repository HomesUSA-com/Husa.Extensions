namespace Husa.Extensions.ServiceBus.Extensions
{
    using System;
    using System.Threading.Tasks;
    using Husa.Extensions.ServiceBus.Handlers;
    using Husa.Extensions.ServiceBus.Interfaces;
    using Husa.Extensions.ServiceBus.Services;
    using Husa.Extensions.ServiceBus.Subscribers;
    using Microsoft.Azure.ServiceBus;
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
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            return CloseClientAsync(provider);
        }

        public static IServiceCollection UseTraceIdProvider(this IServiceCollection services)
        {
            return services
                   .AddScoped<TraceIdProvider>()
                   .AddScoped<IProvideTraceId>(x => x.GetRequiredService<TraceIdProvider>())
                   .AddScoped<IConfigureTraceId>(x => x.GetRequiredService<TraceIdProvider>());
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
