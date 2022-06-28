namespace Husa.Extensions.Api.Client
{
    using Microsoft.Extensions.DependencyInjection;

    public static class HttpClientExtensions
    {
        public static IServiceCollection ConfigureHeaderPropagation(this IServiceCollection services)
        {
            services.AddHeaderPropagation(options =>
            {
                options.Headers.Add("Authorization");
                options.Headers.Add("Cookie");
                options.Headers.Add("CurrentCompanySelected");
            });

            return services;
        }
    }
}
