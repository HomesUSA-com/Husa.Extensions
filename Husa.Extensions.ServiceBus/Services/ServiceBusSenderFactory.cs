namespace Husa.Extensions.ServiceBus.Services;

using System;
using System.Collections.Concurrent;
using Azure.Messaging.ServiceBus;
using Husa.Extensions.ServiceBus.Interfaces;
using Microsoft.Extensions.Logging;

public class ServiceBusSenderFactory : IServiceBusSenderFactory
{
    private readonly ServiceBusClient serviceBusClient;
    private readonly ILogger<ServiceBusSenderFactory> logger;
    private readonly ConcurrentDictionary<string, ServiceBusSender> senders = new();

    public ServiceBusSenderFactory(ServiceBusClient serviceBusClient, ILogger<ServiceBusSenderFactory> logger)
    {
        this.serviceBusClient = serviceBusClient ?? throw new ArgumentNullException(nameof(serviceBusClient));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public ServiceBusSender GetSender(string topicName)
    {
        if (string.IsNullOrWhiteSpace(topicName))
        {
            throw new ArgumentException($"'{nameof(topicName)}' cannot be null or whitespace.", nameof(topicName));
        }

        this.logger.LogInformation("Getting sender for topic {Topic}", topicName);
        return this.senders.GetOrAdd(topicName, this.serviceBusClient.CreateSender);
    }
}
