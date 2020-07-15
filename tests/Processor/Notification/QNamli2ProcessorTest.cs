using Moq;
using Spark.Event.Notification;
using Spark.Packet.Notification;

namespace Spark.Tests.Processor.Notification
{
    public class QNamli2ProcessorTest : ProcessorTest<QNamli2>
    {
        protected override QNamli2 Packet { get; } = new QNamli2
        {
            Raid = new QNamli2.RaidInfo
            {
                Owner = "MyNameIs"
            }
        };

        protected override void CheckEvent()
        {
            EventPipelineMock.Verify(x => x.Emit(It.Is<RaidNotifyEvent>(s => s.Owner == "MyNameIs")), Times.Once);
        }
    }
}