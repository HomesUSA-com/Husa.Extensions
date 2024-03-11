namespace Husa.Extensions.Document.Configuration
{
    using Husa.Extensions.Document.Interfaces;
    using Husa.Extensions.Document.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;

    public static class CosmosDbBootstrapper
    {
        public static IServiceCollection AddCosmosLinqQuery(this IServiceCollection services)
        {
            services.AddScoped<ICosmosLinqQuery, CosmosLinqQuery>();
            return services;
        }

        public static CosmosJsonDotNetSerializer BuildCosmosJsonDotNetSerializer()
        {
            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            };

            var jsonConverter = new StringEnumConverter(
                namingStrategy: new CamelCaseNamingStrategy(),
                allowIntegerValues: true);
            jsonSerializerSettings.Converters.Add(jsonConverter);
            return new(jsonSerializerSettings);
        }
    }
}
