using System.Collections.Generic;
using Spark.Core;
using Spark.Core.Enum;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;
using Spark.Game.Abstraction.Inventory;
using Spark.Game.Inventory;

namespace Spark.Game.Entities
{
    public class Character : ICharacter
    {
        public Character(long id, IClient client)
        {
            Id = id;
            EntityType = EntityType.Player;
            Client = client;

            Skills = new List<ISkill>();
            Inventory = new CharacterInventory();
        }

        public IClient Client { get; }

        public long Id { get; }
        public EntityType EntityType { get; }
        public string Name { get; set; }
        public IMap Map { get; set; }
        public Vector2D Position { get; set; }
        public int HpPercentage { get; set; }
        public int MpPercentage { get; set; }
        public short MorphId { get; set; }
        public byte Speed { get; set; }
        public Direction Direction { get; set; }
        public ICharacterInventory Inventory { get; }
        public IEnumerable<ISkill> Skills { get; set; }
        public int Hp { get; set; }
        public int Mp { get; set; }
        public int MaxHp { get; set; }
        public int MaxMp { get; set; }
        public Class Class { get; set; }
        public Gender Gender { get; set; }

        public bool Equals(IEntity other) => other != null && other.EntityType == EntityType && other.Id == Id;
    }
}