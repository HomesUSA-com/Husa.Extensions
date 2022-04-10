namespace Husa.Extensions.ServiceBus.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Azure.ServiceBus;

    public interface IHandleMessages
    {
        Task HandleMessage(Message message, CancellationToken cancellationToken);

        Task HandleException(ExceptionReceivedEventArgs exceptionReceived);
    }
}
