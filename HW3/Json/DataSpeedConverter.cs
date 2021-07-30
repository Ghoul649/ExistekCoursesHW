using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HW3
{
    class DataSpeedConverter : JsonConverter<DataSpeed>
    {
            public override DataSpeed Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return new DataSpeed(reader.GetString());
            }

            public override void Write(Utf8JsonWriter writer, DataSpeed value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString());
            }
    }
}
