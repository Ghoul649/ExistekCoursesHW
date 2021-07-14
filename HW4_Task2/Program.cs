using HWCommon.Commands;
using System;

namespace HW4_Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandHandler manager = new CommandHandler();
            manager.UseBaseCommands();
            manager.Init(new Program());
            manager.Execute("help");
            manager.UseBaseConsoleIO();
        }
        Circle c = new Circle() { Radius = 1 };
        [Command("Execute delegate")]
        public void Execute() 
        {
            Console.WriteLine($"Result: {c.GetScalar()}");
        }
        [Command("Set the radius of the circle")]
        public void SetRadius(double newRadius) 
        {
            c.Radius = newRadius;
        }
        [Command("Set function")]
        public void SetFunction([Param("Type of function. Allowed values: Radius, Length, Diameter, Area")]FunctionsType func) 
        {
            c.TransformFunction = FunctionsTypeExtension.GetFunction(func);
        }
    }
}
