using Spark.Core;
using Spark.Core.Enum;
using Spark.Core.Extension;
using Spark.Packet.Entities;

namespace Spark.Packet.Factory.Entities
{
    public class GpCreator : PacketCreator<Gp>
    {
        public override string Header { get; } = "gp";
        
        public override Gp Create(string[] content)
        {
            return new Gp
            {
                Position = new Vector2D(content[0].ToInt(), content[1].ToInt()),
                DestinationId = content[2].ToShort(),
                PortalType = content[3].ToEnum<PortalType>(),
                PortalId = content[4].ToInt()
            };
        }
    }
}