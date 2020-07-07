using Spark.Core.Enum;

namespace Spark.Game.Entities
{
    public class Monster : LivingEntity
    {
        public Monster(long id, int gameKey) : base(id, EntityType.Monster) => GameKey = gameKey;

        public int GameKey { get; }
    }
}