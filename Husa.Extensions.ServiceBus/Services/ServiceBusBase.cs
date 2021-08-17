namespace Husa.Extensions.ServiceBus.Services
{
    using System;
    using System.Threading.Tasks;
    using Husa.Extensions.ServiceBus.Extensions;
    using Husa.Extensions.ServiceBus.Interfaces;
    using Microsoft.Azure.ServiceBus;
    using Microsoft.Extensions.Logging;

    public abstract class ServiceBusBase : IServiceBusBase
    {
        internal TopicClient TopicClient = null;
        private readonly ILogger logger;

        public ServiceBusBase(ILogger logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task SendMessage<T>(T eventMessage)
            where T : IProvideBusEvent
        {
            try
            {
                this.logger.LogInformation($"Starting to send a message with id {eventMessage.Id} to the topic: '{this.TopicClient.TopicName}'.");

                var message = new Message(eventMessage.SerializeMessage());

                await this.TopicClient.SendAsync(message);

                this.logger.LogInformation($"Message to the topic: '{this.TopicClient.TopicName}' was sent.");
            }
            catch (Exception exception)
            {
                this.logger.LogError(exception, $"The application failed while attempting to send the message to the topic '{this.TopicClient.TopicName}'. With the following exception message: \n\n{exception.Message}");
            }
            finally
            {
                this.logger.LogInformation($"Closing connection with the Azure Service Bus made for topic '{this.TopicClient.TopicName}'.");
                await this.TopicClient.CloseAsync();
            }
        }
    }
}
