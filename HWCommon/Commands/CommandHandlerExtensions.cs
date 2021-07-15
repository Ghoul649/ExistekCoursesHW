using System;
using System.Collections.Generic;
using System.Text;

namespace HWCommon.Commands
{
    public static class CommandHandlerExtensions
    {
        public static void UseBaseCommands(this CommandHandler cmd)
        {
            new BaseCommands(cmd);
        }
        public static void UseBaseConsoleIO(this CommandHandler cmd) 
        {
            BaseConsoleIO.Start(cmd);
        }
    }
}
