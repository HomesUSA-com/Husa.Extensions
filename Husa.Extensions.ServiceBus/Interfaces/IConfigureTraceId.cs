namespace Husa.Extensions.ServiceBus.Interfaces
{
    public interface IConfigureTraceId
    {
        void SetTraceId(string traceId = null);
    }
}
