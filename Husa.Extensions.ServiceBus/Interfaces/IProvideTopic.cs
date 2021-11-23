namespace Husa.Extensions.ServiceBus.Interfaces
{
    using System.Collections.Generic;
    using Microsoft.Azure.ServiceBus;

    public interface IProvideTopic
    {
        IEnumerable<ITopicClient> TopicClients { get; }
    }
}
