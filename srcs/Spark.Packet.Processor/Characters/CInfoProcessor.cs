using NLog;
using Spark.Event;
using Spark.Event.Characters;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Factory;
using Spark.Packet.Characters;

namespace Spark.Packet.Processor.Characters
{
    public class CInfoProcessor : PacketProcessor<CInfo>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IEntityFactory entityFactory;

        private readonly IEventPipeline eventPipeline;

        public CInfoProcessor(IEventPipeline eventPipeline, IEntityFactory entityFactory)
        {
            this.eventPipeline = eventPipeline;
            this.entityFactory = entityFactory;
        }

        protected override void Process(IClient client, CInfo packet)
        {
            if (client.Character != null)
            {
                return;
            }

            client.Character = entityFactory.CreateCharacter(packet.Id, packet.Name, client);

            eventPipeline.Emit(new CharacterInitializedEvent(client, client.Character));

            Logger.Debug($"Client with id {client.Id} initialized with character {client.Character.Name}");
        }
    }
}