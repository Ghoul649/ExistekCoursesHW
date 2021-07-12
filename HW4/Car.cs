using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace HW4
{
    class Car : IVehicle
    {
        public Point Position { get; set; }
        public double Value { get; set; }
        public double Speed { get; set; }
        public int Year { get; set; }

        public void Move(Point location, Map map)
        {
            Island currentIsland = null;
            foreach (var l in map.Islands) 
            {
                if (Math.Sqrt(Math.Pow(l.Position.X - Position.X, 2) + Math.Pow(l.Position.Y - Position.Y, 2)) <= l.Radius)
                {
                    currentIsland = l;
                    break;
                }
            }
            if (currentIsland == null)
                throw new Exception("The car is sunk");
            Island targetIsland = null;
            foreach (var l in map.Islands)
            {
                if (Math.Sqrt(Math.Pow(l.Position.X - location.X, 2) + Math.Pow(l.Position.Y - location.Y, 2)) <= l.Radius)
                {
                    targetIsland = l;
                    break;
                }
            }

            if (targetIsland == null)
                throw new Exception("The car sank");

        }
    }
}
