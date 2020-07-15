using Spark.Core;
using Spark.Core.Enum;
using Spark.Packet.Extension;

namespace Spark.Packet.Characters
{
    [Packet("at")]
    public class At : IPacket
    {
        public long EntityId { get; set; }
        public int MapId { get; set; }
        public Vector2D Position { get; set; }
        public Direction Direction { get; set; }

        public void Construct(string[] packet)
        {
            EntityId = packet[0].ToLong();
            MapId = packet[1].ToInt();
            Position = new Vector2D(packet[2].ToShort(), packet[3].ToShort());
            Direction = packet[4].ToEnum<Direction>();
        }
    }
}