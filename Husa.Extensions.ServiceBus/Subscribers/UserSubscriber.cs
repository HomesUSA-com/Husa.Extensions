namespace Husa.Extensions.ServiceBus.Subscribers
{
    using System;
    using System.Linq;
    using Husa.Extensions.ServiceBus.Attributes;
    using Microsoft.Azure.ServiceBus;
    using Microsoft.Extensions.Options;

    public class UserSubscriber : ISubscribeToUserService
    {
        public const string TopicName = "topic-user";

        public UserSubscriber(IOptions<ServiceBusSettings> busOptions)
        {
            var busSettings = busOptions is null ? throw new ArgumentNullException(nameof(busOptions)) : busOptions.Value;
            var topicSettings = busSettings.Topics.Single(t => t.Name == TopicName);
            this.Client = new SubscriptionClient(
                busSettings.ConnectionString,
                topicSettings.Name,
                topicSettings.Subscription.Name);
        }

        public ISubscriptionClient Client { get; set; }
    }
}
