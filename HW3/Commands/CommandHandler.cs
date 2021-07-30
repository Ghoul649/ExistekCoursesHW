using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW.Common.Commands
{
    public class CommandHandler
    {
        public Action<string> Echo = (msg) => Console.WriteLine(msg);
        private List<Command> _commands = new List<Command>();
        public IEnumerable<Command> Commands { get => _commands; }
        public void AddCommand(Command cmd) 
        {
            foreach (var command in _commands)
                if (cmd.KeyWord == command.KeyWord)
                    throw new Exception("Keywords must be different");
            _commands.Add(cmd);
        }
        public void Execute(string line) 
        {
            foreach(var cmd in _commands) 
            {
                if (cmd.Parse(line, Echo))
                    return;
            }
            Echo?.Invoke("Unknown command");
        }
    } 
}
