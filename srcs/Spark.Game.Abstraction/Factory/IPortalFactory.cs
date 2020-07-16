using Spark.Core;
using Spark.Core.Enum;

namespace Spark.Game.Abstraction.Factory
{
    public interface IPortalFactory
    {
        IPortal CreatePortal(int id, Vector2D position, short destinationId, PortalType portalType);
    }
}