namespace Husa.Extensions.UserInfo
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Response = Husa.CompanyServicesManager.Api.Contracts.Response;

    public interface IUserCacheRepository
    {
        Task<IEnumerable<Response.User>> GetUsers(IEnumerable<Guid> userIds);
    }
}
