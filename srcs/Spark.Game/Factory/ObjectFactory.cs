using Spark.Core.Enum;
using Spark.Game.Abstraction.Factory;
using Spark.Game.Abstraction.Inventory;
using Spark.Game.Inventory;

namespace Spark.Game.Factory
{
    public class ObjectFactory : IObjectFactory
    {
        public IObjectStack CreateObjectStack(BagType bagType, int objectKey, int slot, int amount)
        {
            return new ObjectStack(bagType, objectKey, slot)
            {
                Count = amount
            };
        }
    }
}