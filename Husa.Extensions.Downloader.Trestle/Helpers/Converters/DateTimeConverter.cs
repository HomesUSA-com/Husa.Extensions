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

        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TryGetDateTime(out var dateTime))
            {
                if (dateTime.TimeOfDay.TotalSeconds == 0)
                {
                    dateTime = dateTime.AddHours(12);
                }

                return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
            }

            return null;
        }
    }
}
