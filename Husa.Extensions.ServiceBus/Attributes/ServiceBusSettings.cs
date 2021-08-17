namespace Husa.Extensions.ServiceBus.Attributes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ServiceBusSettings
    {
        public const string Section = "ServiceBus";

        [Required(AllowEmptyStrings = false, ErrorMessage = "A Service Bus connection string must be provided.")]
        public string ConnectionString { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "A Service Bus Name must be provided.")]
        public string Name { get; set; }

        [Required]
        [MinLength(1)]
        public IEnumerable<TopicSettings> Topics { get; set; }
    }
}
