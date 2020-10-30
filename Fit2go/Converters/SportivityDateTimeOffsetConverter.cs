using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Fit2go.Converters
{
    public class SportivityDateTimeOffsetConverter : JsonConverter<DateTimeOffset>
    {
        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number)
            {
                return DateTimeOffset.FromUnixTimeMilliseconds(reader.GetInt64());
            }
            else
            {
                string seconds = reader.GetString();
                if (long.TryParse(seconds, out long parsed))
                {
                    return DateTimeOffset.FromUnixTimeMilliseconds(parsed);
                }
                else
                {
                    return DateTimeOffset.MinValue;
                }
            }
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToUnixTimeMilliseconds().ToString());
        }
    }
}
