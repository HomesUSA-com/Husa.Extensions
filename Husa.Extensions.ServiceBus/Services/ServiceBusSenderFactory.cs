namespace Husa.Extensions.ServiceBus.Services;

using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Husa.Extensions.ServiceBus.Interfaces;
using Microsoft.Extensions.Logging;

public class ServiceBusSenderFactory : IServiceBusSenderFactory, IAsyncDisposable
{
    private readonly ServiceBusClient serviceBusClient;
    private readonly ILogger<ServiceBusSenderFactory> logger;
    private readonly ConcurrentDictionary<string, ServiceBusSender> senders = new();
    private bool disposed;

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

    public async ValueTask DisposeAsync()
    {
        await this.DisposeAsyncCore();

        // Suppress finalization.
        GC.SuppressFinalize(this);
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
        if (this.disposed)
        {
            return;
        }

        // Dispose all the senders asynchronously
        foreach (var (topic, sender) in this.senders)
        {
            try
            {
                await sender.DisposeAsync();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Failed to dispose sender for topic {Topic}", topic);
            }
        }

        // Clear the dictionary
        this.senders.Clear();

        this.disposed = true;
    }
}
