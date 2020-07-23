using System.Collections.Generic;
using Spark.Packet.Characters;
using Spark.Core.Extension;

namespace Spark.Packet.Factory.Characters
{
    public class SkiCreator : PacketCreator<Ski>
    {
        public override string Header { get; } = "ski";
        
        public override Ski Create(string[] content)
        {
            var skills = new HashSet<int>();
            foreach (string value in content)
            {
                string[] skillId = value.Split('|');
                if (skillId.Length > 0)
                {
                    skills.Add(skillId[0].ToInt());
                    continue;
                }

                skills.Add(value.ToInt());
            }

            return new Ski
            {
                Skills = skills
            };
        }
    }
}