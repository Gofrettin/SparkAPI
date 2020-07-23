using System.Collections.Generic;
using System.Linq;
using Spark.Core.Enum;

namespace Spark.Game.Abstraction.Inventory
{
    public interface ICharacterInventory : IEnumerable<IObjectStack>
    {
        int Gold { get; set; }
        IEnumerable<IObjectStack> GetObjects(BagType bagType);
        void AddObject(IObjectStack objectStack);
        IObjectStack FindObject(int objectKey);
    }
}