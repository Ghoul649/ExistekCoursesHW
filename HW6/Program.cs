using HWCommon.Commands;
using System;
using System.Collections.Generic;

namespace HW6
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
        List<Point> points = new List<Point>();
        [Command("Create new point")]
        public void Create(double X, double Y) 
        {
            var point = new Point() { X = X, Y = Y };
            points.Add(point);
            Console.WriteLine($"Created new point with name '{point.CharName}'");
        }
        [Command("Display points")]
        public void Display() 
        {
            foreach(var p in points) 
                Console.WriteLine(p);
        }
    }
}
