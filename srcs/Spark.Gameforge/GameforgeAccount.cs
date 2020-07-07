using Newtonsoft.Json;

namespace Spark.Gameforge
{
    public class GameforgeAccount
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("displayName")]
        public string Name { get; set; }
    }
}