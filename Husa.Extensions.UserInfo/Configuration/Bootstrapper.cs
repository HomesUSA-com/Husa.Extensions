namespace Husa.Extensions.UserInfo.Configuration
{
    using Microsoft.Extensions.DependencyInjection;

    public static class Bootstrapper
    {
        public static IServiceCollection UseUserCache(this IServiceCollection services)
        {
            services.AddScoped<IUserCacheRepository, UserCacheRepository>();
            return services;
        }
    }
}
