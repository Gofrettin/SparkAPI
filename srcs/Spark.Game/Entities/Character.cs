using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NLog;
using Spark.Core;
using Spark.Core.Enum;
using Spark.Core.Extension;
using Spark.Game.Abstraction;
using Spark.Game.Abstraction.Battle;
using Spark.Game.Abstraction.Entities;
using Spark.Game.Abstraction.Inventory;
using Spark.Game.Inventory;

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
            Buffs = new List<IBuff>();
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
        public int Level { get; set; }
        public List<IBuff> Buffs { get; }
        public short Speed { get; set; }
        public Direction Direction { get; set; }
        public ICharacterInventory Inventory { get; }
        public IEnumerable<ISkill> Skills { get; set; }
        public int Hp { get; set; }
        public int Mp { get; set; }
        public int MaxHp { get; set; }
        public int MaxMp { get; set; }
        public Class Class { get; set; }
        public Gender Gender { get; set; }

        public void WearSpecialist()
        {
            Client.SendPacket("sl 1");
        }
        
        public void UnwearSpecialist()
        {
            Client.SendPacket("sl 0");
        }
        
        public void Walk(Vector2D destination)
        {
            Logger.Debug($"Walking to {destination.X} {destination.Y}");

            if (!Map.IsWalkable(destination))
            {
                Logger.Warn("Destination is not walkable");
                return;
            }

            Stack<Vector2D> path = Map.Pathfinder.Find(Position, destination);
            if (path == null)
            {
                Logger.Error($"Can't found path to {destination}");
                return;
            }
            
            while (path.TryPop(out Vector2D position))
            {
                Logger.Debug($"Walk to {position} with speed {Speed}");
                Client.SendPacket($"walk {position.X} {position.Y} {(position.X + position.Y) % 3 % 2} {Speed}");

                Thread.Sleep((100 / Speed) * 23);
                Position = position;
            }

            Logger.Debug($"Walked to {Position}");

            IMap map = Map;
            IPortal closestPortal = map.Portals.OrderBy(p => p.Position.GetDistance(Position)).FirstOrDefault();

            if (closestPortal == null)
            {
                return;
            }
        
            if (Position.IsInRange(closestPortal.Position, 2))
            {
                Client.SendPacket("preq");
                Logger.Debug("Character on portal, switching map");
            }
        }

        public void WalkInRange(Vector2D position, int range)
        {
            double distance = Position.GetDistance(position);
            if (distance <= range)
            {
                return;
            }

            double ratio = (distance - range) / distance;

            double x = Position.X + (ratio * (position.X - Position.X));
            double y = Position.Y + (ratio * (position.Y - Position.Y));

            Walk(new Vector2D((short)x, (short)y));
        }

        public void Attack(ISkill skill)
        {
            if (!Skills.Contains(skill))
            {
                Logger.Warn($"Can't found skill {skill.SkillKey}");
                return;
            }
            
            if (skill.IsOnCooldown)
            {
                Logger.Trace("Skill is in cooldown");
                 return;
            }

            if (skill.Target == SkillTarget.Target)
            {
                Logger.Warn("Trying to use target skill on self");
                return;
            }

            Logger.Debug($"Using skill with id {skill.SkillKey} on self");

            Client.SendPacket($"u_s {skill.CastId} {EntityType.AsString()} {Id}");

            Thread.Sleep(skill.CastTime * 200);
        }

        public void Attack(ILivingEntity entity)
        {
            ISkill skill = Skills.FirstOrDefault();
            if (skill == null)
            {
                Logger.Warn("Can't found first skill");
                return;
            }

            Attack(skill, entity);
        }

        public void Attack(ISkill skill, ILivingEntity entity)
        {
            Logger.Trace($"Trying to attack {entity.Id}");
            if (!Skills.Contains(skill))
            {
                Logger.Warn("Can't found skill in Skills");
                return;
            }

            if (skill.IsOnCooldown)
            {
                Logger.Trace("Skill is in cooldown");
                return;
            }

            if (skill.Target == SkillTarget.Self)
            {
                Attack(skill);
                return;
            }

            if (entity.Equals(this))
            {
                Logger.Warn("Can't target self");
                return;
            }

            if (skill.Target == SkillTarget.NoTarget)
            {
                Logger.Warn("Incorrect target type");
                return;
            }

            WalkInRange(entity.Position, skill.Range);
            Logger.Debug($"Attacking {entity.EntityType} with id {entity.Id} using {skill.SkillKey}");
            
            Client.SendPacket($"u_s {skill.CastId} {entity.EntityType.AsString()} {entity.Id}");
            Thread.Sleep(skill.CastTime * 200);
        }

        public void Rotate(Direction direction)
        {
            Client.SendPacket($"dir {direction.AsString()} {EntityType.AsString()} {Id}");
        }

        public void PickUp(IMapObject mapObject)
        {
            WalkInRange(mapObject.Position, 1);
            
            Logger.Info($"Picking up item {mapObject.ItemKey}");
            Client.SendPacket($"get {EntityType.AsString()} {Id} {mapObject.Id}");
        }

        public void UseObject(IObjectStack objectStack)
        {
            Client.SendPacket($"u_i {EntityType.AsString()} {Id} {objectStack.Bag.AsString()} {objectStack.Slot} 0 0 ");
        }

        public void UseObject(int objectKey)
        {
            IObjectStack stack = Inventory.FindObject(objectKey);
            if (stack == null)
            {
                Logger.Warn($"Can't found item with key {objectKey}");
                return;
            }

            UseObject(stack);
        }

        public void Rest()
        {
            Client.SendPacket($"rest 1 {EntityType.AsString()} {Id}");
        }
        
        public bool Equals(IEntity other)
        {
            return other != null && other.EntityType == EntityType && other.Id == Id;
        }
    }
}