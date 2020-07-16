using Spark.Core;
using Spark.Core.Enum;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Factory;

namespace Spark.Game.Factory
{
    public class PortalFactory : IPortalFactory
    {
        public IPortal CreatePortal(int id, Vector2D position, short destinationId, PortalType portalType)
        {
            return new Portal(position, destinationId, portalType, id);
        }
    }
}