using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace HW4
{
    class Plane : IVehicle
    {
        public Point Position { get; set; }
        public double Value { get; set; }
        public double Speed { get; set; }
        public int Year { get; set; }
        public int Passengers { get; set; }
        public int Height { get; set; }
        public string GetInfo()
        {
            return $"Plane: Value - {Value}, Speed - {Speed}, Year = {Year}, Passengers - {Passengers}, Position - ({Position.X}; {Position.Y}), Altitude - {Height}";
        }
        public TimeInterval Move(Point location)
        {
            var dist = Math.Sqrt(Math.Pow((Position.X - location.X), 2) + Math.Pow((Position.Y - location.Y), 2));
            Position = location;
            return new TimeInterval(60 + (int)(60 * dist / (1000 * Speed)));
        }
    }
}
