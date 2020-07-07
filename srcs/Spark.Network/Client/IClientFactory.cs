using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Spark.Game;

namespace Spark.Network.Client
{
    public interface IClientFactory
    {
        Task<IClient> CreateClient(IPEndPoint server);
        Task<IClient> CreateClient(IPEndPoint server, string name, int encryptionKey);
        Task<IClient> CreateClient(Process process);
    }
}