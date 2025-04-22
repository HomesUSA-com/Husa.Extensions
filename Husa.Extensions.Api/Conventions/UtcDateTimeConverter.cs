namespace Husa.Extensions.Api.Conventions
{
    using System;
    using System.Globalization;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class UtcDateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string dateString = reader.GetString();

            // Check if the string ends with 'Z' (UTC format)
            if (dateString != null && dateString.EndsWith("Z", StringComparison.OrdinalIgnoreCase))
            {
                // Parse as UTC
                return DateTime.Parse(dateString, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
            }

            // Default parsing
            return DateTime.Parse(dateString, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            // For UTC dates, ensure they're formatted with 'Z'
            if (value.Kind == DateTimeKind.Utc)
            {
                writer.WriteStringValue(value.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture));
            }
            else
            {
                writer.WriteStringValue(value.ToString("yyyy-MM-ddTHH:mm:ss.fff", CultureInfo.InvariantCulture));
            }
        }
    }
}
