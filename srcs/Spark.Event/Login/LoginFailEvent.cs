using Spark.Game.Abstraction;

namespace Spark.Event.Login
{
    public class LoginFailEvent : IEvent
    {
        public IClient Client { get; }
        public byte Reason { get; }

        public LoginFailEvent(IClient client, byte reason)
        {
            Client = client;
            Reason = reason;
        }
    }
}