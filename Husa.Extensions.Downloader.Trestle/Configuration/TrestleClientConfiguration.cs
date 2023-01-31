namespace Husa.Extensions.Downloader.Trestle.Configuration
{
    using Husa.Extensions.Downloader.Trestle.Models;
    using Husa.Extensions.Downloader.Trestle.Services;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class TrestleClientConfiguration
    {
        public static IServiceCollection AddTrestleServices(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddScoped<IBlobTableRepository, BlobTableRepository>();
            services.AddScoped<ITrestleRequester, TrestleRequester>();
            services.AddScoped<ITrestleClient, TrestleClient>();
            return services;
        }

        public static IServiceCollection BindTrestleOptions(this IServiceCollection services)
        {
            services.AddOptions<MarketOptions>()
                .Configure<IConfiguration>((settings, config) => config.GetSection(MarketOptions.Section).Bind(settings));
            services.AddOptions<BlobOptions>()
                .Configure<IConfiguration>((settings, config) => config.GetSection(BlobOptions.Section).Bind(settings));

            return services;
        }
    }
}
