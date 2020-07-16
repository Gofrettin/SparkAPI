using Spark.Core;
using Spark.Core.Enum;
using Spark.Game.Abstraction;

namespace Spark.Game
{
    public class Portal : IPortal
    {
        public Vector2D Position { get; }
        public short DestinationId { get; }
        public PortalType PortalType { get; }
        public int Id { get; }

        public Portal(Vector2D position, short destinationId, PortalType portalType, int id)
        {
            Position = position;
            DestinationId = destinationId;
            PortalType = portalType;
            Id = id;
        }
    }
}