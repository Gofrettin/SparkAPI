using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Spark.Gameforge.Extension;
using Spark.Gameforge.Nostale;

namespace Spark.Gameforge
{
    public sealed class GameforgeService : IGameforgeService
    {
        private const string Url = "https://spark.gameforge.com/api/v1";
        private const string UserAgent = "GameforgeClient/2.0.48";
        private const string MediaType = "application/json";

        private static readonly HttpClient HttpClient = new HttpClient();

        public GameforgeService() => InstallationId = Guid.NewGuid();

        public Guid InstallationId { get; }

        public async Task<GameforgeResponse<string>> GetAuthToken(string email, string password, string locale)
        {
            string json = JsonConvert.SerializeObject(new AuthRequest
            {
                Email = email,
                Password = password,
                Locale = locale
            });

            using (var request = new HttpRequestMessage(HttpMethod.Post, $"{Url}/auth/sessions"))
            {
                request.Content = new StringContent(json, Encoding.UTF8, MediaType);
                request.Headers.Add("TNT-Installation-Id", InstallationId.ToString());

                HttpResponseMessage response = await HttpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    return new GameforgeResponse<string>(string.Empty, response.StatusCode);
                }

                string content = await response.Content.ReadAsStringAsync();
                Dictionary<string, string> parsedContent = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);

                return new GameforgeResponse<string>(parsedContent["token"], response.StatusCode);
            }
        }

        public async Task<GameforgeResponse<IEnumerable<GameforgeAccount>>> GetAccounts(string authToken)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"{Url}/user/accounts"))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                request.Headers.Add("User-Agent", UserAgent);
                request.Headers.Add("TNT-Installation-Id", InstallationId.ToString());

                HttpResponseMessage response = await HttpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    return new GameforgeResponse<IEnumerable<GameforgeAccount>>(Array.Empty<GameforgeAccount>(), response.StatusCode);
                }

                string content = await response.Content.ReadAsStringAsync();
                Dictionary<string, GameforgeAccount> parsedContent = JsonConvert.DeserializeObject<Dictionary<string, GameforgeAccount>>(content);

                return new GameforgeResponse<IEnumerable<GameforgeAccount>>(parsedContent.Values.ToArray(), response.StatusCode);
            }
        }

        public async Task<GameforgeResponse<string>> GetSessionToken(string authToken, GameforgeAccount account)
        {
            string json = JsonConvert.SerializeObject(new SessionRequest
            {
                PlatformGameAccountId = account.Id
            });

            using (var request = new HttpRequestMessage(HttpMethod.Post, $"{Url}/auth/thin/codes"))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                request.Headers.Add("User-Agent", UserAgent);
                request.Headers.Add("TNT-Installation-Id", InstallationId.ToString());

                request.Content = new StringContent(json, Encoding.UTF8, MediaType);

                HttpResponseMessage response = await HttpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    return new GameforgeResponse<string>(string.Empty, response.StatusCode);
                }

                string content = await response.Content.ReadAsStringAsync();
                Dictionary<string, string> parsedContent = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);

                return new GameforgeResponse<string>(parsedContent["code"].ToHex(), response.StatusCode);
            }
        }

        public async Task<GameforgeResponse<string>> GetSessionToken(string email, string password, string locale, Predicate<GameforgeAccount> accountPredicate)
        {
            GameforgeResponse<string> authResponse = await GetAuthToken(email, password, locale);
            if (authResponse.Status != HttpStatusCode.Created)
            {
                return new GameforgeResponse<string>("Failed authentication", authResponse.Status);
            }

            GameforgeResponse<IEnumerable<GameforgeAccount>> accountResponse = await GetAccounts(authResponse.Content);
            if (accountResponse.Status != HttpStatusCode.OK)
            {
                return new GameforgeResponse<string>("Failed to get accounts", accountResponse.Status);
            }

            GameforgeAccount account = accountResponse.Content.FirstOrDefault(accountPredicate.Invoke);
            if (account == null)
            {
                return new GameforgeResponse<string>("Can't found account", HttpStatusCode.NotFound);
            }

            GameforgeResponse<string> sessionResponse = await GetSessionToken(authResponse.Content, account);
            if (sessionResponse.Status != HttpStatusCode.Created)
            {
                return new GameforgeResponse<string>("Failed to get session token", sessionResponse.Status);
            }

            return sessionResponse;
        }

        public async Task<NostaleClientInfo> GetNostaleClientInfo(string locale)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, $"{Url}/patching/download/latest/nostale/default?locale={locale}&architecture=x64&branchToken"))
            {
                HttpResponseMessage response = await HttpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine("?");
                    return default;
                }

                string content = await response.Content.ReadAsStringAsync();
                
                NostalePatch patch = JsonConvert.DeserializeObject<NostalePatch>(content);

                NostaleFile dx = patch.Entries.Find(x => x.File == "NostaleClientX.exe");
                NostaleFile gl = patch.Entries.Find(x => x.File == "NostaleClient.exe");

                if (dx == null || gl == null)
                {
                    return default;
                }

                string dxHash = await GetRemoteFileMd5($"http://patches.gameforge.com/{dx.Path}");
                string glHash = await GetRemoteFileMd5($"http://patches.gameforge.com/{gl.Path}");
                string version = await GetRemoteFileVersion($"http://patches.gameforge.com/{dx.Path}");

                return new NostaleClientInfo
                {
                    DxHash = dxHash,
                    GlHash = glHash,
                    Version = version
                };
            }
        }

        private static async Task<string> GetRemoteFileMd5(string url)
        {
            await using (Stream stream = await HttpClient.GetStreamAsync(url))
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), Guid.NewGuid().ToString());
                using (FileStream file = File.Create(path))
                {
                    await stream.CopyToAsync(file);
                }

                string hash; 
                using (FileStream file = File.OpenRead(path))
                {
                    using (var md5 = MD5.Create())
                    {
                        hash =  BitConverter.ToString(md5.ComputeHash(file)).Replace("-", "");
                    }
                }
                
                File.Delete(path);
                return hash;
            }
        }

        private static async Task<string> GetRemoteFileVersion(string url)
        {
            await using (Stream stream = await HttpClient.GetStreamAsync(url))
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), Guid.NewGuid().ToString());
                using (FileStream file = File.Create(path))
                {
                    await stream.CopyToAsync(file);
                }
                
                string version = FileVersionInfo.GetVersionInfo(path).FileVersion;
                
                File.Delete(path);
                return version;
            }
        }
    }
}