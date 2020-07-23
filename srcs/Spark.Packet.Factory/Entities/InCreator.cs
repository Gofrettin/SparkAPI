using System;
using Spark.Core;
using Spark.Core.Enum;
using Spark.Core.Extension;
using Spark.Packet.Entities;

namespace Spark.Packet.Factory.Entities
{
    public class InCreator : PacketCreator<In>
    {
        public override string Header { get; } = "in";
        
        public override In Create(string[] content)
        {
            EntityType entityType = content[0].ToEnum<EntityType>();
            int startIndex = entityType == EntityType.Player ? 3 : 2;
            
            var packet = new In
            {
                EntityType = entityType,
                Name = entityType == EntityType.Player ? content[1] == "-" ? string.Empty : content[1] : string.Empty,
                GameKey = entityType != EntityType.Player ? content[1].ToInt() : 0,
                EntityId = content[startIndex].ToLong(),
                Position = new Vector2D(content[startIndex + 1].ToShort(), content[startIndex + 2].ToShort()),
                Direction = entityType != EntityType.MapObject ? content[startIndex + 3].ToEnum<Direction>() : Direction.North
            };
            
            content = content.Slice(startIndex + (entityType != EntityType.MapObject ? 4 : 3), content.Length - startIndex + (entityType != EntityType.MapObject ? 4 : 3));

            switch (entityType)
            {
                case EntityType.Monster:
                case EntityType.Npc:
                    packet.Npc = new In.NpcInfo
                    {
                        HpPercentage = content[0].ToInt(),
                        MpPercentage = content[1].ToInt(),
                        Faction = content[3].ToEnum<Faction>(),
                        Owner = content[5].ToNullableLong(),
                        Name = content[9]
                    };
                    break;
                case EntityType.Player:
                    packet.Player = new In.PlayerInfo
                    {
                        Gender = content[1].ToEnum<Gender>(),
                        Class = content[4].ToEnum<Class>(),
                        HpPercentage = content[6].ToByte(),
                        MpPercentage = content[7].ToByte()
                    };
                    break;
                case EntityType.MapObject:
                    packet.MapObject = new In.MapObjectInfo
                    {
                        Amount = content[0].ToInt(),
                        IsQuestRelative = content[1].ToBool(),
                        Owner = content[2].ToLong()
                    };
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return packet;
        }
    }
}