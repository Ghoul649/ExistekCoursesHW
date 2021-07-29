using System;
using HWCommon;
using HWCommon.Commands;

namespace HW8
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandHandler manager = new CommandHandler();
            manager.UseBaseCommands();
            manager.Init(new Core());
            manager.UseBaseConsoleIO();
        }
    }
}
