using Spark.Core.Enum;

namespace Spark.Game.Entities
{
    public class Npc : LivingEntity
    {
        public Npc(long id, int gameKey) : base(id, EntityType.Npc) => GameKey = gameKey;

        public int GameKey { get; }
    }
}