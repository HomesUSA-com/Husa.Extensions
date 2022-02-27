namespace Husa.Extensions.Authorization.Extensions
{
    using Microsoft.Extensions.DependencyInjection;

    public static class AuthorizationBootstrapper
    {
        public static IServiceCollection UseAuthorizationContext(this IServiceCollection services)
        {
            return services
                   .AddScoped<UserProvider>()
                   .AddScoped<IUserContextProvider>(x => x.GetRequiredService<UserProvider>())
                   .AddScoped<IUserProvider>(x => x.GetRequiredService<UserProvider>());
        }
    }
}
