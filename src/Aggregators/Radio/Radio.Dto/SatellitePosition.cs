using System;

namespace Radio.Dto
{
    public class SatellitePosition : Position
    {
        public SatellitePosition(string name, double x, double y) : base(x, y)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
