namespace Husa.Extensions.UserInfo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Husa.CompanyServicesManager.Api.Client.Interfaces;
    using Husa.CompanyServicesManager.Api.Contracts.Request;
    using Husa.Extensions.Cache;
    using Response = Husa.CompanyServicesManager.Api.Contracts.Response;

    public class UserCacheRepository : IUserCacheRepository
    {
        private readonly IServiceSubscriptionClient serviceSubscriptionClient;
        private readonly ICache cache;

        public UserCacheRepository(ICache memoryCache, IServiceSubscriptionClient serviceSubscriptionClient)
        {
            this.cache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            this.serviceSubscriptionClient = serviceSubscriptionClient ?? throw new ArgumentNullException(nameof(serviceSubscriptionClient));
        }

        public async Task<IEnumerable<Response.User>> GetUsers(IEnumerable<Guid> userIds)
        {
            var users = new List<Response.User>();

            if (userIds == null || !userIds.Any())
            {
                return users;
            }

            var idsNotInCache = new List<Guid>();
            foreach (Guid userId in userIds)
            {
                var user = this.GetUserFromCache(userId);
                if (user != null)
                {
                    users.Add(user);
                }
                else
                {
                    idsNotInCache.Add(userId);
                }
            }

            var takeValue = 50;
            while (idsNotInCache.Any())
            {
                var ids = idsNotInCache.Take(takeValue);
                var usersNotInCache = await this.serviceSubscriptionClient.User
                    .GetAsync(new UserRequest { Ids = ids });

                foreach (Response.User user in usersNotInCache.Data)
                {
                    string cacheKey = user.Id.ToString();
                    this.cache.Insert(cacheKey, user, 3600);
                    users.Add(user);
                }

                idsNotInCache = idsNotInCache.Skip(takeValue).ToList();
            }

            return users;
        }

        private Response.User GetUserFromCache(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return null;
            }

            var cacheKey = userId.ToString();
            return this.cache.Contains(cacheKey)
                ? this.cache.Get(cacheKey) as Response.User
                : null;
        }
    }
}
