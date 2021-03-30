using System;

namespace Radio.Dto
{
    public class DecodedMessage
    {
        public DecodedMessage(double x, double y, string message)
        {
            Message = message;
            Position = new MessagePosition(x, y);
        }

        public MessagePosition Position { get; set; }
        public string Message { get; set; }
    }
}
