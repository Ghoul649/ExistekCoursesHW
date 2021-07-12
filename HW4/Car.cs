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

        public TimeInterval Move(Point location)
        {
            var dist = Math.Sqrt(Math.Pow(Position.X - location.X, 2) + Math.Pow(Position.Y - location.Y, 2));
            Position = location;
            return new TimeInterval((int)(60 * dist / Speed));
        }
    }
}
