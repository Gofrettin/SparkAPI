using Spark.Core;
using Spark.Core.Enum;

namespace Spark.Game.Entities
{
    public abstract class Entity
    {
        protected Entity(long id, EntityType entityType)
        {
            Id = id;
            EntityType = entityType;
            Name = string.Empty;
            Position = new Position();
        }

        public long Id { get; }
        public EntityType EntityType { get; }
        public string Name { get; set; }
        public Map Map { get; set; }
        public Position Position { get; set; }
    }
}