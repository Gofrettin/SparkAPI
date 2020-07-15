using Spark.Event.Notification;

namespace Spark.Event.Extension
{
    public static class EventExtensions
    {
        public static void JoinRaid(this RaidNotifyEvent e)
        {
            e.Client.SendPacket($"rl 3 {e.Owner}");
        }
    }
}