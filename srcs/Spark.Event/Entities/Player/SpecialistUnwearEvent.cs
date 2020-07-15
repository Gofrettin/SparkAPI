using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;

namespace Spark.Event.Entities.Player
{
    public class SpecialistUnwearEvent : EntityEvent
    {
        public SpecialistUnwearEvent(IClient client, IEntity entity) : base(client, entity)
        {
        }
    }
}