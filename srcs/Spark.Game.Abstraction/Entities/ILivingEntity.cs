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

        List<IBuff> Buffs { get; }
        
        byte Speed { get; set; }
        Direction Direction { get; set; }
    }
}