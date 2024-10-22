namespace Husa.Extensions.ServiceBus.Interfaces;

using System.Threading.Tasks;

public interface IServiceBusBase2
{
    Task SendMessage<T>(T eventMessage, string userId = null, string correlationId = null)
        where T : IProvideBusEvent;
}
