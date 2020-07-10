
using System.Linq;
using System.Threading.Tasks;
using NLog;
using Spark.Core;
using Spark.Network.Option;
using Spark.Game.Abstraction;
using Spark.Packet.CharacterSelector;

namespace Spark.Processor.CharacterSelector
{
    public class CListEndProcessor : PacketProcessor<CListEnd>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        protected override Task Process(IClient client, CListEnd packet)
        {
            LoginOption option = client.GetStorage<LoginOption>();
            SelectableCharacter character = option.SelectableCharacters.FirstOrDefault(x => option.CharacterSelector.Invoke(x));

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