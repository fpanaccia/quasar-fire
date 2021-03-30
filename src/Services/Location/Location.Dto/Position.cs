using System;

namespace Location.Dto
{
    public class Position
    {
        public Position()
        {

        }

        public Position(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }
    }
}
