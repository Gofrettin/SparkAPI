using Spark.Core.Enum;
using Spark.Core.Extension;
using Spark.Packet.Entities;

namespace Spark.Packet.Factory.Entities
{
    public class DirCreator : PacketCreator<Dir>
    {
        public override string Header { get; } = "dir";
        
        public override Dir Create(string[] content)
        {
            return new Dir
            {
                EntityType = content[0].ToEnum<EntityType>(),
                EntityId = content[1].ToLong(),
                Direction = content[2].ToEnum<Direction>()
            };
        }
    }
}