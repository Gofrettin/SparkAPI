using Spark.Core.Enum;
using Spark.Game.Abstraction.Inventory;

namespace Spark.Game.Inventory
{
    public class ObjectStack : IObjectStack
    {
        public ObjectStack(BagType bag, int objectKey, int slot)
        {
            Bag = bag;
            ObjectKey = objectKey;
            Slot = slot;
        }

        public BagType Bag { get; }
        public int ObjectKey { get; }
        public int Slot { get; }
        public int Count { get; set; }
    }
}