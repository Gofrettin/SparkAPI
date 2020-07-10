using System.Threading.Tasks;
using NLog;
using Spark.Core;
using Spark.Game.Abstraction;
using Spark.Network.Option;
using Spark.Packet.CharacterSelector;

namespace Spark.Processor.CharacterSelector
{
    public class CListProcessor : PacketProcessor<CList>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        protected override Task Process(IClient client, CList packet)
        {
            LoginOption option = client.GetStorage<LoginOption>();
            option.SelectableCharacters.Add(new SelectableCharacter
            {
                Name = packet.Name,
                Slot = packet.Slot
            });

            Logger.Debug($"Added {packet.Name} character to selectable characters");

            return Task.CompletedTask;
        }
    }
}