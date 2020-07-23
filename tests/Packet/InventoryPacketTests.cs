using System.Collections.Generic;
using Spark.Core.Enum;
using Spark.Packet.Inventory;
using Spark.Tests.Attributes;

namespace Spark.Tests.Packet
{
    public class InventoryPacketTests : PacketTests
    {
        [PacketTest(typeof(Inv))]
        public void Inv_Main_Test()
        {
            CreateAndCheckValues("inv 1 0.1243.13 1.1004.14 3.9068.1", new Inv
            {
                BagType = BagType.Main,
                Objects = new List<Inv.ObjectInfo>()
                {
                    new Inv.ObjectInfo
                    {
                        Slot = 0,
                        ObjectKey = 1243,
                        Amount = 13
                    },
                    new Inv.ObjectInfo
                    {
                        Slot = 1,
                        ObjectKey = 1004,
                        Amount = 14
                    },
                    new Inv.ObjectInfo
                    {
                        Slot = 3,
                        ObjectKey = 9068,
                        Amount = 1
                    }
                }
            });
        }
        
        [PacketTest(typeof(Inv))]
        public void Inv_Equipment_Test()
        {
            CreateAndCheckValues("inv 0 0.800.0.0.0.0 1.4131.0.0.0.0 2.8037.0.0.0.0", new Inv
            {
                BagType = BagType.Equipment,
                Objects = new List<Inv.ObjectInfo>()
                {
                    new Inv.ObjectInfo
                    {
                        Slot = 0,
                        ObjectKey = 800,
                        Amount = 1
                    },
                    new Inv.ObjectInfo
                    {
                        Slot = 1,
                        ObjectKey = 4131,
                        Amount = 1
                    },
                    new Inv.ObjectInfo
                    {
                        Slot = 2,
                        ObjectKey = 8037,
                        Amount = 1
                    }
                }
            });
        }
    }
}