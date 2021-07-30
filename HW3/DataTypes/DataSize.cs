using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HW3
{
    [JsonConverter(typeof(DataSizeConverter))]
    struct DataSize
    {
        private static string[] _units = new string[] { "kB", "MB", "GB", "TB", "PB", "EB" };
        private static string[] _lowerUnits;
        public ulong KBytes;
        public DataSize(ulong kb)
        {
            KBytes = kb;
        }
        public DataSize(string value)
        {
            if (_lowerUnits == null)
                init();

            if (char.IsDigit(value.Last()))
            {
                if (!ulong.TryParse(value, out ulong val))
                    throw new FormatException($"Wrong format. Use format like \"123kB\", \"73,7MB\", \"10,1TB\"");
                KBytes = val;
                return;
            }
            string tmp = value.ToLower();
            for (int i = 0; i < _units.Length; i++)
                if (tmp.EndsWith(_lowerUnits[i]))
                {
                    if (!double.TryParse(value.Substring(0, value.Length - _units[i].Length).Trim(), out double val))
                        throw new FormatException($"Wrong format. Use format like \"123kB\", \"73,7MB\", \"10,1TB\"");
                    KBytes = (ulong)(val * Math.Pow(2, i * 10) / (value[value.Length - 1] == 'b' ? 8 : 1));
                    return;
                }
            throw new ArgumentOutOfRangeException("Unknown data unit. Use \"kB\", \"MB\", \"GB\", \"TB\", \"PB\", \"EB\".");
        }
        public override string ToString()
        {
            int mod = (int)(Math.Log(KBytes, 2) / 10);
            mod = Math.Min(_units.Length - 1, mod);
            return $"{Math.Round(KBytes / Math.Pow(2, mod * 10), 1)}{_units[mod]}";
        }
        private static void init()
        {
            _lowerUnits = new string[_units.Length];
            for (int i = 0; i < _lowerUnits.Length; i++)
                _lowerUnits[i] = _units[i].ToLower();
        }
    }

}
