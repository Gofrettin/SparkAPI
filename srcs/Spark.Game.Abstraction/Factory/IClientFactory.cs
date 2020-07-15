using System;
using System.Net;
using Spark.Core;
using Spark.Core.Server;

namespace Spark.Game.Abstraction.Factory
{
    public interface IClientFactory
    {
        IClient CreateClient(IPEndPoint ip, Predicate<WorldServer> serverSelector, Predicate<SelectableCharacter> characterSelector);
    }
}