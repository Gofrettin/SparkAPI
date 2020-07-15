using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;

namespace Spark.Event.Entities
{
    public class SpecialistWearEvent : EntityEvent
    {
        public SpecialistWearEvent(IClient client, IEntity entity, int specialistId) : base(client, entity)
        {
            SpecialistId = specialistId;
        }
        
        public int SpecialistId { get; }
    }
}