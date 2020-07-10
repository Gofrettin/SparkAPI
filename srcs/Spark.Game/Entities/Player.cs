using Spark.Core;
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
        public Position Position { get; set; }
        public int Hp { get; set; }
        public int Mp { get; set; }
    }
}