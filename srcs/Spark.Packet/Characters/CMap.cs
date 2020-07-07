using Spark.Packet.Extension;

namespace Spark.Packet.Characters
{
    [Packet("c_map")]
    public class CMap : IPacket
    {
        public int MapId { get; set; }
        public bool IsSourceMap { get; set; }

        public void Construct(string[] packet)
        {
            MapId = packet[1].ToInt();
            IsSourceMap = packet[2].ToBool();
        }
    }
}