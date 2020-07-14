using NLog;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;
using Spark.Packet.Characters;

namespace Spark.Processor.Characters
{
    public class StatProcessor : PacketProcessor<Stat>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        protected override void Process(IClient client, Stat packet)
        {
            ICharacter character = client.Character;

            character.Hp = packet.Hp;
            character.Mp = packet.Mp;
            character.MaxHp = packet.MaxHp;
            character.MaxMp = packet.MaxMp;

            character.HpPercentage = (byte)(character.Hp == 0 ? 0 : (double)character.Hp / character.MaxHp * 100);
            character.MpPercentage = (byte)(character.Mp == 0 ? 0 : (double)character.Mp / character.MaxMp * 100);
            
            Logger.Debug($"{client.Character.Name} Hp/Mp updated (HP {character.Hp}/{character.MaxHp} - MP {character.Mp}/{character.MaxMp})");
        }
    }
}