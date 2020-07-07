
using System.Linq;
using System.Threading.Tasks;
using NLog;
using Spark.Core;
using Spark.Core.Storage;
using Spark.Game.Abstraction;
using Spark.Packet.CharacterSelector;

namespace Spark.Processor.CharacterSelector
{
    public class CListEndProcessor : PacketProcessor<CListEnd>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        protected override Task Process(IClient client, CListEnd packet)
        {
            LoginStorage storage = client.GetStorage<LoginStorage>();
            SelectableCharacter character = storage.SelectableCharacters.FirstOrDefault(x => storage.CharacterSelector.Invoke(x));

            if (character == null)
            {
                Logger.Error("Can't found character matching predicate");
                return Task.CompletedTask;
            }
            
            client.SendPacket($"select {character.Slot}");
            Logger.Info($"Character {character.Name} selected");
            return Task.CompletedTask;
        }
    }
}