using System.Collections.Generic;
using NLog;
using Spark.Core.Enum;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Factory;
using Spark.Packet.Characters;

namespace Spark.Packet.Processor.Characters
{
    public class SkiProcessor : PacketProcessor<Ski>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly ISkillFactory _skillFactory;

        public SkiProcessor(ISkillFactory skillFactory) => _skillFactory = skillFactory;

        protected override void Process(IClient client, Ski packet)
        {
            var skills = new List<ISkill>();

            foreach (int skillGameId in packet.Skills)
            {
                ISkill skill = _skillFactory.CreateSkill(skillGameId);
                if (skill.Category == SkillCategory.Player)
                {
                    skills.Add(skill);
                }
            }

            client.Character.Skills = skills;
            Logger.Debug($"{skills.Count} skills loaded for {client.Character.Name}");
        }
    }
}