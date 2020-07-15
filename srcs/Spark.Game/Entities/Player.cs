﻿using Spark.Core;
using Spark.Core.Enum;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;

namespace Spark.Game.Entities
{
    public class Player : IPlayer
    {
        public Player(long id)
        {
            Id = id;
            EntityType = EntityType.Player;
        }

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
        public Class Class { get; set; }
        public Gender Gender { get; set; }

        public bool Equals(IEntity other) => other != null && other.EntityType == EntityType && other.Id == Id;
    }
}