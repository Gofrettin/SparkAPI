using System;
using System.Collections.Generic;

namespace Spark.Packet.Characters
{
    [Packet("ski")]
    public class Ski : IPacket
    {
        public HashSet<int> Skills { get; } = new HashSet<int>();
        
        public void Construct(string[] packet)
        {
            foreach (string value in packet)
            {
                string[] skillId = value.Split('|');
                if (skillId.Length > 0)
                {
                    Skills.Add(Convert.ToInt32(skillId[0]));
                    continue;
                }
                
                Skills.Add(Convert.ToInt32(value));
            }
        }
    }
}