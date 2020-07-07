using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Spark.Game;

namespace Spark
{
    public interface ISpark
    {
        Task<IClient> CreateClient(IPEndPoint ip);
        Task<IClient> CreateClient(IPEndPoint ip, string name, int encryptionKey);
        Task<IClient> CreateClient(Process process);
    }
}