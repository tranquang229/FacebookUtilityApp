using Newtonsoft.Json;

namespace FM.Domain.Entities.Facebook.Uid
{
    public class FbUidData
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
