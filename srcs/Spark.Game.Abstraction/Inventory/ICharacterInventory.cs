using System.Collections.Generic;
using Spark.Core.Enum;

namespace Spark.Game.Abstraction.Inventory
{
    public interface ICharacterInventory : IEnumerable<IObjectStack>
    {
        int Gold { get; set; }
        IEnumerable<IObjectStack> GetObjects(BagType bagType);
        void AddObject(IObjectStack objectStack);
    }
}