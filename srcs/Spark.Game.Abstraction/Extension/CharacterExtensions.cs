using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using Spark.Core;
using Spark.Core.Enum;
using Spark.Game.Abstraction.Entities;
using Spark.Game.Abstraction.Inventory;
using Spark.Packet.Extension;

namespace Spark.Game.Abstraction.Extension
{
    public static class CharacterExtensions
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void WearSp(this ICharacter character)
        {
            character.Client.SendPacket("sl 1");
        }
        
        public static void UnwearSp(this ICharacter character)
        {
            character.Client.SendPacket("sl 0");
        }
        
        public static void Walk(this ICharacter character, Vector2D destination)
        {
            Logger.Debug($"Walking to {destination.X} {destination.Y}");

            if (!character.Map.IsWalkable(destination))
            {
                Logger.Warn("Destination is not walkable");
                return;
            }

            bool positiveX = destination.X > character.Position.X;
            bool positiveY = destination.Y > character.Position.Y;

            Vector2D distance = character.Position.GetDistanceTo(destination);

            int stepX = distance.X > 3 ? 3 : distance.X;
            int stepY = distance.Y > 3 ? 3 : distance.Y;

            short x = (short)((positiveX ? 1 : -1) * stepX + character.Position.X);
            short y = (short)((positiveY ? 1 : -1) * stepY + character.Position.Y);

            var nextPosition = new Vector2D(x, y);

            if (!character.Map.IsWalkable(nextPosition))
            {
                Logger.Warn("Next position is not walkable");
                return;
            }

            Logger.Debug($"Walk to {nextPosition} with speed {character.Speed}");
            character.Client.SendPacket($"walk {nextPosition.X} {nextPosition.Y} {(nextPosition.X + nextPosition.Y) % 3 % 2} {character.Speed}");

            Thread.Sleep((1000 / character.Speed) * ((stepX + stepY) + 3));
            
            character.Position = nextPosition;
            if (!character.Position.Equals(destination))
            {
                character.Walk(destination);
                return;
            }
        
            Logger.Debug($"Walked to {character.Position}");

            IMap map = character.Map;
            IPortal closestPortal = map.Portals.OrderBy(p => p.Position.GetDistance(character.Position)).FirstOrDefault();

            if (closestPortal == null)
            {
                return;
            }
        
            if (character.Position.IsInRange(closestPortal.Position, 2))
            {
                character.Client.SendPacket("preq");
                Logger.Debug("Character on portal, switching map");
            }
        }

        public static void WalkInRange(this ICharacter character, Vector2D position, int range)
        {
            double distance = character.Position.GetDistance(position);
            if (distance <= range)
            {
                return;
            }

            double ratio = (distance - range) / distance;

            double x = character.Position.X + ratio * (position.X - character.Position.X);
            double y = character.Position.Y + ratio * (position.Y - character.Position.Y);

            character.Walk(new Vector2D((short)x, (short)y));
        }

        public static void Attack(this ICharacter character, ISkill skill)
        {
            if (!character.Skills.Contains(skill))
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
            character.Client.SendPacket($"u_s {skill.CastId} {character.EntityType.AsString()} {character.Id}");
            
            Thread.Sleep(skill.CastTime * 100);
        }

        public static void Attack(this ICharacter character, ILivingEntity entity)
        {
            ISkill skill = character.Skills.FirstOrDefault();
            if (skill == null)
            {
                Logger.Warn("Can't found first skill");
                return;
            }

            character.Attack(skill, entity);
        }

        public static void Attack(this ICharacter character, ISkill skill, ILivingEntity entity)
        {
            Logger.Trace($"Trying to attack {entity.Id}");
            if (!character.Skills.Contains(skill))
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
                character.Attack(skill);
                return;
            }

            if (entity.Equals(character))
            {
                Logger.Warn("Can't target self");
                return;
            }

            if (skill.Target == SkillTarget.NoTarget)
            {
                Logger.Warn("Incorrect target type");
                return;
            }

            character.WalkInRange(entity.Position, skill.Range);
            
            Logger.Debug($"Attacking {entity.EntityType} with id {entity.Id} using {skill.SkillKey}");
            character.Client.SendPacket($"u_s {skill.CastId} {entity.EntityType.AsString()} {entity.Id}");
        }

        public static void Rotate(this ICharacter character, Direction direction)
        {
            character.Client.SendPacket($"dir {direction.AsString()} {character.EntityType.AsString()} {character.Id}");
        }

        public static void PickUp(this ICharacter character, IMapObject mapObject)
        {
            character.WalkInRange(mapObject.Position, 1);
            
            Logger.Info($"Picking up item {mapObject.ItemKey}");
            character.Client.SendPacket($"get {character.EntityType.AsString()} {character.Id} {mapObject.Id}");
        }

        public static void UseItem(this ICharacter character, IObjectStack @object)
        {
            character.Client.SendPacket($"u_i {character.EntityType.AsString()} {character.Id} {@object.Bag.AsString()} {@object.Slot} 0 0 ");
        }

        public static void UseItem(this ICharacter character, int itemKey)
        {
            IObjectStack stack = character.Inventory.FindObject(itemKey);
            if (stack == null)
            {
                Logger.Warn($"Can't found item with key {itemKey}");
                return;
            }

            character.UseItem(stack);
        }

        public static void Rest(this ICharacter character)
        {
            character.Client.SendPacket($"rest 1 {character.EntityType.AsString()} {character.Id}");
        }
    }
}