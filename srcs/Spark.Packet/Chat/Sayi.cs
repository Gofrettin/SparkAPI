using System.Collections.Generic;
using System.Linq;
using Spark.Core.Enum;
using Spark.Core.Extension;

namespace Spark.Packet.Chat
{
    public class Sayi : IPacket
    {
        public EntityType EntityType { get; set; }
        public long EntityId { get; set; }
        public MessageColor Color { get; set; }
        public int MessageId { get; set; }
        public IEnumerable<int> Parameters { get; set; }
    }
}