using Spark.Core;
using Spark.Core.Enum;
using Spark.Packet.Characters;
using Spark.Packet.Extension;

namespace Spark.Packet.Factory.Characters
{
    public class AtCreator : PacketCreator<At>
    {
        public override string Header { get; } = "at";

        public override At Create(string[] content)
        {
            return new At
            {
                EntityId = content[0].ToLong(),
                MapId = content[1].ToInt(),
                Position = new Vector2D(content[2].ToShort(), content[3].ToShort()),
                Direction = content[4].ToEnum<Direction>()
            };
        }
    }
}