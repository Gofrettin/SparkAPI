using Spark.Game.Abstraction.Entities;

namespace Spark.Event.Characters
{
    public class CharacterInitializedEvent : IEvent
    {
        public CharacterInitializedEvent(ICharacter character) => Character = character;

        public ICharacter Character { get; }
    }
}