namespace Husa.Extensions.Downloader.Trestle.Helpers.Converters
{
    using System.Text.Json.Serialization;
    using System.Text.Json;
    using System;

    public class StringNullableEnumConverter<T> : JsonConverter<T>
    {
        private readonly JsonConverter<T> converter;
        private readonly Type underlyingType;

        public StringNullableEnumConverter()
            : this(null)
        {
        }

        public StringNullableEnumConverter(JsonSerializerOptions options)
        {
            if (options != null)
            {
                this.converter = (JsonConverter<T>)options.GetConverter(typeof(T));
            }

            // cache the underlying type
            this.underlyingType = Nullable.GetUnderlyingType(typeof(T));
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return typeof(T).IsAssignableFrom(typeToConvert);
        }

        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (this.converter != null)
            {
                return this.converter.Read(ref reader, this.underlyingType, options);
            }

            string value = reader.GetString();

            if (string.IsNullOrEmpty(value))
            {
                return default;
            }

            // for performance, parse with ignoreCase:false first.
            if (!Enum.TryParse(this.underlyingType, value, ignoreCase: false, out object result)
            && !Enum.TryParse(this.underlyingType, value, ignoreCase: true, out result))
            {
                throw new JsonException(
                    $"Unable to convert \"{value}\" to Enum \"{underlyingType}\".");
            }

            return (T)result;
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value?.ToString());
        }
    }
}
