namespace Husa.Extensions.Api
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Husa.Extensions.Api.Conventions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    public static class MvcConfigurationExtensions
    {
        private const string DefaultApiPrefix = "api";

        public static MvcOptions AddControllerPrefixConventions(this MvcOptions options, string prefix = DefaultApiPrefix, params string[] excluded)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            options.Conventions.Add(new RoutePrefixConvention(prefix, excluded));

            return options;
        }

        public static JsonSerializerOptions SetConfiguration(this JsonSerializerOptions options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.IgnoreNullValues = false;
            options.AllowTrailingCommas = true;

            options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));

            return options;
        }
    }
}
