namespace Husa.Extensions.ServiceBus.Attributes
{
    using System.ComponentModel.DataAnnotations;
    public class ServiceBusOptions
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "A TopicName must be provided.")]
        public string TopicName { get; set; }

        public string SubscriptionName { get; set; }
    }
}
