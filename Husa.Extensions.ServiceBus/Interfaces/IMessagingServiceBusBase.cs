namespace Husa.Extensions.ServiceBus.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMessagingServiceBusBase
    {
        Task SendMessage<T>(IEnumerable<T> messages, string userId, string correlationId = null, bool dispose = true)
            where T : IProvideBusEvent;

        Task DisposeClient();
    }
}
