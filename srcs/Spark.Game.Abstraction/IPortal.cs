using Spark.Core;
using Spark.Core.Enum;

namespace Spark.Game.Abstraction
{
    public interface IPortal
    {
        Vector2D Position { get; }
        short DestinationId { get; }
        PortalType PortalType { get; }
        int Id { get; }
    }
}