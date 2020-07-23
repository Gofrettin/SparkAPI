using Spark.Core;
using Spark.Core.Enum;
using Spark.Core.Extension;

namespace Spark.Packet.Entities
{
    public class Gp : IPacket
    {
        public Vector2D Position { get; set; }
        public short DestinationId { get; set; }
        public PortalType PortalType { get; set; }
        public int PortalId { get; set; }
    }
}