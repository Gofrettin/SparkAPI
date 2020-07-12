using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using Spark.Core;
using Spark.Core.Enum;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Entities;
using Spark.Packet.Extension;

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
            
            Skills = new List<ISkill>();
        }

        public long Id { get; }
        public EntityType EntityType { get; }
        public string Name { get; set; }
        public IMap Map { get; set; }
        public Vector2D Position { get; set; }
        public int HpPercentage { get; set; }
        public int MpPercentage { get; set; }
        public byte Speed { get; set; }
        public Direction Direction { get; set; }
        public IEnumerable<ISkill> Skills { get; set; }

        public Task Attack(ISkill skill)
        {
            if (!Skills.Contains(skill))
            {
                return Task.CompletedTask;
            }

            if (skill.Target == SkillTarget.Target)
            {
                return Task.CompletedTask;
            }
            
            Client.SendPacket($"u_s {skill.CastId} {EntityType.AsString()} {Id}");
            return Task.Delay(skill.CastTime * 100);
        }

        public Task Attack(ISkill skill, ILivingEntity entity)
        {
            if (!Skills.Contains(skill))
            {
                return Task.CompletedTask;
            }

            if (skill.Target == SkillTarget.Self)
            {
                return Attack(skill);
            }

            if (entity.Equals(this))
            {
                return Task.CompletedTask;
            }

            if (skill.Target == SkillTarget.NoTarget)
            {
                return Task.CompletedTask;
            }

            return WalkInRange(entity.Position, skill.Range).ContinueWith(x =>
            {
                Client.SendPacket($"u_s {skill.CastId} {entity.EntityType.AsString()} {entity.Id}");
                return Task.Delay(skill.CastTime * 100);
            });
        }

        public Task Attack(ILivingEntity entity)
        {
            if (entity.Equals(this))
            {
                return Task.CompletedTask;
            }

            ISkill skill = Skills.FirstOrDefault();
            if (skill == null)
            {
                return Task.CompletedTask;
            }

            return Attack(skill, entity);
        }

        public IClient Client { get; }

        public Task Walk(Vector2D destination)
        {
            if (!Map.IsWalkable(destination))
            {
                Logger.Warn("Destination is not walkable");
                return Task.CompletedTask;
            }

            bool positiveX = destination.X > Position.X;
            bool positiveY = destination.Y > Position.Y;
            
            Vector2D distance = Position.GetDistanceTo(destination);

            int stepX = distance.X >= 5 ? 5 : distance.X;
            int stepY = distance.Y >= 5 ? 5 : distance.Y;

            short x = (short)((positiveX ? 1 : -1) * stepX + Position.X);
            short y = (short)((positiveY ? 1 : -1) * stepY + Position.Y);
            
            var nextPosition = new Vector2D(x, y);

            if (!Map.IsWalkable(nextPosition))
            {
                Logger.Warn("Next position is not walkable");
                return Task.CompletedTask;
            }
            
            Client.SendPacket($"walk {nextPosition.X} {nextPosition.Y} {((nextPosition.X + nextPosition.Y) % 3) % 2} {Speed}");

            return Task.Delay((stepX + stepY) * (1000 / Speed)).ContinueWith(s =>
            {
                Position = nextPosition;
                if (!Position.Equals(destination))
                {
                    Walk(destination);
                } 
                Logger.Info($"Moved to {Position}");
            }, TaskContinuationOptions.ExecuteSynchronously);
        }

        public Task WalkInRange(Vector2D position, int range)
        {
            int distance = Position.GetDistance(position);
            if (distance <= range)
            {
                return Task.CompletedTask;
            }

            double ratio = (distance - range) / (double)distance;
            
            double x = Position.X + ratio * (position.X - Position.X);
            double y = Position.Y + ratio * (position.Y - Position.Y);

            return Walk(new Vector2D((short)x, (short)y));
        }

        public void Turn(Direction direction)
        {
            Client.SendPacket($"dir {direction.AsString()} {EntityType.AsString()} {Id}");
        }

        public bool Equals(IEntity other) => other != null && other.EntityType == EntityType && other.Id == Id;
    }
}