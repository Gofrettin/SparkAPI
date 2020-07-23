using System.Collections.Generic;
using System.Linq;
using Spark.Core.Enum;
using Spark.Core.Extension;

namespace Spark.Packet.Chat
{
    public class Msgi : IPacket
    {
        public MessageType MessageType { get; set; }
        public int MessageId { get; set; }
        public IEnumerable<int> Parameters { get; set; }
    }
}