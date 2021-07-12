using System;
using System.Collections.Generic;
using System.Text;

namespace HW4
{
    class Map
    {
        public List<Island> Islands { get; } = new List<Island>();
        public List<Airport> Airports { get; } = new List<Airport>();
        public List<IVehicle> Vehicles { get; } = new List<IVehicle>();

    }
}
