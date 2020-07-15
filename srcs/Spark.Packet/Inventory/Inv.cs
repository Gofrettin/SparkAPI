using System.Collections.Generic;
using System.Linq;
using Spark.Core.Enum;
using Spark.Packet.Extension;

namespace Spark.Packet.Inventory
{
    [Packet("inv")]
    public class Inv : IPacket
    {
        public BagType BagType { get; set; }
        public List<ObjectInfo> Objects { get; } = new List<ObjectInfo>();
        
        public void Construct(string[] packet)
        {
            BagType = packet[0].ToEnum<BagType>();

            foreach (string value in packet.Skip(1))
            {
                string[] objectInfos = value.Split('.');

                var objectInfo = new ObjectInfo
                {
                    Slot = objectInfos[0].ToInt(),
                    ObjectKey = objectInfos[1].ToInt()
                };

                if (BagType == BagType.Equipment)
                {
                    short rarity = objectInfos[2].ToShort();
                    short upgrade = objectInfos[3].ToShort();

                    objectInfo.Rare = rarity;
                    objectInfo.Upgrade = upgrade;
                }
                else
                {
                    objectInfo.Amount = objectInfos[2].ToInt();
                }
                
                Objects.Add(objectInfo);
            }
        }
    }
    
    public class ObjectInfo
    {
        public int Slot { get; set; }
        public int ObjectKey { get; set; }
        public int Rare { get; set; }
        public int Upgrade { get; set; }
        public int Amount { get; set; } = 1;
    }
}