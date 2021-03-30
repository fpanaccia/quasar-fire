using System;

namespace Location.Dto
{
    public class PositionWithDistance : Position
    {
        public PositionWithDistance()
        {

        }

        public PositionWithDistance(double x, double y, double distance) : base(x, y)
        {
            Distance = distance;
        }

        public double Distance { get; set; }
    }
}
