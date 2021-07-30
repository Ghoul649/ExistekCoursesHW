using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HW3
{
    class DataSizeConverter : JsonConverter<DataSize>
    {
        public override DataSize Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new DataSize(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, DataSize value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
