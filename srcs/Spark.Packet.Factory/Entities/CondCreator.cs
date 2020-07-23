using Spark.Core.Enum;
using Spark.Core.Extension;
using Spark.Packet.Entities;

namespace Spark.Packet.Factory.Entities
{
    public class CondCreator : PacketCreator<Cond>
    {
        public override string Header { get; } = "cond";
        
        public override Cond Create(string[] content)
        {
            return new Cond
            {
                EntityType = content[0].ToEnum<EntityType>(),
                EntityId = content[1].ToLong(),
                CanAttack = content[2].ToBool().Reverse(),
                CanMove = content[3].ToBool().Reverse(),
                Speed = content[4].ToShort()
            };
        }
    }
}