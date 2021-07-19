using HWCommon.Commands;
using System;

namespace HW7
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandHandler manager = new CommandHandler();
            manager.UseBaseCommands();
            manager.Init(new Core());
            manager.Execute("help");
            manager.UseBaseConsoleIO();
        }
    }
}
