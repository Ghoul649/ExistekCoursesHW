using System;
using HWCommon;
using HWCommon.Commands;

namespace HW8
{
    class Program
    {
        static void Main(string[] args)
        {
            AllowedTypes.LoadBase();
            AllowedTypes.Add("matrix", typeof(Matrix<double>));

            CommandHandler manager = new CommandHandler();
            manager.UseBaseCommands();
            manager.Init(new Core(manager));
            Console.WriteLine("->info");
            manager.Execute("info");
            Console.WriteLine("->help");
            manager.Execute("help");
            manager.UseBaseConsoleIO();
        }
    }
}
