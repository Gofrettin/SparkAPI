using Spark.Core.Enum;
using Spark.Packet.Battle;
using Spark.Packet.Extension;

namespace Spark.Packet.Factory.Battle
{
    public class DieCreator : PacketCreator<Die>
    {
        public override string Header { get; } = "die";

        public override Die Create(string[] content)
        {
            return new Die
            {
                EntityType = content[0].ToEnum<EntityType>(),
                EntityId = content[1].ToLong()
            };
        }
    }
}