using HWCommon.Commands;
using System;

namespace HW4
{
    class Program
    {
        static void Main(string[] args)
        {
            var cmd = new CommandHandler();
            new BaseCommands(cmd);

            Console.WriteLine(new TimeInterval(15,240,10,10));

            BaseConsoleIO.Start(cmd);
        }
    }
}
