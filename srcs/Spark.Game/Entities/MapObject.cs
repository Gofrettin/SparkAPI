using Spark.Core;
using Spark.Core.Enum;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;

namespace Spark.Game.Entities
{
    public class MapObject : IMapObject
    {
        public MapObject(long id, int gameKey)
        {
            Id = id;
            GameKey = gameKey;
            EntityType = EntityType.MapObject;
        }

        public long Id { get; }
        public EntityType EntityType { get; }
        public string Name { get; set; }
        public IMap Map { get; set; }
        public Position Position { get; set; }
        public int GameKey { get; set; }
    }
}