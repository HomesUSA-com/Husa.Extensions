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
        private readonly ITopicClient topicClient = null;
        private readonly ILogger logger;

        protected ServiceBusBase(ILogger logger, ITopicClient topicClient)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.topicClient = topicClient ?? throw new ArgumentNullException(nameof(topicClient));
        }

        public async Task SendMessage<T>(
            T eventMessage,
            string userId = null,
            string correlationId = null,
            bool dispose = true)
            where T : IProvideBusEvent
        {
            try
            {
                this.logger.LogInformation("Starting to send a message with id {messageId} to the topic: '{topicName}'.", eventMessage.Id, this.topicClient.TopicName);

                var message = new Message(eventMessage.SerializeMessage());
                message.UserProperties["BodyType"] = typeof(T).FullName;
                message.UserProperties["AssemblyName"] = typeof(T).AssemblyQualifiedName;
                message.UserProperties["UserId"] = userId;
                message.CorrelationId = correlationId;
                await this.topicClient.SendAsync(message);

                this.logger.LogInformation("Message to the topic: '{topicName}' was sent.", this.topicClient.TopicName);
            }
            finally
            {
                if (dispose)
                {
                    await this.DisposeClient();
                }
            }
        }

        public async Task DisposeClient()
        {
            this.logger.LogInformation("Closing connection with the Azure Service Bus made for topic '{topicName}'.", this.topicClient.TopicName);
            await this.topicClient.CloseAsync();
        }
    }
}
