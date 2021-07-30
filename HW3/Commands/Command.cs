using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW.Common.Commands
{
    public class Command
    {
        public string Description { get; set; }
        private string _keyWord; 
        public string KeyWord
        { 
            get => _keyWord; 
            set 
            {
                for (int i = 0; i < value.Length; i++)
                {
                    if (!char.IsLetterOrDigit(value[i]))
                        throw new ArgumentException("Keyword must consist of characters or digits");
                }
                _keyWord = value;
            } 
        }
        private List<Parameter> parameters = new List<Parameter>();
        public Action<object[]> Action { get; set; }
        public Command AddParam(Parameter p) 
        {
            parameters.Add(p);
            return this;
        }
        public string GetSyntax() 
        {
            string res = KeyWord;
            foreach (Parameter p in parameters) 
            {
                res += $" {p.GetCommandParameter()} ";
            }
            return $"{KeyWord}";
        }
        public string GetInfo() 
        {
            return $"{KeyWord} - {Description}";
        }
        public string GetFullInfo() 
        {
            string res = $"{GetSyntax()}\n\tDescription: {Description}\n\tParameters:\n";
            foreach (Parameter p in parameters) 
            {
                res += $"{p.GetInfo()};\n";
            }
            return res;
        }
        public bool Parse(string line, Action<string> echo) 
        {
            if (!line.StartsWith(KeyWord))
                return false;
            line = line.Remove(0, KeyWord.Length);
            if (line.Length > 0 && line[0] != ' ')
                return false;
            int i = 0;
            object[] args = new object[parameters.Count];
            int index = 0;
            foreach (var param in parameters)
            {
                string arg;
                switch (param.ParameterType)
                {
                    case ParameterType.OneWord:
                        arg = Parameter.ParseOneWord(line, ref index);
                        break;
                    case ParameterType.String:
                        arg = Parameter.ParseString(line, ref index);
                        break;
                    default :
                        arg = Parameter.ParseLine(line, ref index);
                        break;
                }
                if (arg == null)
                {
                    echo?.Invoke($"Error! Parameter \"{param.Name}\" is not specified. Use syntaxis {GetSyntax()}");
                    return true;
                }
                var obj = param.Convert(arg,echo);
                if (obj == null)
                    return true;
                args[i++] = obj;
            }
            Action?.Invoke(args);
            return true;
        }
        
    }
}
