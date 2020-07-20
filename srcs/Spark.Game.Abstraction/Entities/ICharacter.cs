using System.Collections.Generic;
using System.Threading.Tasks;
using Spark.Core;
using Spark.Game.Abstraction.Inventory;

namespace Spark.Game.Abstraction.Entities
{
    public interface ICharacter : IPlayer
    {
        IClient Client { get; }
        ICharacterInventory Inventory { get; }
        IEnumerable<ISkill> Skills { get; set; }

        int Hp { get; set; }
        int Mp { get; set; }
        int MaxHp { get; set; }
        int MaxMp { get; set; }
    }
}