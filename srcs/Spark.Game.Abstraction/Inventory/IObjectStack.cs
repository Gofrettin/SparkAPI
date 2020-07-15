using Spark.Core.Enum;

namespace Spark.Game.Abstraction.Inventory
{
    public interface IObjectStack
    {
        BagType Bag { get; }
        int ObjectKey { get; }
        int Slot { get; }
        int Count { get; set; }
    }
}