using System.Collections.Generic;
using Spark.Core;
using Spark.Game;
using Spark.Network.Client.Impl;
using Spark.Packet.CharacterSelector;

namespace Spark.Processor.CharacterSelector
{
    public class CListProcessor : PacketProcessor<CList>
    {
        protected override void Process(IClient client, CList packet)
        {
            List<SelectableCharacter> characters = (client as RemoteClient)?.SelectableCharacters;
            if (characters == null)
            {
                return;
            }

            characters.Add(new SelectableCharacter
            {
                Name = packet.Name,
                Slot = packet.Slot
            });
        }
    }
}