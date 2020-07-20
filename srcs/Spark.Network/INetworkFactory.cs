using System.Diagnostics;
using System.Net;

namespace Spark.Network
{
    public interface INetworkFactory
    {
        INetwork CreateRemoteNetwork(IPEndPoint ip);
        INetwork CreateRemoteNetwork(IPEndPoint ip, int encryptionKey);
        INetwork CreateLocalNetwork(Process process);
    }
}