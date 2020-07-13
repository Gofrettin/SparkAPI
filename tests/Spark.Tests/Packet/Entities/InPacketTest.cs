using NFluent;
using Spark.Core;
using Spark.Core.Enum;
using Spark.Packet.Entities;

namespace Spark.Tests.Packet.Entities
{
    public class InNpcPacketTest : PacketTest<In>
    {
        protected override string Packet { get; } = "in 2 3093 9326 50 28 2 100 100 11017 0 0 -1 1 0 -1 - 2 -1 0 0 0 0 0 0 0 0 0 0 0";

        protected override In Excepted { get; } = new In
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
        };

        protected override void CheckPacket(In packet)
        {
            Check.That(packet.Npc).IsNotNull();
            Check.That(packet.Npc.HpPercentage).IsEqualTo(100);
        }
    }

    public class InPlayerPacketTest : PacketTest<In>
    {
        protected override string Packet { get; } =
            "in 1 Makalash - 1204334 69 44 2 0 0 2 2 2 204.4856.4868.4865.4031.4129.8362.4266.-1.4443 89 100 0 -1 4 1 0 40 0 0 3 2 -1 - 14 0 0 0 0 88 0 0|0|0 0 0 10 1 9313";

        protected override In Excepted { get; } = new In
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
        };
    }

    public class InMapObjectPacketTest : PacketTest<In>
    {
        protected override string Packet { get; } = "in 9 1241 708392 17 9 80 0 0 0";

        protected override In Excepted { get; } = new In
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
        };
    }
}