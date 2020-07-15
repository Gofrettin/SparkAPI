using NFluent;
using Spark.Core.Enum;
using Spark.Game.Abstraction.Entities;
using Spark.Packet.Inventory;
using Spark.Tests.Attributes;

namespace Spark.Tests.Processor
{
    public class InventoryProcessorTests : ProcessorTests
    {
        [ProcessorTest(typeof(Inv))]
        public void Inv_Main_Test()
        {
            using (GameContext context = CreateContext())
            {
                ICharacter character = context.Character;

                context.Process(new Inv()
                {
                    BagType = BagType.Main,
                    Objects =
                    {
                        new ObjectInfo
                        {
                            Slot = 1,
                            ObjectKey = 140,
                            Amount = 10
                        },
                        new ObjectInfo
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
                    Objects =
                    {
                        new ObjectInfo
                        {
                            Slot = 1,
                            ObjectKey = 140,
                            Amount = 10
                        },
                        new ObjectInfo
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