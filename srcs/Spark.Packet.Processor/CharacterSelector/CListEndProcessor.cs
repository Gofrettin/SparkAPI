using System.Linq;
using NLog;
using Spark.Core;
using Spark.Core.Configuration;
using Spark.Game.Abstraction;
using Spark.Packet.CharacterSelector;

namespace Spark.Packet.Processor.CharacterSelector
{
    public class CListEndProcessor : PacketProcessor<CListEnd>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        protected override void Process(IClient client, CListEnd packet)
        {
            LoginConfiguration option = client.GetConfiguration<LoginConfiguration>();
            if (option == null)
            {
                return;
            }
            
            SelectableCharacter character = option.SelectableCharacters.FirstOrDefault(x => option.CharacterSelector.Invoke(x));
            if (character == null)
            {
                Logger.Error("Can't found character matching predicate");
                return;
            }

            client.SendPacket($"select {character.Slot}");
            Logger.Debug($"Character {character.Name} selected");
        }
    }
}