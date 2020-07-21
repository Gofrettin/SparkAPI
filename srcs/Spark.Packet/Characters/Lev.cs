using Spark.Packet.Extension;

namespace Spark.Packet.Characters
{
    [Packet("lev")]
    public class Lev : IPacket
    {
        public int Level { get; set; }
        
        public void Construct(string[] packet)
        {
            Level = packet[0].ToInt();
        }
    }
}