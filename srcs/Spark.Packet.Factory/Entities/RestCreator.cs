using Spark.Core.Enum;
using Spark.Core.Extension;
using Spark.Packet.Entities;

namespace Spark.Packet.Factory.Entities
{
    public class RestCreator : PacketCreator<Rest>
    {
        public override string Header { get; } = "rest";

        public override Rest Create(string[] content)
        {
            return new Rest
            {
                EntityType = content[0].ToEnum<EntityType>(),
                EntityId = content[1].ToLong(),
                IsResting = content[2].ToBool()
            };
        }
    }
}