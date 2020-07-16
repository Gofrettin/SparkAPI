using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Factory;
using Spark.Packet.Entities;

namespace Spark.Processor.Entities
{
    public class GpProcessor : PacketProcessor<Gp>
    {
        private readonly IPortalFactory _portalFactory;

        public GpProcessor(IPortalFactory portalFactory)
        {
            _portalFactory = portalFactory;
        }
        
        protected override void Process(IClient client, Gp packet)
        {
            IMap map = client.Character.Map;
            if (map == null)
            {
                return;
            }

            IPortal portal = _portalFactory.CreatePortal(packet.PortalId, packet.Position, packet.DestinationId, packet.PortalType);
            if (portal == null)
            {
                return;
            }
            
            map.AddPortal(portal);
        }
    }
}