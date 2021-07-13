using HWCommon.Commands;
using HWCommon.Commands.Parsers;
using System;
using System.Collections.Generic;

namespace HW4
{
    class Program
    {
        static void Main(string[] args)
        {
            var cmd = new CommandHandler();
            new BaseCommands(cmd);
            cmd.Init(new Program());
            Console.WriteLine(new TimeInterval(15,240,10,10));

            BaseConsoleIO.Start(cmd);
        }

        List<IVehicle> Vehicles = new List<IVehicle>();

        [Command("Display vehicles")]
        public void PrintList() 
        {
            Console.WriteLine("\nVehicles:");
            int index = 0;
            foreach (var v in Vehicles)
                Console.WriteLine($"{index++}.\t{v.GetInfo()}");
        }

        [Command("Move vehicle")]
        public void Move(int index, int X, int Y) 
        {
            Console.WriteLine(Vehicles[index].Move(new System.Drawing.Point(X, Y)));
        }

        [Command("Create new vehicle type of Car")]
        public void CreateCar(double value, double speed, int year) 
        {
            Vehicles.Add(new Car() { Value = value, Speed = speed, Year = year });
        }

        [Command("Create new vehicle type of Plane")]
        public void CreatePlane(double value, double speed, int year,int passengers, int height)
        {
            Vehicles.Add(new Plane() { Value = value, Speed = speed, Year = year, Passengers = passengers, Height = height });
        }

        [Command("Create new vehicle type of Ship")]
        public void CreateShip(double value, double speed, int year,int passengers, [QuotedText] string port)
        {
            Vehicles.Add(new Ship() { Value = value, Speed = speed, Year = year, Passengers = passengers, Port = port });
        }
    }
}
