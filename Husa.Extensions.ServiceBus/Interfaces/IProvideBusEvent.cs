namespace Husa.Extensions.ServiceBus.Interfaces
{
    using System;

    public interface IProvideBusEvent
    {
        Guid Id { get; set; }
    }
}
