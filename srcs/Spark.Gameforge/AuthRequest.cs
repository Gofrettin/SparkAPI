using Newtonsoft.Json;

namespace Spark.Gameforge
{
    public class AuthRequest
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }
    }
}