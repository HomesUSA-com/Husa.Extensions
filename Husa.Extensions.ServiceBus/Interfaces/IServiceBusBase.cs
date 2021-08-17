namespace Husa.Extensions.ServiceBus.Interfaces
{
    using System.Threading.Tasks;

    public interface IServiceBusBase
    {
        Task SendMessage<T>(T eventMessage)
            where T : IProvideBusEvent;
    }
}
