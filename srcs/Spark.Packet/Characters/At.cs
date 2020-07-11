using Spark.Core;
using Spark.Core.Enum;
using Spark.Packet.Extension;

namespace Spark.Packet.Characters
{
    [Packet("at")]
    public class At : IPacket
    {
        public int MapId { get; set; }
        public Position Position { get; set; }
        public Direction Direction { get; set; }
        
        public void Construct(string[] packet)
        {
            MapId = packet[1].ToInt();
            Position = new Position(packet[2].ToShort(), packet[3].ToShort());
            Direction = packet[4].ToEnum<Direction>();
        }
    }
}