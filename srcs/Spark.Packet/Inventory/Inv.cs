using System.Collections.Generic;
using System.Linq;
using Spark.Core.Enum;
using Spark.Core.Extension;

namespace Spark.Packet.Inventory
{
    public class Inv : IPacket
    {
        public BagType BagType { get; set; }
        public IEnumerable<ObjectInfo> Objects { get; set; }
        
        public class ObjectInfo
        {
            public int Slot { get; set; }
            public int ObjectKey { get; set; }
            public int Rare { get; set; }
            public int Upgrade { get; set; }
            public int Amount { get; set; } = 1;
        }
    }
}