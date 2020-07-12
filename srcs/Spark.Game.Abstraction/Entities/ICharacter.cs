using System.Collections.Generic;
using System.Threading.Tasks;
using Spark.Core;
using Spark.Core.Enum;

namespace Spark.Game.Abstraction.Entities
{
    public interface ICharacter : IPlayer
    {
        IClient Client { get; }
        
        IEnumerable<ISkill> Skills { get; set; }

        Task Attack(ISkill skill);
        Task Attack(ISkill skill, ILivingEntity entity);
        Task Attack(ILivingEntity entity);
        
        Task Walk(Vector2D vector2D);
        Task WalkInRange(Vector2D position, int range);
        
        void Turn(Direction direction);
    }
}