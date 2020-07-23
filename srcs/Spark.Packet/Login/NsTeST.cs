using System.Collections.Generic;
using Spark.Core.Server;

namespace Spark.Packet.Login
{
    public class NsTeST : IPacket
    {
        public string Name { get; set; }
        public int RegionId { get; set; }
        public int EncryptionKey { get; set; }
        public IEnumerable<WorldServer> Servers { get; set; }
    }
}