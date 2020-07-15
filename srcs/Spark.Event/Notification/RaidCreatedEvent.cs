using Spark.Game.Abstraction;

namespace Spark.Event.Notification
{
    public class RaidCreatedEvent : IEvent
    {
        public IClient Client { get; }
        public int RaidId { get; }
        public string Owner { get; }

        public RaidCreatedEvent(IClient client, int raidId, string owner)
        {
            Client = client;
            RaidId = raidId;
            Owner = owner;
        }
    }
}