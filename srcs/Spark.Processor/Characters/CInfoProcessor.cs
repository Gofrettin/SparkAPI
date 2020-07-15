using NLog;
using Spark.Event;
using Spark.Event.Characters;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Factory;
using Spark.Packet.Characters;

namespace Spark.Processor.Characters
{
    public class CInfoProcessor : PacketProcessor<CInfo>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IEntityFactory _entityFactory;

        private readonly IEventPipeline _eventPipeline;

        public CInfoProcessor(IEventPipeline eventPipeline, IEntityFactory entityFactory)
        {
            _eventPipeline = eventPipeline;
            _entityFactory = entityFactory;
        }

        protected override void Process(IClient client, CInfo packet)
        {
            if (client.Character != null)
            {
                return;
            }

            client.Character = _entityFactory.CreateCharacter(packet.Id, packet.Name, client);

            _eventPipeline.Emit(new CharacterInitializedEvent(client, client.Character));

            Logger.Info($"Client with id {client.Id} initialized with character {client.Character.Name}");
        }
    }
}