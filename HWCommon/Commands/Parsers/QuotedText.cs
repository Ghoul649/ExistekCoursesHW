namespace HWCommon.Commands.Parsers
{
    public enum QuotesType 
    {
        Single,
        Double,
        Both
    }
    public class QuotedText : ParserAttribute 
    {
        public QuotesType QuotesType { get; set; } = QuotesType.Both;
        public bool Escaping { get; set; } = true;
        public char EscapingChar { get; set; } = '\\';
        
        public override string Parse(string value, ref int i)
        {
            if (!value.Skip(ref i, ' '))
                return null;
            bool dq = false;
            if (value[i] == '\'')
            {
                if (QuotesType == QuotesType.Double)
                    return null;
            }
            else if (value[i] == '"') 
            {
                if (QuotesType == QuotesType.Single)
                    return null;
                dq = true;
            }
            int startIndex = ++i;
            if (Escaping)
            {
                if (!value.Find(ref i, dq ? '"' : '\'', EscapingChar))
                    return null;

                return value.Substring(startIndex, i++ - startIndex).CompleteEscaping(EscapingChar);
            }
            else
            {
                if (!value.Find(ref i, dq ? '"' : '\''))
                    return null;
                return value.Substring(startIndex, i++ - startIndex);
            }
            
        }

        public override string Sample(string value)
        {
            string val = value.Replace("\\", "\\\\").Replace(QuotesType == QuotesType.Single ? "'" : "\"", QuotesType == QuotesType.Single ? "\\'" : "\\\"}");
            return (QuotesType == QuotesType.Single ? "'" : "\"") + val + (QuotesType == QuotesType.Single ? "'" : "\"");
        }
    }
}
