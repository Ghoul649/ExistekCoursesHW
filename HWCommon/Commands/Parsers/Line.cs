using System;
using System.Collections.Generic;
using System.Text;

namespace HWCommon.Commands.Parsers
{
    public class Line : ParserAttribute
    {
        public override string Parse(string value, ref int i)
        {
            if (!value.Skip(ref i, ' '))
                return null;
            int start = i;
            i = value.Length;
            return value.Substring(start, i - start);
        }

        public override string Sample(string value)
        {
            return value;
        }
    }
}
