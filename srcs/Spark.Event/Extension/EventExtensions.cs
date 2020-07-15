using Spark.Event.Exchange;
using Spark.Event.Notification;

namespace Spark.Event.Extension
{
    public static class EventExtensions
    {
        public static void Join(this RaidListNotifyEvent e)
        {
            e.Client.SendPacket($"rl 3 {e.Owner}");
        }

        public static void Accept(this ExchangeRequestEvent e)
        {
            e.Client.SendPacket($"#req_exc^2^{e.Sender.Id}");
        }

        public static void Deny(this ExchangeRequestEvent e)
        {
            e.Client.SendPacket($"#req_exc^5^{e.Sender.Id}");
        }

        public static void Lock(this ExchangeLockedEvent e)
        {
            e.Client.SendPacket("exc_list 0 0");
        }

        public static void Validate(this ExchangeLockedEvent e)
        {
            e.Client.SendPacket("req_exc 3");
        }

        public static void Lock(this ExchangeStartedEvent e)
        {
            e.Client.SendPacket("exc_list 0 0");
        }
    }
}