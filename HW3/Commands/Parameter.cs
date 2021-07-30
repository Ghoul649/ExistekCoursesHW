using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HW.Common.Commands
{
    public enum ParameterType
    {
        OneWord,
        String,
        Line
    }
    public abstract class Parameter 
    {
        private string _name;
        public string Name { get=>_name;
            set 
            {
                for (int i = 0; i < value.Length; i++) 
                {
                    if (!char.IsLetterOrDigit(value[i]))
                        throw new ArgumentException("Name must consist of characters or digits");
                }
                _name = value;
            }
        }
        public ParameterType ParameterType { get; set; }
        public string Description { get; set; }
        public override string ToString() => Name;
        public abstract string GetCommandParameter();
        public abstract string GetInfo();
        public abstract object Convert(string value, Action<string> echo);
        public static string ParseOneWord(string line, ref int i) 
        {
            int startIndex = i; 
            bool param = false;
            while (i < line.Length) 
            {
                if (line[i] == ' ')
                {
                    i++;
                    continue; 
                }
                param = true;
                startIndex = i;
                break;
            }
            if (!param)
                return null;
            while (i < line.Length)
            {
                if (line[i] != ' ')
                {
                    i++;
                    continue;
                }
                break;
            }
            return line.Substring(startIndex, i - startIndex);
        }
        public static string ParseString(string line, ref int i) 
        {
            int startIndex = i;
            bool param = false;
            bool doubleQuotes = false;
            while (i < line.Length)
            {
                if (line[i] == '"')
                {
                    doubleQuotes = true;
                    startIndex = i++;
                    param = true;
                    break;
                }
                if (line[i] == '\'') 
                {
                    startIndex = i++;
                    param = true;
                    break;
                }
                i++;
            }
            if (!param)
                return null;
            while (i < line.Length)
            {
                if (line[i] == '\\') 
                {
                    i++;
                    if (i < line.Length)
                    {
                        i++;
                        continue;
                    }
                    break;
                }
                if ((doubleQuotes && line[i] != '"') || (!doubleQuotes && line[i] != '\''))
                {
                    i++;
                    continue;
                }
                i++;
                break;
            }
            if (i - startIndex <= 2)
                return "";
            return line.Substring(startIndex + 1, i - startIndex - 2)
                .Replace("\\'", "'")
                .Replace("\\\"", "\"")
                .Replace("\\\\", "\\");
        }
        public static string ParseLine(string line, ref int i)
        {
            int startIndex = i;
            bool param = false;
            while (i < line.Length)
            {
                if (line[i] == ' ')
                {
                    i++;
                    continue;
                }
                param = true;
                startIndex = i;
                break;
            }
            i = line.Length;
            if (!param)
                return "";
            return line.Substring(startIndex);
        }
    }
    public class Parameter<T> : Parameter
    { 
        public Func<string, T> Converter { get; set; }
        public override string GetCommandParameter()
        {
            if (ParameterType == ParameterType.OneWord)
                return $"{{({typeof(T).Name}){Name}}}";
            if (ParameterType == ParameterType.String)
                return $"\"{{({typeof(T).Name}){Name}}}\"";
            return $"{{({typeof(T).Name}){Name}}}...";
        }
        public override string GetInfo()
        {
            return $"{{({typeof(T).Name}){Name}}} - {Description}";
        }
        public override object Convert(string value, Action<string> echo)
        {
            if (Converter == null)
            {
                if (typeof(T) == typeof(string))
                {
                    return value;
                }
                var method = typeof(T).GetMethods().Where(type => type.Name == "Parse" && type.IsPublic && type.IsStatic && type.GetParameters().Where(param => param.ParameterType == typeof(string)).Any()).First();
                if (method == null)
                    throw new Exception($"There is no method \"Parse\" for type \"{typeof(T)}\"");
                try
                {
                    return method.Invoke(null, new object[] { value });
                }
                catch (TargetInvocationException e)
                {
                    echo?.Invoke($"Filed converting value \"{value}\" of parameter \"{Name}\" to Type \"{typeof(T).Name}\": {e.InnerException.Message}");
                    return null;
                }
            }
            return Converter(value);
                    
        }
    }
}
