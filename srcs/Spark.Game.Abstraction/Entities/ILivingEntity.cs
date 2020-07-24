using System.Collections.Generic;
using Spark.Core.Enum;
using Spark.Game.Abstraction.Battle;

namespace Spark.Game.Abstraction.Entities
{
    public interface ILivingEntity : IEntity
    {
        int HpPercentage { get; set; }
        int MpPercentage { get; set; }
        short MorphId { get; set; }
        int Level { get; set; }
        bool IsResting { get; set; }

        List<IBuff> Buffs { get; }
        
        short Speed { get; set; }
        Direction Direction { get; set; }
    }
}