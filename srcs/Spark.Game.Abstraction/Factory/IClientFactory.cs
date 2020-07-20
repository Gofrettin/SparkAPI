using System;
using System.Diagnostics;
using System.Net;
using Spark.Core;
using Spark.Core.Server;

namespace Spark.Game.Abstraction.Factory
{
    public interface IClientFactory
    {
        IClient CreateRemoteClient(IPEndPoint ip, Predicate<WorldServer> serverSelector, Predicate<SelectableCharacter> characterSelector);
        IClient CreateLocalClient(Process process);
    }
}