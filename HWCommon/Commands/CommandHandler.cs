using HWCommon.Commands.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HWCommon.Commands
{
    public class CommandHandler : IEnumerable<Command>
    {
        protected static bool _convertersLoaded = false;
        protected List<Command> _commands = new List<Command>();
        public CommandHandler() 
        {
            if (!_convertersLoaded)
                Converter.ReloadConverters();
        }
        public void Init(object module)
        {
            Type moduleType = module.GetType();
            var moduleMethods = moduleType.GetMethods()
                .Where(method => method.IsPublic);
            foreach (var method in moduleMethods) 
            {
                foreach (var attr in method.GetCustomAttributes(typeof(CommandAttribute), true)) 
                {
                    _commands.Add(new Command(attr as CommandAttribute, method, module));
                }
            }
        }
        public void Execute(string value) 
        {
            foreach (var cmd in _commands)
                if (cmd.Parse(value))
                    return;
            throw new ArgumentException($"Unknown command");
        }

        public IEnumerator<Command> GetEnumerator() => _commands.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public Command this[string keyword] 
        {
            get 
            {
                foreach (var cmd in this) 
                {
                    if (cmd.Keyword == keyword)
                        return cmd;
                }
                return null;
            }
        }
    }
}
