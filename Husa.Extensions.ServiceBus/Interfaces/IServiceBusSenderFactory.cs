namespace Husa.Extensions.ServiceBus.Interfaces;

using Azure.Messaging.ServiceBus;

public interface IServiceBusSenderFactory
{
    ServiceBusSender GetSender(string topicName);
}
