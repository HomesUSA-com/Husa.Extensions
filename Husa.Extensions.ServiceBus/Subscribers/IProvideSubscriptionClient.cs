namespace Husa.Extensions.ServiceBus.Subscribers
{
    using Microsoft.Azure.ServiceBus;

    public interface IProvideSubscriptionClient
    {
        public ISubscriptionClient Client { get; set; }
    }
}
