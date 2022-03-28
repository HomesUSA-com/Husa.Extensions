namespace Husa.Extensions.UserInfo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Husa.Extensions.Cache;
    using Husa.Extensions.UserInfo.Interfaces;

    public abstract class UserQueriesRepository : IQueryUsersRepository
    {
        private readonly ICache cache;

        protected UserQueriesRepository(
            ICache memoryCache)
        {
            this.cache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
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

            var users = await this.GetUsersById(userIds);
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

        public async Task<IEnumerable<IUserEntity>> GetUsersById(IEnumerable<Guid> userIds)
        {
            var users = new List<IUserEntity>();

            if (!userIds.Any())
            {
                return users;
            }

            var idsNotInCache = new List<Guid>();
            foreach (Guid userId in userIds)
            {
                if (this.cache.Contains(userId.ToString()))
                {
                    var user = this.cache.Get(userId.ToString());
                    users.Add(user as IUserEntity);
                }
                else
                {
                    idsNotInCache.Add(userId);
                }
            }

            var usersNotInCache = await this.GetUsersNotInCache(idsNotInCache);

            foreach (IUserEntity user in usersNotInCache)
            {
                this.cache.Insert(user.Id.ToString(), user, 3600);
                users.Add(user);
            }

            return users;
        }

        public virtual Task<IEnumerable<IUserEntity>> GetUsersNotInCache(IEnumerable<Guid> userIds)
        {
            throw new NotImplementedException();
        }
    }
}
