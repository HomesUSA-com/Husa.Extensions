namespace Husa.Extensions.ServiceBus.Interfaces
{
    using System.Threading.Tasks;

    public interface IServiceBusBase
    {
        Task SendMessage<T>(T eventMessage, string userId = null, string correlationId = null)
            where T : IProvideBusEvent;

        Task SendMessageNoDispose<T>(T eventMessage, string userId = null, string correlationId = null)
            where T : IProvideBusEvent;

        Task DisposeClient();
    }
}
