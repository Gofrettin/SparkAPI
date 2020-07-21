using Spark.Core.Enum;
using Spark.Packet.Battle;
using Spark.Core.Extension;

namespace Spark.Packet.Factory.Battle
{
    public class BfCreator : PacketCreator<Bf>
    {
        public override string Header { get; } = "bf";

        public override Bf Create(string[] content)
        {
            string[] buffInfo = content[2].Split('.');
            
            return new Bf
            {
                EntityType = content[0].ToEnum<EntityType>(),
                EntityId = content[1].ToLong(),
                BuffId = buffInfo[1].ToLong(),
                Duration = buffInfo[2].ToLong()
            };
        }
    }
}