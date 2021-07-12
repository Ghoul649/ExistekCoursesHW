using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace HW4
{
    interface IVehicle
    {
        Point Position { get; set; }
        double Value { get; set; }
        double Speed { get; set; }
        int Year { get; set; }
        void Move(Point location, Map map);
    }
}
