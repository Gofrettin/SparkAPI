using System.Net;

namespace Spark.Network.Session
{
    public interface ISessionFactory
    {
        ISession CreateSession(IPEndPoint ip);
        ISession CreateSession(IPEndPoint ip, int encryptionKey);
    }
}