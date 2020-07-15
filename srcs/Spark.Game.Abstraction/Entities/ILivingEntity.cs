using Spark.Core.Enum;

namespace Spark.Game.Abstraction.Entities
{
    public interface ILivingEntity : IEntity
    {
        int HpPercentage { get; set; }
        int MpPercentage { get; set; }
        short MorphId { get; set; }
        
        byte Speed { get; set; }
        Direction Direction { get; set; }
    }
}