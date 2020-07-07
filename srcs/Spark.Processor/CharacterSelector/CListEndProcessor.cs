using System.Collections.Generic;
using Spark.Core;
using Spark.Event;
using Spark.Event.CharacterSelector;
using Spark.Game;
using Spark.Network.Client.Impl;
using Spark.Packet.CharacterSelector;

namespace Spark.Processor.CharacterSelector
{
    public class CListEndProcessor : PacketProcessor<CListEnd>
    {
        private readonly IEventPipeline _eventPipeline;

        public CListEndProcessor(IEventPipeline eventPipeline) => _eventPipeline = eventPipeline;

        protected override void Process(IClient client, CListEnd packet)
        {
            IEnumerable<SelectableCharacter> characters = (client as RemoteClient)?.SelectableCharacters;

            _eventPipeline.Emit(new CharacterSelectReadyEvent(client, characters));
        }
    }
}