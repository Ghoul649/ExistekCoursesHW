using System;

namespace HWCommon.Commands
{
    public static class BaseConsoleIO
    {
        public static void Start(CommandHandler manager)
        {
            while (true)
            {
                try
                {
                    Console.Write("->");
                    manager.Execute(Console.ReadLine());
                }
                catch (Exception e)
                {
                    var c = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.ForegroundColor = c;
                }
            }
        }
    }
}
