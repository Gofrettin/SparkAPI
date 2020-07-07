using Spark.Packet.Extension;

namespace Spark.Packet.CharacterSelector
{
    [Packet("clist_start")]
    public class CListStart : IPacket
    {
        public int Index { get; set; }

        public void Construct(string[] packet)
        {
            Index = packet[0].ToInt();
        }
    }
}