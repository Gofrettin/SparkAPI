using System.Linq;
using System.Threading.Tasks;
using NLog;
using Spark.Core;
using Spark.Game.Abstraction;
using Spark.Network.Option;
using Spark.Packet.CharacterSelector;

namespace Spark.Processor.CharacterSelector
{
    public class CListEndProcessor : PacketProcessor<CListEnd>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        protected override void Process(IClient client, CListEnd packet)
        {
            LoginOption option = client.GetOption<LoginOption>();
            SelectableCharacter character = option.SelectableCharacters.FirstOrDefault(x => option.CharacterSelector.Invoke(x));

            if (character == null)
            {
                Logger.Error("Can't found character matching predicate");
                return;
            }

            client.SendPacket($"select {character.Slot}");
            Logger.Info($"Character {character.Name} selected");
        }
    }
}