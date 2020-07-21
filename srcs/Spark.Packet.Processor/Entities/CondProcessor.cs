using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;
using Spark.Packet.Entities;

namespace Spark.Packet.Processor.Entities
{
    public class CondProcessor : PacketProcessor<Cond>
    {
        protected override void Process(IClient client, Cond packet)
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

            entity.Speed = packet.Speed;
        }
    }
}