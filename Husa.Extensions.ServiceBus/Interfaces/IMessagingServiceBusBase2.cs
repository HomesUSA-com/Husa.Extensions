namespace Husa.Extensions.ServiceBus.Interfaces
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Husa.Extensions.Common.Enums;

    public interface IMessagingServiceBusBase2
    {
        Task SendMessage<T>(IEnumerable<T> messages, string userId, string correlationId = null, CancellationToken cancellationToken = default)
            where T : IProvideBusEvent;

        Task SendMessage<T>(IEnumerable<T> messages, string userId, MarketCode market, string correlationId = null, CancellationToken cancellationToken = default)
            where T : IProvideBusEvent;
    }
}
