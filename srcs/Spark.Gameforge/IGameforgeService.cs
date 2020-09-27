using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Spark.Gameforge.Nostale;

namespace Spark.Gameforge
{
    public interface IGameforgeService
    {
        Guid InstallationId { get; }

        Task<GameforgeResponse<string>> GetAuthToken(string email, string password, string locale);
        Task<GameforgeResponse<IEnumerable<GameforgeAccount>>> GetAccounts(string authToken);
        Task<GameforgeResponse<string>> GetSessionToken(string authToken, GameforgeAccount account);
        Task<GameforgeResponse<string>> GetSessionToken(string email, string password, string locale, Predicate<GameforgeAccount> accountPredicate);

        Task<NostaleClientInfo> GetNostaleClientInfo(string locale);
    }
}