using System.Collections.Generic;
using Spark.Core.Server;

namespace Spark.Packet.Login
{
    public class NsTeSt : IPacket
    {
        public string Name { get; set; }
        public int RegionId { get; set; }
        public int EncryptionKey { get; set; }
        public IEnumerable<WorldServer> Servers { get; set; }
    }
}