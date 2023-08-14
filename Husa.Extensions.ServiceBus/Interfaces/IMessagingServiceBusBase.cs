namespace Husa.Extensions.ServiceBus.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Husa.Extensions.Common.Enums;

    public interface IMessagingServiceBusBase
    {
        Task SendMessage<T>(IEnumerable<T> messages, string userId, string correlationId = null, bool dispose = true)
            where T : IProvideBusEvent;

        Task SendMessage<T>(IEnumerable<T> messages, string userId, MarketCode market, string correlationId = null, bool dispose = true)
            where T : IProvideBusEvent;

        Task DisposeClient();
    }
}
