using System.Collections.Generic;
using System.Linq;
using Spark.Core.Enum;
using Spark.Packet.Extension;

namespace Spark.Packet.Notification
{
    [Packet("sayi")]
    public class Sayi : IPacket
    {
        public EntityType EntityType { get; set; }
        public long EntityId { get; set; }
        public MessageColor Color { get; set; }
        public int MessageId { get; set; }
        public List<int> Parameters { get; set; }

        public void Construct(string[] packet)
        {
            EntityType = packet[0].ToEnum<EntityType>();
            EntityId = packet[1].ToLong();
            Color = packet[2].ToEnum<MessageColor>();
            MessageId = packet[3].ToInt();
            Parameters = new List<int>();

            foreach (string value in packet.Skip(4))
            {
                Parameters.Add(value.ToInt());
            }
        }
    }
}