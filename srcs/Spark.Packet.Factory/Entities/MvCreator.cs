using Spark.Core;
using Spark.Core.Enum;
using Spark.Core.Extension;
using Spark.Packet.Entities;

namespace Spark.Packet.Factory.Entities
{
    public class MvCreator : PacketCreator<Mv>
    {
        public override string Header { get; } = "mv";
        
        public override Mv Create(string[] content)
        {
            return new Mv
            {
                EntityType = content[0].ToEnum<EntityType>(),
                EntityId = content[1].ToLong(),
                Position = new Vector2D(content[2].ToShort(), content[3].ToShort()),
                Speed = content[4].ToShort()
            };
        }
    }
}