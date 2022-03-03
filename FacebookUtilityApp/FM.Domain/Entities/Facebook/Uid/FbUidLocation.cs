using Newtonsoft.Json;

namespace FM.Domain.Entities.Facebook.Uid
{
    public class FbUidLocation
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("location")]
        public FbUidLocationLocation Location { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

    }
}