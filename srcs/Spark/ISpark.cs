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
        IClient CreateRemoteClient(IPEndPoint ip, string token, Predicate<WorldServer> serverSelector, Predicate<SelectableCharacter> characterSelector);
        Task<GameforgeResponse<string>> GetSessionToken(string email, string password, string locale, Predicate<GameforgeAccount> predicate);

        void AddEventHandler<T>(T handler) where T : IEventHandler;
    }
}