namespace Husa.Extensions.ServiceBus.Services;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Husa.Extensions.Common.Enums;
using Husa.Extensions.ServiceBus.Attributes;
using Husa.Extensions.ServiceBus.Extensions;
using Husa.Extensions.ServiceBus.Interfaces;
using Microsoft.Extensions.Logging;

public abstract class MessagingServiceBusBase2 : IMessagingServiceBusBase2
{
    private readonly ServiceBusSender sender = null;
    private readonly string topicName;
    private readonly ILogger logger;

    protected MessagingServiceBusBase2(IServiceBusSenderFactory senderFactory, string topicName, ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(senderFactory);

        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.sender = senderFactory.GetSender(topicName);
        this.topicName = topicName;
    }

    public async Task SendMessage<T>(
        IEnumerable<T> messages,
        string userId,
        string correlationId = null,
        CancellationToken cancellationToken = default)
        where T : IProvideBusEvent => await this.SendMessageBatch(messages, userId, market: null, correlationId, cancellationToken);

    public async Task SendMessage<T>(
        IEnumerable<T> messages,
        string userId,
        MarketCode market,
        string correlationId = null,
        CancellationToken cancellationToken = default)
        where T : IProvideBusEvent => await this.SendMessageBatch(messages, userId, market, correlationId, cancellationToken);

    private async Task SendMessageBatch<T>(
        IEnumerable<T> messages,
        string userId,
        MarketCode? market,
        string correlationId,
        CancellationToken cancellationToken = default)
        where T : IProvideBusEvent
    {
        using ServiceBusMessageBatch messageBatch = await this.sender.CreateMessageBatchAsync(cancellationToken);

        foreach (var message in messages)
        {
            this.logger.LogInformation("Starting to send a message with id {MessageId}. for topic {Topic}", message.Id, this.topicName);
            var serviceBusMessage = new ServiceBusMessage(message.SerializeMessage());
            serviceBusMessage.ApplicationProperties.Add(MessageMetadataConstants.BodyTypeField, typeof(T).FullName);
            serviceBusMessage.ApplicationProperties.Add(MessageMetadataConstants.AssemblyNameField, typeof(T).AssemblyQualifiedName);
            serviceBusMessage.ApplicationProperties.Add(MessageMetadataConstants.UserIdField, userId);
            if (market.HasValue)
            {
                serviceBusMessage.ApplicationProperties.Add(MessageMetadataConstants.MarketField, market.Value.ToString());
            }

            if (string.IsNullOrEmpty(correlationId))
            {
                serviceBusMessage.CorrelationId = Guid.NewGuid().ToString();
            }

            if (!messageBatch.TryAddMessage(serviceBusMessage))
            {
                // if it is too large for the batch
                this.logger.LogWarning("The message {MessageId} is too large to fit in the batch.", message.Id);
            }
        }

        // Use the producer client to send the batch of messages to the Service Bus topic
        await this.sender.SendMessagesAsync(messageBatch, cancellationToken);
        this.logger.LogInformation("A batch of messages has been published to the topic {Topic}.", this.topicName);
    }
}
