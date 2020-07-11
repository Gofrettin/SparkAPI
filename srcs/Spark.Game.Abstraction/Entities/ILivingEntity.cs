using Spark.Core.Enum;

namespace Spark.Game.Abstraction.Entities
{
    public interface ILivingEntity : IEntity
    {
        int Hp { get; set; }
        int Mp { get; set; }
        
        byte Speed { get; set; }
        Direction Direction { get; set; }

        bool IsAlive => Hp > 0;
    }
}