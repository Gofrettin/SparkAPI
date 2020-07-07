using Newtonsoft.Json;

namespace Spark.Gameforge
{
    public class SessionRequest
    {
        [JsonProperty("platformGameAccountId")]
        public string PlatformGameAccountId { get; set; }
    }
}