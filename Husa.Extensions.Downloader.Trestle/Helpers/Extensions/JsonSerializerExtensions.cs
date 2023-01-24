namespace Husa.Extensions.Downloader.Trestle.Helpers.Extensions
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public static class JsonSerializerExtensions
    {
        public static JsonSerializerOptions SetConfiguration(this JsonSerializerOptions options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.AllowTrailingCommas = true;

            options.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));

            return options;
        }
    }
}
