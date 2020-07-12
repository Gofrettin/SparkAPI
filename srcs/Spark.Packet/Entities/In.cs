using System.Linq;
using Spark.Core;
using Spark.Core.Enum;
using Spark.Core.Extension;
using Spark.Packet.Extension;

namespace Spark.Packet.Entities
{
    [Packet("in")]
    public class In : IPacket
    {
        public EntityType EntityType { get; set; }
        public string Name { get; set; }
        public int GameKey { get; set; }
        public long EntityId { get; set; }
        public Vector2D Position { get; set; }
        public Direction Direction { get; set; }
        
        public PlayerInfo Player { get; set; }
        public NpcInfo Npc { get; set; }
        public MapObjectInfo MapObject { get; set; }

        public void Construct(string[] packet)
        {
            EntityType = packet[0].ToEnum<EntityType>();
            
            int startIndex = EntityType == EntityType.Player ? 3 : 2;

            Name = EntityType == EntityType.Player ? packet[1] : string.Empty;
            GameKey = EntityType != EntityType.Player ? packet[1].ToInt() : 0;
            EntityId = packet[startIndex].ToLong();
            Position = new Vector2D(packet[startIndex + 1].ToShort(), packet[startIndex + 2].ToShort());
            Direction = EntityType != EntityType.MapObject ? packet[startIndex + 3].ToEnum<Direction>() : Direction.North;

            packet = packet.Slice(0, startIndex + 3);
            
            switch (EntityType)
            {
                case EntityType.Monster:
                case EntityType.Npc:
                    Npc = new NpcInfo
                    {
                        HpPercentage = packet[0].ToByte(),
                        MpPercentage = packet[1].ToByte(),
                        Faction = packet[3].ToEnum<Faction>(),
                        Owner = packet[5].ToNullableLong(),
                        Name = packet[9]
                    }; 
                    break;
                case EntityType.Player:
                    Player = new PlayerInfo
                    {
                        Gender = packet[1].ToEnum<Gender>(),
                        Class = packet[4].ToEnum<Class>(),
                        HpPercentage = packet[6].ToByte(),
                        MpPercentage = packet[7].ToByte()
                    };
                    break;
                case EntityType.MapObject:
                    MapObject = new MapObjectInfo
                    {
                        Amount = packet[0].ToInt(),
                        IsQuestRelative = packet[1].ToBool(),
                        Owner = packet[2].ToLong()
                    };
                    break;
            }
        }

        public class NpcInfo
        {
            public byte HpPercentage { get; set; }
            public byte MpPercentage { get; set; }
            public Faction Faction { get; set; }
            public long? Owner { get; set; }
            public string Name { get; set; }
        }

        public class PlayerInfo
        {
            public Gender Gender { get; set; }
            public Class Class { get; set; }
            public byte HpPercentage { get; set; }
            public byte MpPercentage { get; set; }
        }

        public class MapObjectInfo
        {
            public int Amount { get; set; }
            public bool IsQuestRelative { get; set; }
            public long Owner { get; set; }
        }
    }
}