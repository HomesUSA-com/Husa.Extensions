namespace Husa.Extensions.Downloader.Trestle.Helpers.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public class StringListEnumConverter<T> : JsonConverter<IEnumerable<T>>
    {
        public override IEnumerable<T> Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new ArgumentException(string.Format("Unexpected token on StringListEnumConverter. Expected String, got {0}.", reader.TokenType));
            }

            string stringList = reader.GetString().Trim();
            char[] delimiter = new char[1] { ',' };
            string[] stringEnums = stringList.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
            var result = new List<T>();

            for (int sequence = 0; sequence <= stringEnums.Length - 1; sequence++)
            {
                var value = stringEnums[sequence];
                if (!string.IsNullOrEmpty(value))
                {
                    if (Enum.IsDefined(typeof(T), value))
                    {
                        result.Add((T)Enum.Parse(typeof(T), value));
                    }
                    else
                    {
                        throw new ArgumentException(string.Format("Unexpected value parsing. Expected {0} string, got {1}", typeof(T).Name, value));
                    }
                }
            }

            return result;
        }

        public override void Write(Utf8JsonWriter writer, IEnumerable<T> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
