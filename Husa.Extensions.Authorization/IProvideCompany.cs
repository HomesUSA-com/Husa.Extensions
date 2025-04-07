namespace Husa.Extensions.Authorization
{
    using System;

    public interface IProvideCompany
    {
        Guid CompanyId { get; }
    }
}
