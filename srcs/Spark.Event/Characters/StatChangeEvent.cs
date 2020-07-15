using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;

namespace Spark.Event.Characters
{
    public class StatChangeEvent : IEvent
    {
        public StatChangeEvent(IClient client, ICharacter character)
        {
            Client = client;
            Character = character;
        }

        public ICharacter Character { get; }
        public IClient Client { get; }
    }
}