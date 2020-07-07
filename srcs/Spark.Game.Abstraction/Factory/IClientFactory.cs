using System;
using System.Net;
using System.Threading.Tasks;
using Spark.Core;
using Spark.Core.Server;

namespace Spark.Game.Abstraction.Factory
{
    public interface IClientFactory
    {
        Task<IClient> CreateClient(IPEndPoint ip, Predicate<WorldServer> serverSelector, Predicate<SelectableCharacter> characterSelector);
    }
}