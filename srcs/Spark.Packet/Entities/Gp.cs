using Spark.Core;
using Spark.Core.Enum;
using Spark.Packet.Extension;

namespace Spark.Packet.Entities
{
    [Packet("gp")]
    public class Gp : IPacket
    {
        public Vector2D Position { get; set; }
        public short DestinationId { get; set; }
        public PortalType PortalType { get; set; }
        public int PortalId { get; set; }
        
        public void Construct(string[] packet)
        {
            Position = new Vector2D(packet[0].ToInt(), packet[1].ToInt());
            DestinationId = packet[2].ToShort();
            PortalType = packet[3].ToEnum<PortalType>();
            PortalId = packet[4].ToInt();
        }
    }
}