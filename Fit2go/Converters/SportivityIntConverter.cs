using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Fit2go.Converters
{
    public class SportivityIntConverter : JsonConverter<int>
    {
        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options) =>
            writer.WriteStringValue(value.ToString());
    }
}
