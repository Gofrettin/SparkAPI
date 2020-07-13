using Spark.Core.Enum;

namespace Spark.Game.Abstraction.Entities
{
    public interface IPlayer : ILivingEntity
    {
        Class Class { get; set; }
        Gender Gender { get; set; }
    }
}