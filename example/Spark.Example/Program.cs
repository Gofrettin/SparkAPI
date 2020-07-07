using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using NLog;
using Spark.Core.Server;
using Spark.Game.Abstraction;
using Spark.Gameforge;

namespace Spark.Example
{
    public static class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        public static async Task Main(string[] args)
        {
            using (ISpark spark = Spark.CreateInstance())
            {
                GameforgeResponse<string> auth = await spark.GameforgeService.GetAuthToken("zeezeearmy@gmail.com", "yann59150", "fr-FR");
                if (auth.Status != HttpStatusCode.Created)
                {
                    Logger.Error($"Failed gameforge auth {auth.Status}");
                    return;
                }

                GameforgeResponse<IEnumerable<GameforgeAccount>> accounts = await spark.GameforgeService.GetAccounts(auth.Content);
                if (accounts.Status != HttpStatusCode.OK)
                {
                    Logger.Error($"Failed to get accounts {accounts.Status}");
                    return;
                }

                foreach (GameforgeAccount account in accounts.Content)
                {
                    GameforgeResponse<string> token = await spark.GameforgeService.GetSessionToken(auth.Content, account);
                    if (token.Status != HttpStatusCode.Created)
                    {
                        Logger.Error($"Failed to get token for {account.Name} ({token.Status})");
                        continue;
                    }
                    
                    await spark.CreateRemoteClient(LoginServer.Fr, token.Content, 
                        x => x.ServerId == 2 && x.ChannelId == 5,
                        x => x.Slot == 0);

                    await Task.Delay(1000);
                }

                await Task.Delay(-1);
            }
        }
    }
}