using System;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using Spark.Core;
using Spark.Core.Enum;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;

namespace Spark.Game.Entities
{
    public class Character : ICharacter
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        public Character(long id, IClient client)
        {
            Id = id;
            EntityType = EntityType.Player;
            Client = client;
        }

        public long Id { get; }
        public EntityType EntityType { get; }
        public string Name { get; set; }
        public IMap Map { get; set; }
        public Position Position { get; set; }
        public int Hp { get; set; }
        public int Mp { get; set; }
        public byte Speed { get; set; }
        public Direction Direction { get; set; }

        public IClient Client { get; }
        
        public void Move(Position destination)
        {
            if (!Map.IsWalkable(destination))
            {
                Logger.Warn("Destination is not walkable");
                return;
            }

            bool positiveX = destination.X > Position.X;
            bool positiveY = destination.Y > Position.Y;
            
            Position distance = Position.GetDistance(destination);

            int stepX = distance.X >= 5 ? 5 : distance.X;
            int stepY = distance.Y >= 5 ? 5 : distance.Y;

            short x = (short)((positiveX ? 1 : -1) * stepX + Position.X);
            short y = (short)((positiveY ? 1 : -1) * stepY + Position.Y);
            
            var nextPosition = new Position(x, y);

            if (!Map.IsWalkable(nextPosition))
            {
                Logger.Warn("Next position is not walkable");
                return;
            }
            
            Client.SendPacket($"walk {nextPosition.X} {nextPosition.Y} 1 {Speed}");

            Task.Delay((stepX + stepY) * (1000 / Speed)).ContinueWith(s =>
            {
                Position = nextPosition;
                if (!Position.Equals(destination))
                {
                    Move(destination);
                }
                Logger.Info($"Moved to {Position}");
            }, TaskContinuationOptions.ExecuteSynchronously);
        }
    }
}