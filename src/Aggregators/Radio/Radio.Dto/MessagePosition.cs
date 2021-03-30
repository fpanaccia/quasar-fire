using System;

namespace Radio.Dto
{
    public class MessagePosition
    {
        public MessagePosition(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }
    }
}
