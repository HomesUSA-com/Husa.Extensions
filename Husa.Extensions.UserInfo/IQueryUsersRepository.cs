namespace Husa.Extensions.UserInfo
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Husa.Extensions.UserInfo.Interfaces;

    public interface IQueryUsersRepository
    {
        Task FillUserNameAsync(IEnumerable<IProvideUserInfo> userInfos);

        Task FillUserNameAsync(IProvideUserInfo userInfo);

        Task<IEnumerable<IUserEntity>> GetUsersById(IEnumerable<Guid> userIds);
    }
}
