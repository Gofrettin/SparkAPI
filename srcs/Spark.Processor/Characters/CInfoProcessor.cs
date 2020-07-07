using NLog;
using Spark.Event;
using Spark.Event.Characters;
using Spark.Game;
using Spark.Game.Entities;
using Spark.Packet.Characters;

namespace Spark.Processor.Characters
{
    public class CInfoProcessor : PacketProcessor<CInfo>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IEventPipeline _eventPipeline;

        public CInfoProcessor(IEventPipeline eventPipeline) => _eventPipeline = eventPipeline;

        protected override void Process(IClient client, CInfo packet)
        {
            if (client.Character != null)
            {
                return;
            }

            client.Character = new Character(packet.Id, client)
            {
                Name = packet.Name
            };

            _eventPipeline.Emit(new CharacterInitializedEvent(client.Character));

            Logger.Info($"Client with id {client.Id} initialized with character {client.Character.Name}");
        }
    }
}