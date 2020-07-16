using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        
        public static Task Walk(this ICharacter character, Vector2D destination)
        {
            if (!character.Map.IsWalkable(destination))
            {
                Logger.Warn("Destination is not walkable");
                return Task.CompletedTask;
            }

            bool positiveX = destination.X > character.Position.X;
            bool positiveY = destination.Y > character.Position.Y;

            Vector2D distance = character.Position.GetDistanceTo(destination);

            int stepX = distance.X >= 5 ? 5 : distance.X;
            int stepY = distance.Y >= 5 ? 5 : distance.Y;

            short x = (short)((positiveX ? 1 : -1) * stepX + character.Position.X);
            short y = (short)((positiveY ? 1 : -1) * stepY + character.Position.Y);

            var nextPosition = new Vector2D(x, y);

            if (!character.Map.IsWalkable(nextPosition))
            {
                Logger.Warn("Next position is not walkable");
                return Task.CompletedTask;
            }

            character.Client.SendPacket($"walk {nextPosition.X} {nextPosition.Y} {(nextPosition.X + nextPosition.Y) % 3 % 2} {character.Speed}");

            return Task.Delay((stepX + stepY) * (1000 / character.Speed)).ContinueWith(s =>
            {
                character.Position = nextPosition;
                if (!character.Position.Equals(destination))
                {
                    return character.Walk(destination);
                }
                
                Logger.Info($"Moved to {character.Position}");

                IMap map = character.Map;
                IPortal closestPortal = map.Portals.OrderBy(p => p.Position.GetDistance(character.Position)).FirstOrDefault();

                if (closestPortal == null)
                {
                    return Task.CompletedTask;
                }
                
                if (character.Position.IsInRange(closestPortal.Position, 2))
                {
                    character.Client.SendPacket("preq");
                    Logger.Info("Character on portal, switching map");
                }
                
                return Task.CompletedTask;
            }, TaskContinuationOptions.ExecuteSynchronously);
        }

        public static Task WalkInRange(this ICharacter character, Vector2D position, int range)
        {
            int distance = character.Position.GetDistance(position);
            if (distance <= range)
            {
                return Task.CompletedTask;
            }

            double ratio = (distance - range) / (double)distance;

            double x = character.Position.X + ratio * (position.X - character.Position.X);
            double y = character.Position.Y + ratio * (position.Y - character.Position.Y);

            return character.Walk(new Vector2D((short)x, (short)y));
        }

        public static Task Attack(this ICharacter character, ISkill skill)
        {
            if (!character.Skills.Contains(skill))
            {
                return Task.CompletedTask;
            }

            if (skill.Target == SkillTarget.Target)
            {
                return Task.CompletedTask;
            }

            character.Client.SendPacket($"u_s {skill.CastId} {character.EntityType.AsString()} {character.Id}");
            return Task.Delay(skill.CastTime * 100);
        }

        public static Task Attack(this ICharacter character, ILivingEntity entity)
        {
            ISkill skill = character.Skills.FirstOrDefault();
            if (skill == null)
            {
                Logger.Warn("Can't found first skill");
                return Task.CompletedTask;
            }

            return character.Attack(skill, entity);
        }

        public static Task Attack(this ICharacter character, ISkill skill, ILivingEntity entity)
        {
            if (!character.Skills.Contains(skill))
            {
                Logger.Warn("Can't found skill in Skills");
                return Task.CompletedTask;
            }

            if (skill.Target == SkillTarget.Self)
            {
                return character.Attack(skill);
            }

            if (entity.Equals(character))
            {
                Logger.Warn("Can't target self");
                return Task.CompletedTask;
            }

            if (skill.Target == SkillTarget.NoTarget)
            {
                Logger.Warn("Incorrect target type");
                return Task.CompletedTask;
            }

            return character.WalkInRange(entity.Position, skill.Range).ContinueWith(x =>
            {
                character.Client.SendPacket($"u_s {skill.CastId} {entity.EntityType.AsString()} {entity.Id}");
                return Task.Delay(skill.CastTime * 100);
            });
        }

        public static void Rotate(this ICharacter character, Direction direction)
        {
            character.Client.SendPacket($"dir {direction.AsString()} {character.EntityType.AsString()} {character.Id}");
        }

        public static Task PickUp(this ICharacter character, IMapObject mapObject)
        {
            return character.WalkInRange(mapObject.Position, 1).ContinueWith(x => { character.Client.SendPacket($"get {character.EntityType.AsString()} {character.Id} {mapObject.Id}"); });
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
                return;
            }

            character.UseItem(stack);
        }
    }
}