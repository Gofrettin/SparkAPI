using Spark.Core;
using Spark.Core.Enum;

namespace Spark.Game.Abstraction.Entities
{
    public interface ICharacter : IPlayer
    {
        IClient Client { get; }

        void Move(Vector2D vector2D);
        void Turn(Direction direction);
    }
}