namespace Husa.Extensions.Downloader.Trestle.Helpers.Converters
{
    using System.Text.Json.Serialization;
    using System.Text.Json;
    using System;

    public class IntToStringConverter : JsonConverter<string>
    {
        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            if (value != null)
            {
                writer.WriteStringValue(value);
            }
        }

        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
            reader.TokenType switch
            {
                JsonTokenType.Null => null,
                JsonTokenType.String => reader.GetString(),
                JsonTokenType.Number => reader.TryGetInt64(out long l) ? Convert.ToString(l) : null,
                _ => throw new JsonException(),
            };
    }
}
