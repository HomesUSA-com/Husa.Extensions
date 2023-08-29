namespace Husa.Extensions.ServiceBus.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Azure.Messaging.ServiceBus;
    using Husa.Extensions.Common.Enums;
    using Husa.Extensions.ServiceBus.Attributes;
    using Husa.Extensions.ServiceBus.Extensions;
    using Husa.Extensions.ServiceBus.Interfaces;
    using Microsoft.Extensions.Logging;

    public abstract class MessagingServiceBusBase : IMessagingServiceBusBase
    {
        private readonly ServiceBusClient client = null;
        private readonly ServiceBusSender sender = null;
        private readonly string topicName;
        private readonly ILogger logger;

        protected MessagingServiceBusBase(ILogger logger, ServiceBusClient client, string topicName)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.client = client ?? throw new ArgumentNullException(nameof(client));
            this.topicName = topicName ?? throw new ArgumentNullException(nameof(topicName));
            this.sender = client.CreateSender(topicName);
        }

        public async Task SendMessage<T>(
            IEnumerable<T> messages,
            string userId,
            string correlationId = null,
            bool dispose = true)
            where T : IProvideBusEvent => await this.SendMessageBatch(messages, userId, market: null, correlationId, dispose);

        public async Task SendMessage<T>(
            IEnumerable<T> messages,
            string userId,
            MarketCode market,
            string correlationId = null,
            bool dispose = true)
            where T : IProvideBusEvent => await this.SendMessageBatch(messages, userId, market, correlationId, dispose);

        public async Task DisposeClient()
        {
            this.logger.LogInformation("Closing connection with the Azure Service Bus made for topic {topicName}.", this.topicName);
            await this.sender.DisposeAsync();
            await this.client.DisposeAsync();
        }

        private async Task SendMessageBatch<T>(
            IEnumerable<T> messages,
            string userId,
            MarketCode? market,
            string correlationId,
            bool dispose = true)
            where T : IProvideBusEvent
        {
            try
            {
                using ServiceBusMessageBatch messageBatch = await this.sender.CreateMessageBatchAsync();

                foreach (var message in messages)
                {
                    this.logger.LogInformation("Starting to send a message with id {messageId}. for topic {topicName}", message.Id, this.topicName);
                    var msgType = message.GetType();
                    var messageProp = msgType.GetProperty("Message");
                    var serviceBusMessage = (messageProp is not null)
                        ? new ServiceBusMessage(messageProp.GetValue(message).SerializeMessage())
                        : new ServiceBusMessage(message.SerializeMessage());
                    serviceBusMessage.ApplicationProperties.Add(MessageMetadataConstants.BodyTypeField, typeof(T).FullName);
                    serviceBusMessage.ApplicationProperties.Add(MessageMetadataConstants.AssemblyNameField, typeof(T).AssemblyQualifiedName);
                    serviceBusMessage.ApplicationProperties.Add(MessageMetadataConstants.UserIdField, userId);
                    if (market.HasValue)
                    {
                        serviceBusMessage.ApplicationProperties.Add(MessageMetadataConstants.MarketField, market.Value.ToString());
                    }

                    var marketProp = msgType.GetProperty("Market");
                    if (marketProp is not null)
                    {
                        var msgMarket = (MarketCode?)marketProp.GetValue(message);
                        if (msgMarket.HasValue)
                        {
                            serviceBusMessage.ApplicationProperties.Add(MessageMetadataConstants.MarketField, msgMarket.Value.ToString());
                        }
                    }

                    if (string.IsNullOrEmpty(correlationId))
                    {
                        serviceBusMessage.CorrelationId = Guid.NewGuid().ToString();
                    }

                    if (!messageBatch.TryAddMessage(serviceBusMessage))
                    {
                        // if it is too large for the batch
                        this.logger.LogWarning("The message {messageId} is too large to fit in the batch.", message.Id);
                    }
                }

                // Use the producer client to send the batch of messages to the Service Bus topic
                await this.sender.SendMessagesAsync(messageBatch);
                this.logger.LogInformation("A batch of messages has been published to the topic {topicName}.", this.topicName);
            }
            finally
            {
                if (dispose)
                {
                    await this.DisposeClient();
                }
            }
        }
    }
}
