using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace HW4
{
    class Ship : IVehicle
    {
        public Point Position { get; set; }
        public double Value { get; set; }
        public double Speed { get; set; }
        public int Year { get; set; }

        public void Move(Point location, Map map)
        {
            throw new NotImplementedException();
        }
    }
}
