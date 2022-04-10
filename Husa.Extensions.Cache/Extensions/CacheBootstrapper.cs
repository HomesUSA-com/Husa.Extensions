namespace Husa.Extensions.Cache.Extensions
{
    using Microsoft.Extensions.DependencyInjection;

    public static class CacheBootstrapper
    {
        public static IServiceCollection UseCache(this IServiceCollection services)
        {
            return services
                   .AddScoped<ICache, InMemoryCache>();
        }
    }
}
