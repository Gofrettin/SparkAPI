using System.Collections.Generic;
using Spark.Core.Server;

namespace Spark.Event.Login
{
    public class LoginCompleteEvent : IEvent
    {
        public LoginCompleteEvent(string name, int encryptionKey, IEnumerable<WorldServer> servers)
        {
            Name = name;
            EncryptionKey = encryptionKey;
            Servers = servers;
        }

        public string Name { get; }
        public int EncryptionKey { get; }
        public IEnumerable<WorldServer> Servers { get; }
    }
}