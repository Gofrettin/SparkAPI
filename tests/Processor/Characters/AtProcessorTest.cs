using NFluent;
using Spark.Core;
using Spark.Core.Enum;
using Spark.Packet.Characters;

namespace Spark.Tests.Processor.Characters
{
    public class AtProcessorTest : ProcessorTest<At>
    {
        protected override At Packet { get; } = new At
        {
            EntityId = 123456,
            MapId = 2544,
            Position = new Vector2D(24, 42),
            Direction = Direction.South
        };
            
        protected override void CheckResult()
        {
            Check.That(Client.Character.Map).IsNotNull();
            Check.That(Client.Character.Map.Id).IsEqualTo(Packet.MapId);
            Check.That(Client.Character.Position).IsEqualTo(Packet.Position);
            Check.That(Client.Character.Direction).IsEqualTo(Packet.Direction);
        }
    }
}