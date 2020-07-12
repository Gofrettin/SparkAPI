using System.Linq;
using Spark.Game.Abstraction;
using Spark.Packet.Battle;

namespace Spark.Processor.Battle
{
    public class SrProcessor : PacketProcessor<Sr>
    {
        protected override void Process(IClient client, Sr packet)
        {
            ISkill skill = client.Character.Skills.FirstOrDefault(x => x.CastId == packet.CastId);
            if (skill == null)
            {
                return;
            }

            skill.IsOnCooldown = false;
        }
    }
}