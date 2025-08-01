namespace Husa.Extensions.ShowingTime.Interfaces
{
    using System;

    public interface IContactOrder
    {
        Guid ContactId { get; set; }
        Guid ScopeId { get; set; }
        int Order { get; set; }
    }
}
