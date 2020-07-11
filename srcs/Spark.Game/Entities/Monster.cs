using Spark.Core;
using Spark.Core.Enum;
using Spark.Database.Data;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;

namespace Spark.Game.Entities
{
    public class Monster : IMonster
    {
        public Monster(long id, int gameKey, MonsterData data)
        {
            Id = id;
            GameKey = gameKey;
            EntityType = EntityType.Monster;

            Name = data.NameKey;
        }

        public long Id { get; }
        public EntityType EntityType { get; }
        public string Name { get; set; }
        public IMap Map { get; set; }
        public Position Position { get; set; }
        public int GameKey { get; set; }
        public int Hp { get; set; }
        public int Mp { get; set; }
        public byte Speed { get; set; }
        public Direction Direction { get; set; }
    }
}