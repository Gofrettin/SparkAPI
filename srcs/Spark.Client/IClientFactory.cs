using System;
using System.Net;
using System.Threading.Tasks;
using Spark.Core;
using Spark.Core.Server;

namespace Spark.Client
{
    public interface IClientFactory
    {
        Task<Game.Client> CreateClient(IPEndPoint ip, Predicate<WorldServer> serverSelector, Predicate<SelectableCharacter> characterSelector);
    }
}