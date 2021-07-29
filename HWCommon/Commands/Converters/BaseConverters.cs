
namespace HWCommon.Commands.Converters
{
    public static class BaseConverters
    {
        [Converter(typeof(string))]
        public static object StringConv(string value) 
        {
            return value;
        }

        [Converter(typeof(int))]
        public static object IntConv(string value)
        {
            return int.Parse(value);
        }
        [Converter(typeof(uint))]
        public static object UIntConv(string value)
        {
            return uint.Parse(value);
        }
        [Converter(typeof(long))]
        public static object LongConv(string value)
        {
            return long.Parse(value);
        }
        [Converter(typeof(ulong))]
        public static object ULongConv(string value)
        {
            return ulong.Parse(value);
        }
        [Converter(typeof(float))]
        public static object FloatConv(string value)
        {
            return float.Parse(value);
        }
        [Converter(typeof(double))]
        public static object DoubleConv(string value)
        {
            return double.Parse(value);
        }
        [Converter(typeof(System.Type))]
        public static object ToType(string value)
        {
            return AllowedTypes.GetType(value);
        }
    }
}
