using System.Collections.Generic;
using NFluent;
using Spark.Core.Enum;
using Spark.Event.Inventory;
using Spark.Game.Abstraction.Entities;
using Spark.Packet.Inventory;
using Spark.Tests.Attributes;

namespace Spark.Tests.Processor
{
    public class InventoryProcessorTests : ProcessorTests
    {
        [ProcessorTest(typeof(Gold))]
        [EventTest(typeof(GoldChangeEvent))]
        public void Gold_Test()
        {
            using (GameContext context = CreateContext())
            {
                ICharacter character = context.Character;
                
                context.Process(new Gold
                {
                    Classic = 123456,
                    Bank = 123
                });

                Check.That(character.Inventory.Gold).IsEqualTo(123456);
                
                context.Verify<GoldChangeEvent>(x => x.Gold == 123456);
            }
        }
        
        [ProcessorTest(typeof(Inv))]
        public void Inv_Main_Test()
        {
            using (GameContext context = CreateContext())
            {
                ICharacter character = context.Character;

                context.Process(new Inv
                {
                    BagType = BagType.Main,
                    Objects = new List<Inv.ObjectInfo>()
                    {
                        new Inv.ObjectInfo
                        {
                            Slot = 1,
                            ObjectKey = 140,
                            Amount = 10
                        },
                        new Inv.ObjectInfo
                        {
                            Slot = 8,
                            ObjectKey = 220,
                            Amount = 1
                        },
                    }
                });

                Check.That(character.Inventory).HasElementThatMatches(x => x.Slot == 1 && x.Bag == BagType.Main && x.ObjectKey == 140 && x.Count == 10);
                Check.That(character.Inventory).HasElementThatMatches(x => x.Slot == 8 && x.Bag == BagType.Main && x.ObjectKey == 220 && x.Count == 1);
            }
        }
        
        [ProcessorTest(typeof(Inv))]
        public void Inv_Equipment_Test()
        {
            using (GameContext context = CreateContext())
            {
                ICharacter character = context.Character;

                context.Process(new Inv
                {
                    BagType = BagType.Equipment,
                    Objects = new List<Inv.ObjectInfo>()
                    {
                        new Inv.ObjectInfo
                        {
                            Slot = 1,
                            ObjectKey = 140,
                            Amount = 10
                        },
                        new Inv.ObjectInfo
                        {
                            Slot = 8,
                            ObjectKey = 220,
                            Amount = 1
                        },
                    }
                });

                Check.That(character.Inventory).HasElementThatMatches(x => x.Slot == 1 && x.Bag == BagType.Equipment && x.ObjectKey == 140 && x.Count == 10);
                Check.That(character.Inventory).HasElementThatMatches(x => x.Slot == 8 && x.Bag == BagType.Equipment && x.ObjectKey == 220 && x.Count == 1);
            }
        }
    }
}