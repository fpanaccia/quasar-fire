using System;

namespace Radio.Dto
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
