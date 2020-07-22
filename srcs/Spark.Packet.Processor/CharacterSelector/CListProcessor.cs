using NLog;
using Spark.Core;
using Spark.Core.Configuration;
using Spark.Game.Abstraction;
using Spark.Packet.CharacterSelector;

namespace Spark.Packet.Processor.CharacterSelector
{
    public class CListProcessor : PacketProcessor<CList>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        protected override void Process(IClient client, CList packet)
        {
            LoginConfiguration configuration = client.GetConfiguration<LoginConfiguration>();
            configuration.SelectableCharacters.Add(new SelectableCharacter
            {
                Name = packet.Name,
                Slot = packet.Slot
            });

            Logger.Debug($"Added {packet.Name} character to selectable characters");
        }
    }
}