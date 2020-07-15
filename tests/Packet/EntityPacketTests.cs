using Spark.Core;
using Spark.Core.Enum;
using Spark.Packet.Entities;
using Spark.Tests.Attributes;

namespace Spark.Tests.Packet
{
    public class EntityPacketTests : PacketTests
    {
        [PacketTest(typeof(CMode))]
        public void CMode_Test()
        {
            CreateAndCheckValues("c_mode 1 123456 11 13 11 0 10 0", new CMode
            {
                EntityType = EntityType.Player,
                EntityId = 123456,
                MorphId = 11,
                MorphUpgrade = 13,
                MorphDesign = 11,
                MorphBonus = 0,
                Size = 10
            });
        }

        [PacketTest(typeof(Cond))]
        public void Cond_Test()
        {
            CreateAndCheckValues("cond 1 123456 0 0 12", new Cond
            {
                EntityType = EntityType.Player,
                EntityId = 123456,
                CanAttack = true,
                CanMove = true,
                Speed = 12
            });
        }

        [PacketTest(typeof(Dir))]
        public void Dir_Test()
        {
            CreateAndCheckValues("dir 1 123456 0", new Dir
            {
                EntityType = EntityType.Player,
                EntityId = 123456,
                Direction = Direction.North
            });
        }

        [PacketTest(typeof(In))]
        public void In_As_Npc_Test()
        {
            CreateAndCheckValues("in 2 3093 9326 50 28 2 100 100 11017 0 0 -1 1 0 -1 - 2 -1 0 0 0 0 0 0 0 0 0 0 0", new In
            {
                EntityType = EntityType.Npc,
                EntityId = 9326,
                GameKey = 3093,
                Position = new Vector2D(50, 28),
                Direction = Direction.South,
                Npc = new In.NpcInfo
                {
                    HpPercentage = 100,
                    MpPercentage = 100,
                    Faction = Faction.Neutral,
                    Name = string.Empty,
                    Owner = null
                }
            });
        }

        [PacketTest(typeof(In))]
        public void In_As_Player_Test()
        {
            CreateAndCheckValues("in 1 Makalash - 1204334 69 44 2 0 0 2 2 2 204.4856.4868.4865.4031.4129.8362.4266.-1.4443 89 100 0 -1 4 1 0 40 0 0 3 2 -1 - 14 0 0 0 0 88 0 0|0|0 0 0 10 1 9313", new In
            {
                Name = "Makalash",
                EntityId = 1204334,
                Position = new Vector2D(69, 44),
                Direction = Direction.South,
                EntityType = EntityType.Player,
                Player = new In.PlayerInfo
                {
                    HpPercentage = 89,
                    MpPercentage = 100,
                    Class = Class.Archer,
                    Gender = Gender.Male
                }
            });
        }

        [PacketTest(typeof(In))]
        public void In_As_MapObject_Test()
        {
            CreateAndCheckValues("in 9 1241 708392 17 9 80 0 0 0", new In
            {
                EntityType = EntityType.MapObject,
                GameKey = 1241,
                EntityId = 708392,
                Position = new Vector2D(17, 9),
                MapObject = new In.MapObjectInfo
                {
                    Amount = 80,
                    IsQuestRelative = false,
                    Owner = 0
                }
            });
        }

        [PacketTest(typeof(Mv))]
        public void Mv_Test()
        {
            CreateAndCheckValues("mv 3 2102 24 143 4", new Mv
            {
                EntityType = EntityType.Monster,
                EntityId = 2102,
                Position = new Vector2D(24, 143),
                Speed = 4
            });
        }

        [PacketTest(typeof(Out))]
        public void Out_Test()
        {
            CreateAndCheckValues("out 1 123456", new Out
            {
                EntityType = EntityType.Player,
                EntityId = 123456
            });
        }
    }
}