using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace HWCommon.Commands
{
    public delegate string CommandAction(object[] args);
    public class Command
    {
        public string Keyword { get; protected set; }
        public string Description { get; set; }
        public bool IgnoreCase { get; set; } = true;
        public CommandAction Action { get; protected set; }
        public MethodInfo Method { get; protected set; }
        public IEnumerable<Parameter> Parameters { get => _params; }
        protected List<Parameter> _params = new List<Parameter>();
        protected object _module;
        public Command(CommandAttribute attr, MethodInfo method, object module) 
        {
            _module = module;
            Method = method;
            Keyword = attr?.Keyword ?? method.Name;
            Description = attr?.Description;
            foreach (var param in method.GetParameters()) 
            {
                _params.Add(new Parameter(param));
            }
            Action = args =>
            {
                return Method.Invoke(_module, args)?.ToString();
            };
        }
        public virtual bool Parse(string command) 
        {
            if (!command.StartsWith(Keyword, IgnoreCase? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
                return false;
            int index = Keyword.Length;
            object[] args = new object[_params.Count];
            if (command.Length > index)
                if (command[index] != ' ')
                    return false;

            for (int i = 0; i < _params.Count; i++) 
            {
                try
                {
                    args[i] = _params[i].Parse(command, ref index);
                }
                catch (ArgumentException e) 
                {
                    throw new Exception($"Wrong syntax. Use: {GetSyntax()}");
                }
            }
            string extraStr = null;
            if (command.Length > ++index) 
            {
                extraStr = command.Substring(index);
                if (!string.IsNullOrWhiteSpace(extraStr))
                    return false;
            }
            try
            {
                Action.Invoke(args);
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
            return true;
        }
        public string GetSyntax() 
        {
            StringBuilder sb = new StringBuilder(Keyword);
            foreach (var param in Parameters)
            {
                sb.Append($" {param.Parser.Sample(param.Name)}");
            }
            return sb.ToString();
        }
    }
}
