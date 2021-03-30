using System;

namespace Satellite.Dto
{
    public class SatellitePosition
    {
        public SatellitePosition(string name, decimal x, decimal y)
        {
            Name = name;
            X = x;
            Y = y;
        }

        public string Name { get; set; }
        public decimal X { get; set; }
        public decimal Y { get; set; }
    }
}
