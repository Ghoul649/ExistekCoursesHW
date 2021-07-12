using System;

namespace HWCommon.Commands.Parsers
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public abstract class ParserAttribute : Attribute
    {
        public abstract string Parse(string value, ref int i);
        public abstract string Sample(string value);
    }
}
