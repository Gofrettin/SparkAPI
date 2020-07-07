using Spark.Core.Enum;

namespace Spark.Game.Entities
{
    public class MapObject : Entity
    {
        public MapObject(long id, int gameKey) : base(id, EntityType.MapObject) => GameKey = gameKey;

        public int GameKey { get; }
    }
}