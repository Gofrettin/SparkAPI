using Spark.Core.Enum;
using Spark.Core.Extension;
using Spark.Packet.Entities;

namespace Spark.Packet.Factory.Entities
{
    public class OutCreator : PacketCreator<Out>
    {
        public override string Header { get; } = "out";
        
        public override Out Create(string[] content)
        {
            return new Out
            {
                EntityType = content[0].ToEnum<EntityType>(),
                EntityId = content[1].ToLong()
            };
        }
    }
}