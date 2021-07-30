using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HW3
{
    [JsonConverter(typeof(DataSpeedConverter))]
    struct DataSpeed
    {
        public DataSize DataSize;
        public DataSpeed(ulong value)
        {
            DataSize = new DataSize(value);
        }
        public DataSpeed(string value)
        {
            string[] args = value.Split('/');
            if (args.Length == 1)
                try
                {
                    DataSize = new DataSize(value);
                    return;
                }
                catch (FormatException e)
                {
                    throw new FormatException($"Wrong format. Use format like \"123kB\", \"73kB/s\", \"1,1GB/s\"");
                }
            if (args.Length > 2)
                throw new FormatException($"Wrong format. Use format like \"123kB\", \"73kB/s\", \"1,1GB/s\"");
            DataSize = new DataSize(args[0]);
        }
        public override string ToString()
        {
            return $"{DataSize}/s";
        }
    }
}
