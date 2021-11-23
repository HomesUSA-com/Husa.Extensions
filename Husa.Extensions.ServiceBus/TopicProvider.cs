namespace Husa.Extensions.ServiceBus
{
    using System;
    using System.Collections.Generic;
    using Husa.Extensions.ServiceBus.Interfaces;
    using Microsoft.Azure.ServiceBus;

    public class TopicProvider : IProvideTopic
    {
        public TopicProvider(IEnumerable<ITopicClient> topicClients)
        {
            this.TopicClients = topicClients ?? throw new ArgumentNullException(nameof(topicClients));
        }

        public IEnumerable<ITopicClient> TopicClients { get; }
    }
}
