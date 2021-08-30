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
        private readonly IOptions<ServiceBusSettings> busSettings;

        public UserSubscriber(IOptions<ServiceBusSettings> busSettings)
        {
            this.busSettings = busSettings ?? throw new ArgumentNullException(nameof(busSettings));
            var topicSettings = this.busSettings.Value.Topics.Single(t => t.Name == TopicName);

            this.Client = new SubscriptionClient(
                this.busSettings.Value.ConnectionString,
                topicSettings.Name,
                topicSettings.Subscription.Name);
        }

        public ISubscriptionClient Client { get; set; }
    }
}
