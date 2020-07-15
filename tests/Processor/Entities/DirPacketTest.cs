using NFluent;
using Spark.Core.Enum;
using Spark.Database.Data;
using Spark.Game;
using Spark.Game.Abstraction;
using Spark.Game.Entities;
using Spark.Packet.Entities;

namespace Spark.Tests.Processor.Entities
{
    public class DirPacketTest : ProcessorTest<Dir>
    {
        protected override Dir Packet { get; } = new Dir
        {
            EntityType = EntityType.Monster,
            EntityId = 123,
            Direction = Direction.North
        };

        public DirPacketTest()
        {
            Map.AddEntity(Client.Character);
        }
        
        protected override void CheckOutput()
        {
            Check.That(Client.Character.Direction).IsEqualTo(Direction.North);
        }
    }
}