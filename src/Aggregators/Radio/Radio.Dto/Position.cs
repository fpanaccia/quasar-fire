﻿using System;

namespace Radio.Dto
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
