using System.Linq;
using Spark.Game.Abstraction.Inventory;

namespace Spark.Game.Abstraction.Extension
{
    public static class InventoryExtensions
    {
        public static IObjectStack FindObject(this ICharacterInventory characterInventory, int objectKey)
        {
            return characterInventory.FirstOrDefault(x => x.ObjectKey == objectKey);
        }
    }
}