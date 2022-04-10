namespace Husa.Extensions.ServiceBus.Attributes
{
    using System.ComponentModel.DataAnnotations;

    public class TopicSettings
    {
        [Required]
        public string ConnectionString { get; set; }

        [Required]
        public string Name { get; set; }

        public SubscriptionSettings Subscription { get; set; }
    }
}
