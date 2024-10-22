namespace Husa.Extensions.ServiceBus.Services;

using System;
using System.Threading.Tasks;
using Husa.Extensions.Common.Enums;
using Husa.Extensions.ServiceBus.Attributes;
using Husa.Extensions.ServiceBus.Extensions;
using Husa.Extensions.ServiceBus.Interfaces;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;

public abstract class ServiceBusBase2 : IServiceBusBase2
{
    private readonly ITopicClient topicClient = null;
    private readonly ILogger logger;

    protected ServiceBusBase2(ITopicClient topicClient, ILogger logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        this.topicClient = topicClient ?? throw new ArgumentNullException(nameof(topicClient));
    }

    public async Task SendMessage<T>(
        T eventMessage,
        string userId = null,
        string correlationId = null)
        where T : IProvideBusEvent => await this.SendSingleMessage(eventMessage, userId, market: null, correlationId);

    public async Task SendMessage<T>(
        T eventMessage,
        string userId,
        MarketCode market,
        string correlationId = null)
        where T : IProvideBusEvent => await this.SendSingleMessage(eventMessage, userId, market, correlationId);

    public async Task SendSingleMessage<T>(
        T eventMessage,
        string userId,
        MarketCode? market,
        string correlationId)
        where T : IProvideBusEvent
    {
        this.logger.LogInformation("Starting to send a message with id {MessageId} to the topic: '{Topic}'.", eventMessage.Id, this.topicClient.TopicName);

        var message = new Message(eventMessage.SerializeMessage());
        message.UserProperties[MessageMetadataConstants.BodyTypeField] = typeof(T).FullName;
        message.UserProperties[MessageMetadataConstants.AssemblyNameField] = typeof(T).AssemblyQualifiedName;
        message.UserProperties[MessageMetadataConstants.UserIdField] = userId;

        if (market.HasValue)
        {
            message.UserProperties[MessageMetadataConstants.MarketField] = market.Value.ToString();
        }

        if (string.IsNullOrEmpty(correlationId))
        {
            message.CorrelationId = Guid.NewGuid().ToString();
        }

        await this.topicClient.SendAsync(message);

        this.logger.LogInformation("Message to the topic: '{Topic}' was sent.", this.topicClient.TopicName);
    }
}
