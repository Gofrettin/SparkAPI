using Spark.Packet.Extension;

namespace Spark.Packet.Characters
{
    [Packet("stat")]
    public class Stat : IPacket
    {
        public int Hp { get; set; }
        public int Mp { get; set; }
        public int MaxHp { get; set; }
        public int MaxMp { get; set; }
        
        public void Construct(string[] packet)
        {
            Hp = packet[0].ToInt();
            Mp = packet[2].ToInt();
            MaxHp = packet[1].ToInt();
            MaxMp = packet[3].ToInt();
        }
    }
}