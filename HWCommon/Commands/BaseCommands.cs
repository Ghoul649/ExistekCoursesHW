using System;
using System.Linq;

namespace HWCommon.Commands
{
    public class BaseCommands
    {
        protected CommandHandler manager;
        public BaseCommands(CommandHandler cmd) 
        {
            manager = cmd;
            cmd.Init(this);
        }

        [Command("Display info about commands")]
        public void help()
        {
            Console.WriteLine("\nCommands:");
            foreach (Command cmd in manager)
            {
                Console.WriteLine($"\t{cmd.Keyword} - {cmd.Description}");
            }
        }
        [Command("Display info about specified command")]
        public void command([Param(Description = "Command")] string Command)
        {
            var cmd = manager[Command];
            Console.Write("\nSyntax:\n\t");
            Console.WriteLine(cmd.GetSyntax());
            if (cmd.Description != null)
            {
                Console.Write("\nDescription:\n\t");
                Console.WriteLine(cmd.Description);
            }

            if (cmd.Parameters.Any())
            {
                Console.Write("\nParameters:\n");
                foreach (var param in cmd.Parameters)
                    Console.WriteLine($"\t{AllowedTypes.GeTypeName(param.ParameterType)} {param.Name} {(string.IsNullOrWhiteSpace(param.Description)?' ':'-')} {param.Description}");
            }
            Console.Write("\nOutput:\n");
            Console.WriteLine($"\t{cmd.Output}");

        }
    }
}
