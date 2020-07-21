using System.Linq;
using NLog;
using Spark.Game.Abstraction;
using Spark.Packet.Battle;

namespace Spark.Packet.Processor.Battle
{
    public class SrProcessor : PacketProcessor<Sr>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        protected override void Process(IClient client, Sr packet)
        {
            ISkill skill = client.Character.Skills.FirstOrDefault(x => x.CastId == packet.CastId);
            if (skill == null)
            {
                Logger.Warn($"Can't found skill with cast id {packet.CastId}");
                return;
            }

            skill.IsOnCooldown = false;
        }
    }
}