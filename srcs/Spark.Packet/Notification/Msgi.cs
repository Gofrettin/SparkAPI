using System.Collections.Generic;
using System.Linq;
using Spark.Core.Enum;
using Spark.Packet.Extension;

namespace Spark.Packet.Notification
{
    [Packet("msgi")]
    public class Msgi : IPacket
    {
        public MessageType MessageType { get; set; }
        public int MessageId { get; set; }
        public List<short> Parameters { get; set; }
        
        public void Construct(string[] packet)
        {
            MessageType = packet[0].ToEnum<MessageType>();
            MessageId = packet[1].ToInt();
            Parameters = new List<short>();

            foreach (string value in packet.Skip(2))
            {
                Parameters.Add(value.ToShort());
            }
        }
    }
}