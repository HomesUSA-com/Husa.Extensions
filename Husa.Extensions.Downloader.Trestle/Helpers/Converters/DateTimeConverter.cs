namespace Husa.Extensions.Downloader.Trestle.Helpers.Converters
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class DateTimeConverter : JsonConverter<DateTime?>
    {
        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (value != null)
            {
                writer.WriteStringValue((DateTime)value);
            }
        }

        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
            reader.TryGetDateTime(out var dateTime) ? DateTime.SpecifyKind(dateTime, DateTimeKind.Utc) : null;
    }
}
