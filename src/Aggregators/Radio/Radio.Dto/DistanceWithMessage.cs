using System;

namespace Radio.Dto
{
    public class DistanceWithMessage
    {
        public DistanceWithMessage()
        {

        }

        public DistanceWithMessage(DistanceWithMessage distanceWithMessage)
        {
            Distance = distanceWithMessage.Distance;
            Message = distanceWithMessage.Message;
        }

        public double Distance { get; set; }
        public string[] Message { get; set; }
    }
}
