using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Spark.Core.Enum;
using Spark.Game.Abstraction.Inventory;

namespace Spark.Game.Inventory
{
    public class CharacterInventory : ICharacterInventory
    {
        public CharacterInventory() => Objects = new Dictionary<BagType, List<IObjectStack>>();

        public Dictionary<BagType, List<IObjectStack>> Objects { get; }

        public int Gold { get; set; }
        public IEnumerable<IObjectStack> GetObjects(BagType bagType) => Objects.GetValueOrDefault(bagType, new List<IObjectStack>());

        public void AddObject(IObjectStack objectStack)
        {
            List<IObjectStack> objects = Objects.GetValueOrDefault(objectStack.Bag);
            if (objects == null)
            {
                objects = new List<IObjectStack>();
                Objects[objectStack.Bag] = objects;
            }

            objects.Add(objectStack);
        }

        public IEnumerator<IObjectStack> GetEnumerator() => Objects.Values.SelectMany(x => x).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}