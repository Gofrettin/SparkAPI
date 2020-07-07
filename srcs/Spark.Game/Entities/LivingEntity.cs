using Spark.Core.Enum;

namespace Spark.Game.Entities
{
    public class LivingEntity : Entity
    {
        public LivingEntity(long id, EntityType entityType) : base(id, entityType)
        {
            Hp = 0;
            Mp = 0;
        }

        public int Hp { get; set; }
        public int Mp { get; set; }

        public bool IsAlive => Hp > 0;
    }
}