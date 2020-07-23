using Spark.Core.Extension;

namespace Spark.Packet.Characters
{ 
    public class Stat : IPacket
    {
        public int Hp { get; set; }
        public int Mp { get; set; }
        public int MaxHp { get; set; }
        public int MaxMp { get; set; }
    }
}