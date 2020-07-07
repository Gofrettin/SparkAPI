using Spark.Packet.Extension;

namespace Spark.Packet.CharacterSelector
{
    [Packet("clist")]
    public class CList : IPacket
    {
        public int Slot { get; set; }
        public string Name { get; set; }

        public void Construct(string[] packet)
        {
            Slot = packet[0].ToInt();
            Name = packet[1];
        }
    }
}