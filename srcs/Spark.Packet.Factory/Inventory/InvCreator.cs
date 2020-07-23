using System.Collections.Generic;
using System.Linq;
using Spark.Core.Enum;
using Spark.Core.Extension;
using Spark.Packet.Inventory;

namespace Spark.Packet.Factory.Inventory
{
    public class InvCreator : PacketCreator<Inv>
    {
        public override string Header { get; } = "inv";
        
        public override Inv Create(string[] content)
        {
            BagType bagType = content[0].ToEnum<BagType>();
            var objects = new List<Inv.ObjectInfo>();

            foreach (string value in content.Skip(1))
            {
                string[] objectInfos = value.Split('.');

                var objectInfo = new Inv.ObjectInfo
                {
                    Slot = objectInfos[0].ToInt(),
                    ObjectKey = objectInfos[1].ToInt()
                };

                if (bagType == BagType.Equipment)
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
                
                objects.Add(objectInfo);
            }

            return new Inv
            {
                BagType = bagType,
                Objects = objects
            };
        }
    }
}