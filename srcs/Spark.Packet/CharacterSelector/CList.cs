using Spark.Core.Extension;

namespace Spark.Packet.CharacterSelector
{
    public class CList : IPacket
    {
        public int Slot { get; set; }
        public string Name { get; set; }
    }
}