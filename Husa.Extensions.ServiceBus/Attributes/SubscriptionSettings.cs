namespace Husa.Extensions.ServiceBus.Attributes
{
    using System.ComponentModel.DataAnnotations;

    public class SubscriptionSettings
    {
        [Required]
        public string Name { get; set; }
    }
}
