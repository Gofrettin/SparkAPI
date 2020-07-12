using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;
using Spark.Packet.Entities;

namespace Spark.Processor.Entities
{
    public class DirProcessor : PacketProcessor<Dir>
    {
        protected override void Process(IClient client, Dir packet)
        {
            IMap map = client.Character.Map;
            if (map == null)
            {
                return;
            }

            ILivingEntity entity = map.GetEntity<ILivingEntity>(packet.EntityType, packet.EntityId);
            if (entity == null)
            {
                return;
            }

            entity.Direction = packet.Direction;
        }
    }
}