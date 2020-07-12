using System;
using System.Net;
using System.Threading.Tasks;
using Spark.Core;
using Spark.Core.Server;
using Spark.Event;
using Spark.Game.Abstraction;
using Spark.Gameforge;

namespace Spark
{
    public interface ISpark
    {
        IGameforgeService GameforgeService { get; }
        IEventPipeline EventPipeline { get; }

        IClient CreateRemoteClient(IPEndPoint ip, string token, Predicate<WorldServer> serverSelector, Predicate<SelectableCharacter> characterSelector);
        Task<GameforgeResponse<string>> GetSessionToken(string email, string password, string locale, Predicate<GameforgeAccount> accountSelector);

        void AddEventHandler(IEventHandler eventHandler);
        void AddEventHandler<T>(Action<T> handler) where T : IEvent;
    }
}