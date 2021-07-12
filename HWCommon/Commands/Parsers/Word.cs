using System;
using System.Linq;

namespace HWCommon.Commands.Parsers
{
    public class Word : ParserAttribute
    {
        public bool Escaping { get; set; } = false;
        public override string Parse(string value, ref int index)
        {
            if (!value.Skip(ref index, ' '))
                return null;
            int startIndex = index ;
            value.Find(ref index, ' ');
            return value.Substring(startIndex, index - startIndex);
        }

        public override string Sample(string value)
        {
            var res = value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (res.Any())
                return res.First();
            return "value";
        }
    }
}
