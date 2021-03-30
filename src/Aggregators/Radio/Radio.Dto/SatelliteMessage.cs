using System;

namespace Radio.Dto
{
    public class SatelliteMessage : DistanceWithMessage
    {
        public SatelliteMessage()
        {

        }

        public SatelliteMessage(string name, DistanceWithMessage distanceWithMessage) : base(distanceWithMessage)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
