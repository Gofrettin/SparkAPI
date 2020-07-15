using Spark.Core;
using Spark.Core.Enum;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;

namespace Spark.Game.Entities
{
    public class MapObject : IMapObject
    {
        public MapObject(long id, int gameKey, int amount)
        {
            Id = id;
            ItemKey = gameKey;
            EntityType = EntityType.MapObject;
            Amount = amount;
        }

        public long Id { get; }
        public EntityType EntityType { get; }
        public string Name { get; set; }
        public IMap Map { get; set; }
        public Vector2D Position { get; set; }
        public int ItemKey { get; set; }
        public int Amount { get; }

        public bool Equals(IEntity other) => other != null && other.EntityType == EntityType && other.Id == Id;
    }
}