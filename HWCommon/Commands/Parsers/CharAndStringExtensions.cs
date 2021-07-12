using System.Text;

namespace HWCommon.Commands.Parsers
{
    public static class CharAndStringExtensions
    {
        public static bool IsOneOf(this char c, char[] arr) 
        {
            for (int i = 0; i < arr.Length; i++) 
                if (c == arr[i])
                    return true;
            return false;
        }
        public static bool Skip(this string value, ref int i, char c)
        {
            while (i < value.Length)
            {
                if (value[i] != c)
                    return true;
                i++;
            }
            return false;
        }
        public static bool Skip(this string value, ref int i, char[] chars)
        {
            while (i < value.Length)
            {
                if (!value[i].IsOneOf(chars))
                    return true;
                i++;
            }
            return false;
        }
        public static bool Find(this string value, ref int i, char c) 
        {
            while (i < value.Length)
            {
                if (value[i] == c)
                    return true;
                i++;
            }
            return false;
        }
        public static bool Find(this string value, ref int i, char c, char escaping)
        {
            bool esc = false;
            while (i < value.Length)
            {
                if (esc)
                {
                    esc = false;
                    i++;
                    continue;
                }
                if (value[i] == escaping)
                    esc = true;
                if (value[i] == c)
                    return true;
                i++;
            }
            return false;
        }
        public static bool Find(this string value, ref int i, char[] chars)
        {
            while (i < value.Length)
            {
                if (value[i].IsOneOf(chars))
                    return true;
                i++;
            }
            return false;
        }
        public static bool Find(this string value, ref int i, char[] chars, char escaping)
        {
            bool esc = false;
            while (i < value.Length)
            {
                if (esc)
                {
                    esc = false;
                    continue;
                }
                if (value[i] == escaping)
                    esc = true;
                if (value[i].IsOneOf(chars))
                    return true;
                i++;
            }
            return false;
        }
        public static string CompleteEscaping(this string value, char escChar) 
        {
            StringBuilder b = new StringBuilder(value.Length);
            int index = 0;
            bool esc = false;
            while (index < value.Length) 
            {
                int startIndex = index;
                if (esc)
                {
                    esc = false;
                    index++;
                }
                if (!value.Find(ref index, escChar))
                {
                    b.Append(value, startIndex, index - startIndex);
                    return b.ToString();
                }
                b.Append(value, startIndex, index - startIndex);
                index += 1;
                esc = true;
            }
            return b.ToString();
        }
    }
}
