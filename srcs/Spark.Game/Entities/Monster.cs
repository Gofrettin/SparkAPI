using System.Collections.Generic;
using Spark.Core;
using Spark.Core.Enum;
using Spark.Database.Data;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Battle;
using Spark.Game.Abstraction.Entities;

namespace Spark.Game.Entities
{
    public class Monster : IMonster
    {
        public Monster(long id, int gameKey, MonsterData data)
        {
            Id = id;
            MonsterKey = gameKey;
            EntityType = EntityType.Monster;

            Name = data.NameKey;
            Buffs = new List<IBuff>();
        }

        public long Id { get; }
        public EntityType EntityType { get; }
        public string Name { get; set; }
        public IMap Map { get; set; }
        public Vector2D Position { get; set; }
        public int MonsterKey { get; set; }
        public int HpPercentage { get; set; }
        public int MpPercentage { get; set; }
        public short MorphId { get; set; }
        public List<IBuff> Buffs { get; }
        public short Speed { get; set; }
        public Direction Direction { get; set; }

        public bool Equals(IEntity other) => other != null && other.EntityType == EntityType && other.Id == Id;
    }
}