using Spark.Core.Enum;

namespace Spark.Game.Entities
{
    public class Player : LivingEntity
    {
        public Player(long id) : base(id, EntityType.Player)
        {
        }
    }
}