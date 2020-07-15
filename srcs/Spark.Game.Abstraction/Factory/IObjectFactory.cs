using Spark.Core.Enum;
using Spark.Game.Abstraction.Inventory;

namespace Spark.Game.Abstraction.Factory
{
    public interface IObjectFactory
    {
        IObjectStack CreateObjectStack(BagType bagType, int objectKey, int slot, int amount);
    }
}