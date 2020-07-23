using System;
using System.Collections.Generic;

namespace Spark.Packet.Characters
{
    public class Ski : IPacket
    {
        public HashSet<int> Skills { get; set; }
    }
}