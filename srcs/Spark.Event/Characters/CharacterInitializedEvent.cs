using Spark.Game.Entities;

namespace Spark.Event.Characters
{
    public class CharacterInitializedEvent : IEvent
    {
        public CharacterInitializedEvent(Character character) => Character = character;

        public Character Character { get; }
    }
}