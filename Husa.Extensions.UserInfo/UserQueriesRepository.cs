namespace Husa.Extensions.UserInfo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Husa.Extensions.UserInfo.Interfaces;

    public class UserQueriesRepository : IQueryUsersRepository
    {
        private readonly IUserCacheRepository userCacheRepository;

        public UserQueriesRepository(IUserCacheRepository memoryCache)
        {
            this.userCacheRepository = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public async Task FillUserNameAsync(IEnumerable<IProvideUserInfo> userInfos)
        {
            var userIds = userInfos.SelectMany((entity) =>
            {
                var ids = new List<Guid>();
                if (entity.SysCreatedBy.HasValue && !entity.SysCreatedBy.Value.Equals(Guid.Empty))
                {
                    ids.Add(entity.SysCreatedBy.Value);
                }

                if (entity.SysModifiedBy.HasValue && !entity.SysModifiedBy.Value.Equals(Guid.Empty))
                {
                    ids.Add(entity.SysModifiedBy.Value);
                }

                return ids;
            }).Distinct().ToList();

            var users = await this.userCacheRepository.GetUsers(userIds);
            if (!users.Any())
            {
                return;
            }

            var information = from entity in userInfos
                              join createdByUser in users on entity.SysCreatedBy equals createdByUser.Id into createdMatch
                              from createdByUser in createdMatch.DefaultIfEmpty()
                              join modifiedByUser in users on entity.SysModifiedBy equals modifiedByUser.Id into modifiedMatch
                              from modifiedByUser in modifiedMatch.DefaultIfEmpty()
                              select new
                              {
                                  Entity = entity,
                                  CreatedByUser = createdByUser != null ? $"{createdByUser.FirstName} {createdByUser.LastName}" : string.Empty,
                                  ModifiedByUser = modifiedByUser != null ? $"{modifiedByUser.FirstName} {modifiedByUser.LastName}" : string.Empty,
                              };

            foreach (var userInfo in information)
            {
                userInfo.Entity.ModifiedBy = userInfo.ModifiedByUser;
                userInfo.Entity.CreatedBy = userInfo.CreatedByUser;
            }
        }

        public async Task FillUserNameAsync(IProvideUserInfo userInfo)
        {
            if (userInfo is null)
            {
                return;
            }

            await this.FillUserNameAsync(new List<IProvideUserInfo> { userInfo });
        }
    }
}
