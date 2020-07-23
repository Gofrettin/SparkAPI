using Spark.Core.Extension;

namespace Spark.Packet.Characters
{
    public class CInfo : IPacket
    {
        public string Name { get; set; }
        public long Id { get; set; }
    }
}