namespace Husa.Extensions.ServiceBus.Extensions
{
    using System;
    using System.Threading.Tasks;
    using Husa.Extensions.ServiceBus.Handlers;
    using Husa.Extensions.ServiceBus.Subscribers;
    using Microsoft.Azure.ServiceBus;

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

        public static async Task CloseClient(this IProvideSubscriptionClient provider)
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

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
