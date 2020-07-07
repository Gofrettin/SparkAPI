using System.Collections.Generic;
using Spark.Core;
using Spark.Game;

namespace Spark.Event.CharacterSelector
{
    public class CharacterSelectReadyEvent : IEvent
    {
        public CharacterSelectReadyEvent(IClient client, IEnumerable<SelectableCharacter> characters)
        {
            Client = client;
            Characters = characters;
        }

        public IClient Client { get; }
        public IEnumerable<SelectableCharacter> Characters { get; }
    }
}