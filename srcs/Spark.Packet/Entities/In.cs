using System;
using Spark.Core;
using Spark.Core.Enum;

namespace Spark.Packet.Entities
{
    public class In : IPacket
    {
        public EntityType EntityType { get; set; }
        public string Name { get; set; } = string.Empty;
        public int GameKey { get; set; }
        public long EntityId { get; set; }
        public Vector2D Position { get; set; }
        public Direction Direction { get; set; }

        public PlayerInfo Player { get; set; }
        public NpcInfo Npc { get; set; }
        public MapObjectInfo MapObject { get; set; }
        
        public class NpcInfo
        {
            public int HpPercentage { get; set; }
            public int MpPercentage { get; set; }
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